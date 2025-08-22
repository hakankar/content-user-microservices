using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ContentRepository : IContentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ContentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Content> GetAll(bool tracking = true) {
            var query = _dbContext.Contents.AsQueryable();
            if (!tracking)
                query = _dbContext.Contents.AsNoTracking();
            return query;
        }

        public async Task<Content> AddAsync(Content content, CancellationToken cancellationToken = default)
        {
            await _dbContext.Contents.AddAsync(content, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return content;
        }

        public async Task<Content> UpdateAsync(Content content, CancellationToken cancellationToken = default)
        {
            _dbContext.Contents.Update(content);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return content;
        }


        public async Task<Content> DeleteAsync(Content content, CancellationToken cancellationToken = default)
        {
            _dbContext.Contents.Remove(content);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return content;
        }
    }
}
