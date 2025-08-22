using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.UnitTests.Common;
using Moq;

namespace Domain.UnitTests.BoundedContexts.UserContext.UserAggregate
{
    public class UserManagementTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly UserManagement _userManagement;

        public UserManagementTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _userManagement = new UserManagement(_userRepoMock.Object);
        }

        private IQueryable<User> BuildUsers(params User[] users)
            => users.AsQueryable();

        [Fact]
        public void GetAll_Should_Call_Repository()
        {
            var users = BuildUsers(new User("Test", "test@mail.com", "123"));
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(users.BuildMock());

            var result = _userManagement.GetAll();
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task CreateUser_Should_Throw_When_Email_Exists()
        {
            var users = BuildUsers(new User("Exist", "exist@mail.com", "123"));
            _userRepoMock.Setup(r => r.GetAll(false)).Returns(users.BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.CreateUserAsync("Test", "exist@mail.com", "123"));
        }

        [Fact]
        public async Task CreateUser_Should_Return_User_When_NotExists()
        {
            _userRepoMock.Setup(r => r.GetAll(false)).Returns(BuildUsers().BuildMock());

            var result = await _userManagement.CreateUserAsync("Test", "new@mail.com", "123");

            Assert.Equal("Test", result.FullName);
            Assert.Equal("new@mail.com", result.Email);
        }

        [Fact]
        public async Task UpdateUser_Should_Throw_When_Email_Conflict()
        {
            var user = new User("Test", "test@mail.com", "123");
            var other = new User("Other", "dup@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(false)).Returns(BuildUsers(other).BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.UpdateUserAsync(user, "New", "dup@mail.com"));
        }

        [Fact]
        public async Task UpdateUser_Should_Update_When_NoConflict()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(false)).Returns(BuildUsers().BuildMock());

            var result = await _userManagement.UpdateUserAsync(user, "Updated", "new@mail.com");

            Assert.Equal("Updated", result.FullName);
            Assert.Equal("new@mail.com", result.Email);
        }

        [Fact]
        public async Task DeleteUser_Should_Throw_When_User_Null()
        {
            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.DeleteUserAsync(null));
        }

        [Fact]
        public async Task DeleteUser_Should_Call_Delete_When_User_Exists()
        {
            var user = new User("Test", "test@mail.com", "123");

            var result = await _userManagement.DeleteUserAsync(user);

            Assert.True(result.IsDeleted);
        }

        [Fact]
        public async Task CompleteCreate_Should_Throw_When_AlreadyExists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers(user).BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.CompleteCreateAsync(user));
        }

        [Fact]
        public async Task CompleteCreate_Should_Add_When_NotExists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers().BuildMock());
            _userRepoMock.Setup(r => r.AddAsync(user, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(user);

            var result = await _userManagement.CompleteCreateAsync(user);

            _userRepoMock.Verify(r => r.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task CompleteUpdate_Should_Throw_When_NotExists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers().BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.CompleteUpdateAsync(user));
        }

        [Fact]
        public async Task CompleteUpdate_Should_Call_Update_When_Exists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers(user).BuildMock());
            _userRepoMock.Setup(r => r.UpdateAsync(user, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(user);

            var result = await _userManagement.CompleteUpdateAsync(user);

            _userRepoMock.Verify(r => r.UpdateAsync(user, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task CompleteDelete_Should_Throw_When_NotExists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers().BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _userManagement.CompleteDeleteAsync(user));
        }

        [Fact]
        public async Task CompleteDelete_Should_Call_Delete_When_Exists()
        {
            var user = new User("Test", "test@mail.com", "123");
            _userRepoMock.Setup(r => r.GetAll(true)).Returns(BuildUsers(user).BuildMock());
            _userRepoMock.Setup(r => r.DeleteAsync(user, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(user);

            var result = await _userManagement.CompleteDeleteAsync(user);

            _userRepoMock.Verify(r => r.DeleteAsync(user, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(user, result);
        }
    }
}
