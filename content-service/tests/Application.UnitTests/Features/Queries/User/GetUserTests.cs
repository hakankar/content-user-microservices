using Application.DTOs;
using Application.Features.Commands.Content;
using Application.Features.Queries.Content;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Features.Commands
{
    public class GetContentTests
    {

        [Fact]
        public async Task Handler_Should_Return_Content_When_Found()
        {
            // Arrange
            var content = new Content("Test", "testbody", Guid.NewGuid());
            var userId = content.Id;

            var contents = new List<Content> { content }.AsQueryable();
            

            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Content, ContentDto>();
            });

            IMapper mapper = config.CreateMapper();

            var handler = new GetContentHandler(mockContentManagement.Object, mapper);
            var query = new GetContentQuery { Id = userId };

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(userId, response.Data.Id);
            Assert.Equal("Test", response.Data.Title);
            Assert.Equal("testbody", response.Data.Body);
        }

        [Fact]
        public async Task Handler_Should_Throw_When_Content_Not_Found()
        {
            // Arrange
            var contents = new List<Content>().AsQueryable();

            var mockContentManagement = new Mock<IContentManagement>();
            mockContentManagement.Setup(m => m.GetAll(It.IsAny<bool>())).Returns(contents.BuildMock());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Content, ContentDto>();
            });

            IMapper mapper = config.CreateMapper();

            var handler = new GetContentHandler(mockContentManagement.Object, mapper);
            var query = new GetContentQuery { Id = Guid.NewGuid() };

            // Assert
            var ex = await Assert.ThrowsAsync<CustomException>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal("Content not found.", ex.Message);
            Assert.Equal(ExceptionType.NotFound, ex.Type);
        }
    }
}
