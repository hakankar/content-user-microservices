using Application.Abstraction.InternalApiServices;
using Application.Common;
using Application.DTOs.InternalApiServices;
using Application.Features.Commands.Content;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class CreateContentTests
    {
        private readonly CreateContentValidator _validator = new CreateContentValidator();

        [Fact]
        public void Validator_Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new CreateContentCommand { Title = "", Body = "testbody", UserId = Guid.NewGuid().ToString() };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Title is required.");
        }


        [Fact]
        public void Validator_Should_Have_Error_When_UserId_Is_Empty()
        {
            var model = new CreateContentCommand { Title = "Test", Body = "testbody", UserId = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserId)
                  .WithErrorMessage("UserId is required.");
        }

        [Fact]
        public void Validator_Should_Pass_When_Model_Is_Valid()
        {
            var model = new CreateContentCommand { Title = "Test", Body = "testbody", UserId = Guid.NewGuid().ToString() };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact]
        public async Task Handler_Should_Call_ContentManagement_Methods()
        {
            // Arrange
            var content = new Content("test", "testbody", Guid.NewGuid());
            var userId = content.UserId;
            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement
                .Setup(m => m.CreateContentAsync("Test", "testbody", userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(content);

            var mockUserApiService = new Mock<IUserApiService>();

            mockUserApiService
                .Setup(m => m.GetUserAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => Task.FromResult(
                    new BaseResponse<UserDto?>
                    {
                        Data = new UserDto
                        {
                            Id = userId,
                            FullName = "Test User",
                            Email = "test@example.com"
                        }
                    }
                ).Result);

            var handler = new CreateContentHandler(mockContentManagement.Object, mockUserApiService.Object);
            var command = new CreateContentCommand
            {
                Title = "Test",
                Body = "testbody",
                UserId = userId.ToString()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockContentManagement.Verify(m => m.CreateContentAsync("Test", "testbody", userId, It.IsAny<CancellationToken>()), Times.Once);
            mockContentManagement.Verify(m => m.CompleteCreateAsync(content, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
