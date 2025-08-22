using Application.DTOs;
using Application.Features.Queries.Content;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class GetContentsTests
    {

        private readonly IMapper _mapper;

        public GetContentsTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Content, ContentDto>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Return_Contents_When_Found()
        {
            // Arrange
            var contents = new List<Content>
            {
                new("test", "testbody", Guid.NewGuid()),
                new("test2", "testbody2", Guid.NewGuid())
            }.AsQueryable();


            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(false)).Returns(contents.BuildMock());

            var handler = new GetContentsHandler(mockContentManagement.Object, _mapper);
            var query = new GetContentsQuery();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Count);
            Assert.Contains(response.Data, u => u.Title == "test");
            Assert.Contains(response.Data, u => u.Body == "testbody2");
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_When_No_Contents()
        {
            // Arrange
            var contents = new List<Content>().AsQueryable();


            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(false)).Returns(contents.BuildMock());

            var handler = new GetContentsHandler(mockContentManagement.Object, _mapper);
            var query = new GetContentsQuery();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Empty(response.Data);
        }
    }
}
