using Api.Controllers;
using Application.Common;
using Application.Features.Commands.User;
using Application.Features.Queries.User;
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
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var command = new CreateUserCommand { FullName = "Test", Email = "Test", Password = "Test" };
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.CreateUserAsync(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateUserCommand { Id = id };
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.UpdateUserAsync(id, command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new BaseResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(It.Is<DeleteUserCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.DeleteUserAsync(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new GetUserResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetUserAsync(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var query = new GetUsersQuery();
            var expected = new GetUsersResponse { Status = HttpStatusCode.OK };

            _mediatorMock
                .Setup(m => m.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetUsersAsync(query) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expected.Status, result.StatusCode);
            Assert.Equal(expected, result.Value);
            _mediatorMock.Verify(m => m.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

