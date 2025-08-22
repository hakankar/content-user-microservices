using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public UserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> GetAll(bool tracking = true) {
            var query = _dbContext.Users.AsQueryable();
            if (!tracking)
                query = _dbContext.Users.AsNoTracking();
            return query;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }


        public async Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
