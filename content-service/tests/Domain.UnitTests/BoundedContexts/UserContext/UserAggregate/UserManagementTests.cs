using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Common;
using Domain.Entities;
using Domain.UnitTests.Common;
using Moq;

namespace Domain.UnitTests.BoundedContexts.ContentContext.ContentAggregate
{
    public class ContentManagementTests
    {
        private readonly Mock<IContentRepository> _contentRepoMock;
        private readonly ContentManagement _contentManagement;

        public ContentManagementTests()
        {
            _contentRepoMock = new Mock<IContentRepository>();
            _contentManagement = new ContentManagement(_contentRepoMock.Object);
        }

        private IQueryable<Content> BuildContents(params Content[] contents)
            => contents.AsQueryable();

        [Fact]
        public void GetAll_Should_Call_Repository()
        {
            var contents = BuildContents(new Content("Test", "testbody", Guid.NewGuid()));
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(contents.BuildMock());

            var result = _contentManagement.GetAll();
            Assert.Equal(contents, result);
        }

        [Fact]
        public async Task CreateContent_Should_Throw_When_Title_Exists()
        {
            var contents = BuildContents(new Content("Exist", "testbody", Guid.NewGuid()));
            _contentRepoMock.Setup(r => r.GetAll(false)).Returns(contents.BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.CreateContentAsync("Exist", "testbody", Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateContent_Should_Return_Content_When_NotExists()
        {
            _contentRepoMock.Setup(r => r.GetAll(false)).Returns(BuildContents().BuildMock());

            var result = await _contentManagement.CreateContentAsync("Test", "testbody", Guid.NewGuid());

            Assert.Equal("Test", result.Title);
            Assert.Equal("testbody", result.Body);
        }

        [Fact]
        public async Task UpdateContent_Should_Throw_When_Title_Conflict()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            var other = new Content("Test", "testbody2", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(false)).Returns(BuildContents(other).BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.UpdateContentAsync(content, "Test", "testbody2", Guid.NewGuid()));
        }

        [Fact]
        public async Task UpdateContent_Should_Update_When_NoConflict()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(false)).Returns(BuildContents().BuildMock());

            var result = await _contentManagement.UpdateContentAsync(content, "Updated", "testbody", Guid.NewGuid());

            Assert.Equal("Updated", result.Title);
            Assert.Equal("testbody", result.Body);
        }

        [Fact]
        public async Task DeleteContent_Should_Throw_When_Content_Null()
        {
            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.DeleteContentAsync(null));
        }

        [Fact]
        public async Task DeleteContent_Should_Call_Delete_When_Content_Exists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());

            var result = await _contentManagement.DeleteContentAsync(content);

            Assert.True(result.IsDeleted);
        }

        [Fact]
        public async Task CompleteCreate_Should_Throw_When_AlreadyExists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents(content).BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.CompleteCreateAsync(content));
        }

        [Fact]
        public async Task CompleteCreate_Should_Add_When_NotExists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents().BuildMock());
            _contentRepoMock.Setup(r => r.AddAsync(content, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(content);

            var result = await _contentManagement.CompleteCreateAsync(content);

            _contentRepoMock.Verify(r => r.AddAsync(content, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(content, result);
        }

        [Fact]
        public async Task CompleteUpdate_Should_Throw_When_NotExists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents().BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.CompleteUpdateAsync(content));
        }

        [Fact]
        public async Task CompleteUpdate_Should_Call_Update_When_Exists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents(content).BuildMock());
            _contentRepoMock.Setup(r => r.UpdateAsync(content, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(content);

            var result = await _contentManagement.CompleteUpdateAsync(content);

            _contentRepoMock.Verify(r => r.UpdateAsync(content, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(content, result);
        }

        [Fact]
        public async Task CompleteDelete_Should_Throw_When_NotExists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents().BuildMock());

            await Assert.ThrowsAsync<CustomException>(() =>
                _contentManagement.CompleteDeleteAsync(content));
        }

        [Fact]
        public async Task CompleteDelete_Should_Call_Delete_When_Exists()
        {
            var content = new Content("Test", "testbody", Guid.NewGuid());
            _contentRepoMock.Setup(r => r.GetAll(true)).Returns(BuildContents(content).BuildMock());
            _contentRepoMock.Setup(r => r.DeleteAsync(content, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(content);

            var result = await _contentManagement.CompleteDeleteAsync(content);

            _contentRepoMock.Verify(r => r.DeleteAsync(content, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(content, result);
        }
    }
}
