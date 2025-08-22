using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.BoundedContexts.UserContext.UserAggregate
{
    public interface IUserManagement
    {
        public IQueryable<User> GetAll(bool tracking = true);
        public Task<User> CreateUserAsync(string fullName, string email, string password, CancellationToken cancellationToken = default);
        public Task<User> UpdateUserAsync(User user, string fullName, string email, CancellationToken cancellationToken = default);
        public Task<User> DeleteUserAsync(User user, CancellationToken cancellationToken = default);

        public Task<User> CompleteCreateAsync(User user, CancellationToken cancellationToken = default);
        public Task<User> CompleteUpdateAsync(User user, CancellationToken cancellationToken = default);
        public Task<User> CompleteDeleteAsync(User user, CancellationToken cancellationToken = default);
    }
}
