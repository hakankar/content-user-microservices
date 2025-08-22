using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Content> Contents { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
