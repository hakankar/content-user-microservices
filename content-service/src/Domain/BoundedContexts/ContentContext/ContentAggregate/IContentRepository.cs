using Domain.Entities;
using Domain.Interfaces;

namespace Domain.BoundedContexts.ContentContext.ContentAggregate
{
    public interface IContentRepository: ICustomRepository<Content>
    {
         IQueryable<Content> GetAll(bool tracking= true);
         Task<Content> AddAsync(Content content, CancellationToken cancellationToken = default);
         Task<Content> UpdateAsync(Content content, CancellationToken cancellationToken = default);
         Task<Content> DeleteAsync(Content content, CancellationToken cancellationToken = default);
    }
}
