using Application.Features.Commands.User;
using Application.UnitTests.Common;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class DeleteUserTests
    {
        [Fact]
        public async Task Handler_Should_Delete_User_When_Found()
        {
            // Arrange

            var user = new User("test", "test@mail.com", "12345");
            var userId = user.Id;

            var users = new List<User> { user }.AsQueryable();


            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());

            var handler = new DeleteUserHandler(mockUserManagement.Object);
            var command = new DeleteUserCommand { Id = userId };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockUserManagement.Verify(m => m.CompleteDeleteAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var users = new List<User>().AsQueryable();

            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());

            var handler = new DeleteUserHandler(mockUserManagement.Object);
            var command = new DeleteUserCommand { Id = Guid.NewGuid() };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
