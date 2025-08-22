using Application.Features.Commands.Content;
using Application.UnitTests.Common;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class DeleteContentTests
    {
        [Fact]
        public async Task Handler_Should_Delete_Content_When_Found()
        {
            // Arrange

            var content = new Content("test", "testbody", Guid.NewGuid());
            var userId = content.Id;

            var contents = new List<Content> { content }.AsQueryable();


            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());

            var handler = new DeleteContentHandler(mockContentManagement.Object);
            var command = new DeleteContentCommand { Id = userId };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            mockContentManagement.Verify(m => m.CompleteDeleteAsync(content, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_Content_Not_Found()
        {
            // Arrange
            var contents = new List<Content>().AsQueryable();

            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());

            var handler = new DeleteContentHandler(mockContentManagement.Object);
            var command = new DeleteContentCommand { Id = Guid.NewGuid() };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Content not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
