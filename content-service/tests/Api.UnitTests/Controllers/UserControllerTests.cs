using Api.Controllers;
using Application.Common;
using Application.Features.Commands.Content;
using Application.Features.Queries.Content;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace Api.UnitTests.Controllers
{
    public class ContentControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ContentController _controller;

        public ContentControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ContentController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateContentAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var command = new CreateContentCommand { Title = "Test", Body = "Test", UserId = Guid.NewGuid().ToString() };
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.CreateContentAsync(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateContentAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateContentCommand { Id = id };
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.UpdateContentAsync(id, command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteContentAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(It.Is<DeleteContentCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.DeleteContentAsync(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteContentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetContentAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new GetContentResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetContentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetContentAsync(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetContentQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetContentsAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var query = new GetContentsQuery();
            var expected = new GetContentsResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetContentsAsync(query) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

