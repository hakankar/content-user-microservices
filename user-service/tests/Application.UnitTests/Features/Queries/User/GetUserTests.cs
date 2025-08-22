using Application.DTOs;
using Application.Features.Commands.User;
using Application.Features.Queries.User;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class GetUserTests
    {

        [Fact]
        public async Task Handler_Should_Return_User_When_Found()
        {
            // Arrange
            var user = new User("Test", "test@mail.com", "12345");
            var userId = user.Id;

            var users = new List<User> { user }.AsQueryable();
            

            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            IMapper mapper = config.CreateMapper();

            var handler = new GetUserHandler(mockUserManagement.Object, mapper);
            var query = new GetUserQuery { Id = userId };

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(userId, response.Data.Id);
            Assert.Equal("Test", response.Data.FullName);
            Assert.Equal("test@mail.com", response.Data.Email);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_User_Not_Found()
        {
            // Arrange
            var users = new List<User>().AsQueryable();

            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(users.BuildMock());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            IMapper mapper = config.CreateMapper();

            var handler = new GetUserHandler(mockUserManagement.Object, mapper);
            var query = new GetUserQuery { Id = Guid.NewGuid() };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal("User not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
