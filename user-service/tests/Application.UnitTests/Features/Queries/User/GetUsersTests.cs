using Application.DTOs;
using Application.Features.Queries.User;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class GetUsersTests
    {

        private readonly IMapper _mapper;

        public GetUsersTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Return_Users_When_Found()
        {
            // Arrange
            var users = new List<User>
            {
                new("test", "test@mail.com", "12345"),
                new("test2", "test2@mail.com", "12345")
            }.AsQueryable();


            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(false)).Returns(users.BuildMock());

            var handler = new GetUsersHandler(mockUserManagement.Object, _mapper);
            var query = new GetUsersQuery();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Count);
            Assert.Contains(response.Data, u => u.FullName == "test");
            Assert.Contains(response.Data, u => u.Email == "test2@mail.com");
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_When_No_Users()
        {
            // Arrange
            var users = new List<User>().AsQueryable();


            var mockUserManagement = new Mock<IUserManagement>();
            mockUserManagement.Setup(m => m.GetAll(false)).Returns(users.BuildMock());

            var handler = new GetUsersHandler(mockUserManagement.Object, _mapper);
            var query = new GetUsersQuery();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Empty(response.Data);
        }
    }
}
