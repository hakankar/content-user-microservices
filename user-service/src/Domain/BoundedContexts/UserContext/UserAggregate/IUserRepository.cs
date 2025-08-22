using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BoundedContexts.UserContext.UserAggregate
{
    public interface IUserRepository: ICustomRepository<User>
    {
         IQueryable<User> GetAll(bool tracking= true);
         Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
         Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
         Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default);
    }
}
