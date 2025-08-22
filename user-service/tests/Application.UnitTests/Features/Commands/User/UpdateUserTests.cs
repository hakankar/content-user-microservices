using Application.Features.Commands.User;
using Application.UnitTests.Common;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class UpdateUserTests
    {
        private readonly UpdateUserValidator _validator = new UpdateUserValidator();

        [Fact]
        public void Validator_Should_Have_Error_When_FullName_Is_Empty()
        {
            var model = new UpdateUserCommand { FullName = "", Email = "test@mail.com" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FullName)
                  .WithErrorMessage("Full name is required.");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new UpdateUserCommand { FullName = "Test", Email = "wrong" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Invalid email address format.");
        }

        [Fact]
        public void Validator_Should_Pass_When_Model_Is_Valid()
        {
            var model = new UpdateUserCommand { FullName = "Test", Email = "test@mail.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact]
        public async Task Handler_Should_Update_User_When_Found()
        {
            // Arrange
           
            var user = new User("test", "test@mail.com", "12345");
            var userId = user.Id;

            var users = new List<User> { user }.AsQueryable();


            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());
            mockUserManagement.Setup(m => m.UpdateUserAsync(user, "Updated", "new@mail.com", It.IsAny<CancellationToken>()))
                              .ReturnsAsync(user);

            var handler = new UpdateUserHandler(mockUserManagement.Object);
            var command = new UpdateUserCommand { Id = userId, FullName = "Updated", Email = "new@mail.com" };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockUserManagement.Verify(m => m.UpdateUserAsync(user, "Updated", "new@mail.com", It.IsAny<CancellationToken>()), Times.Once);
            mockUserManagement.Verify(m => m.CompleteUpdateAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var users = new List<User>().AsQueryable();

            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());

            var handler = new UpdateUserHandler(mockUserManagement.Object);
            var command = new UpdateUserCommand { Id = Guid.NewGuid(), FullName = "Test", Email = "test@mail.com" };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
