using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Domain.BoundedContexts.ContentContext.ContentAggregate
{
    public class ContentManagement : IContentManagement
    {
        private readonly IContentRepository _contentRepository;
        public ContentManagement(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }
        public IQueryable<Content> GetAll(bool tracking = true)
        {
            return _contentRepository.GetAll(tracking);
        }
        public async Task<Content> CreateContentAsync(string title, string body, Guid userId, CancellationToken cancellationToken = default)
        {
            var existContent = await _contentRepository.GetAll(false).AnyAsync(x => x.Title == title, cancellationToken);
            if (existContent)
                throw new CustomException("Content already added.", Enums.ExceptionType.Conflict);

            var content = new Content(title, body, userId);
            return content;
        }
        public async Task<Content> UpdateContentAsync(Content content, string title, string body, Guid userId, CancellationToken cancellationToken = default)
        {
            var existContent = await _contentRepository.GetAll(false).AnyAsync(x => x.Id != content.Id && x.Title == title, cancellationToken);
            if (existContent)
                throw new CustomException("Title already used.", Enums.ExceptionType.Conflict);

            content.Update(title, body, userId);
            return content;
        }
        public async Task<Content> DeleteContentAsync(Content content, CancellationToken cancellationToken = default)
        {
            if (content == null)
                throw new CustomException("Content not found.", Enums.ExceptionType.NotFound);

            content.Delete();
            return await Task.FromResult(content);
        }


        public async Task<Content> CompleteCreateAsync(Content content, CancellationToken cancellationToken = default)
        {
            var existContent = await GetAll().AnyAsync(x => x.Id == content.Id, cancellationToken);
            if (existContent)
                throw new CustomException("Content already added.", Enums.ExceptionType.Conflict);

            await _contentRepository.AddAsync(content, cancellationToken);
            return content;

        }

        public async Task<Content> CompleteUpdateAsync(Content content, CancellationToken cancellationToken = default)
        {
            var existContent = await GetAll().AnyAsync(x => x.Id == content.Id, cancellationToken);
            if (!existContent)
                throw new CustomException("Content not found.", Enums.ExceptionType.NotFound);

            await _contentRepository.UpdateAsync(content, cancellationToken);
            return content;

        }

        public async Task<Content> CompleteDeleteAsync(Content content, CancellationToken cancellationToken = default)
        {
            var existContent = await GetAll().AnyAsync(x => x.Id == content.Id, cancellationToken);
            if (!existContent)
                throw new CustomException("Content not found.", Enums.ExceptionType.NotFound);

            await _contentRepository.DeleteAsync(content, cancellationToken);
            return content;

        }

    }
}
