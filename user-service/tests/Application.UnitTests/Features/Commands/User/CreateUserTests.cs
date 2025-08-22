using Application.Features.Commands.User;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class CreateUserTests
    {
        private readonly CreateUserValidator _validator = new CreateUserValidator();

        [Fact]
        public void Validator_Should_Have_Error_When_FullName_Is_Empty()
        {
            var model = new CreateUserCommand { FullName = "", Email = "test@mail.com", Password = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FullName)
                  .WithErrorMessage("Full name is required.");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new CreateUserCommand { FullName = "Test", Email = "wrong", Password = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Invalid email address format.");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Password_Is_Empty()
        {
            var model = new CreateUserCommand { FullName = "Test", Email = "test@mail.com", Password = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Validator_Should_Pass_When_Model_Is_Valid()
        {
            var model = new CreateUserCommand { FullName = "Test", Email = "test@mail.com", Password = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact]
        public async Task Handler_Should_Call_UserManagement_Methods()
        {
            // Arrange
            var user = new User("test", "test@mail.com", "12345");
            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement
                .Setup(m => m.CreateUserAsync("Test", "test@mail.com", "123", It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var handler = new CreateUserHandler(mockUserManagement.Object);
            var command = new CreateUserCommand
            {
                FullName = "Test",
                Email = "test@mail.com",
                Password = "123"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockUserManagement.Verify(m => m.CreateUserAsync("Test", "test@mail.com", "123", It.IsAny<CancellationToken>()), Times.Once);
            mockUserManagement.Verify(m => m.CompleteCreateAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
