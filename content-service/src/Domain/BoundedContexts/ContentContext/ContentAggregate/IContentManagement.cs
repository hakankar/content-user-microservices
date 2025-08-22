using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.BoundedContexts.ContentContext.ContentAggregate
{
    public interface IContentManagement
    {
        public IQueryable<Content> GetAll(bool tracking = true);
        public Task<Content> CreateContentAsync(string title, string body, Guid userId, CancellationToken cancellationToken = default);
        public Task<Content> UpdateContentAsync(Content content, string title, string body, Guid userId, CancellationToken cancellationToken = default);
        public Task<Content> DeleteContentAsync(Content content, CancellationToken cancellationToken = default);


        public Task<Content> CompleteCreateAsync(Content content, CancellationToken cancellationToken = default);

        public Task<Content> CompleteUpdateAsync(Content content, CancellationToken cancellationToken = default);

        public  Task<Content> CompleteDeleteAsync(Content content, CancellationToken cancellationToken = default);
    }
}
