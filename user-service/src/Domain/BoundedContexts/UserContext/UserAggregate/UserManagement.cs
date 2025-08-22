using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Domain.BoundedContexts.UserContext.UserAggregate
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;
        public UserManagement(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IQueryable<User> GetAll(bool tracking = true)
        {
            return _userRepository.GetAll(tracking);
        }
        public async Task<User> CreateUserAsync(string fullName, string email, string password, CancellationToken cancellationToken = default)
        {
            var existUser = await _userRepository.GetAll(false).AnyAsync(x => x.Email == email, cancellationToken);
            if (existUser)
                throw new CustomException("User already added.", Enums.ExceptionType.Conflict);

            var user = new User(fullName, email, password);
            return user;
        }
        public async Task<User> UpdateUserAsync(User user, string fullName, string email, CancellationToken cancellationToken = default)
        {
            var existUser = await _userRepository.GetAll(false).AnyAsync(x => x.Id != user.Id && x.Email == email, cancellationToken);
            if (existUser)
                throw new CustomException("Email already used.", Enums.ExceptionType.Conflict);

            user.Update(fullName, email);
            return user;
        }
        public async Task<User> DeleteUserAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new CustomException("User not found.", Enums.ExceptionType.NotFound);

            user.Delete();
            return await Task.FromResult(user);
        }


        public async Task<User> CompleteCreateAsync(User user, CancellationToken cancellationToken = default)
        {
            var existUser = await GetAll().AnyAsync(x => x.Id == user.Id, cancellationToken);
            if (existUser)
                throw new CustomException("User already added.", Enums.ExceptionType.Conflict);

            await _userRepository.AddAsync(user, cancellationToken);
            return user;
        }

        public async Task<User> CompleteUpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var existUser = await GetAll().AnyAsync(x => x.Id == user.Id, cancellationToken);
            if (!existUser)
                throw new CustomException("User not found.", Enums.ExceptionType.NotFound);

            await _userRepository.UpdateAsync(user, cancellationToken);
            return user;
        }

        public async Task<User> CompleteDeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            var existUser = await GetAll().AnyAsync(x => x.Id == user.Id, cancellationToken);
            if (!existUser)
                throw new CustomException("User not found.", Enums.ExceptionType.NotFound);

            await _userRepository.DeleteAsync(user, cancellationToken);
            return user;
        }

    }
}
