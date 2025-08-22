using Application.Features.Commands.Content;
using Application.UnitTests.Common;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class UpdateContentTests
    {
        private readonly UpdateContentValidator _validator = new UpdateContentValidator();

        [Fact]
        public void Validator_Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new UpdateContentCommand { Title = "", Body = "testbody", UserId = Guid.NewGuid().ToString() };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Title is required.");
        }


        [Fact]
        public void Validator_Should_Pass_When_Model_Is_Valid()
        {
            var model = new UpdateContentCommand { Title = "Test", Body = "testbody", UserId = Guid.NewGuid().ToString() };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact]
        public async Task Handler_Should_Update_Content_When_Found()
        {
            // Arrange
           
            var content = new Content("test", "testbody", Guid.NewGuid());
            var contentId = content.Id;
            var userId = content.UserId;

            var contents = new List<Content> { content }.AsQueryable();


            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());
            mockContentManagement.Setup(m => m.UpdateContentAsync(content, "Updated", "testbody", userId, It.IsAny<CancellationToken>()))
                              .ReturnsAsync(content);

            var handler = new UpdateContentHandler(mockContentManagement.Object);
            var command = new UpdateContentCommand { Id = contentId, Title = "Updated", Body = "testbody", UserId = userId.ToString() };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockContentManagement.Verify(m => m.UpdateContentAsync(content, "Updated", "testbody", userId, It.IsAny<CancellationToken>()), Times.Once);
            mockContentManagement.Verify(m => m.CompleteUpdateAsync(content, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_Content_Not_Found()
        {
            // Arrange
            var contents = new List<Content>().AsQueryable();

            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());

            var handler = new UpdateContentHandler(mockContentManagement.Object);
            var command = new UpdateContentCommand { Id = Guid.NewGuid(), Title = "Test", Body = "testbody", UserId = Guid.NewGuid().ToString() };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Content not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
