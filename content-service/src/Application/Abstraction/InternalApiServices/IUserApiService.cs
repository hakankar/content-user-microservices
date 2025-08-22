using Application.Common;
using Application.DTOs.InternalApiServices;

namespace Application.Abstraction.InternalApiServices
{
    public interface IUserApiService
    {
        public Task<BaseResponse<UserDto?>> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
