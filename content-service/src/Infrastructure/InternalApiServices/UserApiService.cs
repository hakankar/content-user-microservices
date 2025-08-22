using Application.Abstraction;
using Application.Abstraction.InternalApiServices;
using Application.Common;
using Application.DTOs.InternalApiServices;
using Application.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.InternalApiServices
{
    public class UserApiService : IUserApiService
    {
        private readonly string _apiUrl;
        private readonly IHttpClientWrapper _httpClientWrapper;
        public UserApiService(IHttpClientWrapper httpClientWrapper, IOptions<InternalApiServicesOptions> options)
        {
            _httpClientWrapper = httpClientWrapper;
            _apiUrl = options.Value.UserApiServiceUrl;
        }

        public async Task<BaseResponse<UserDto?>> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var builder = new UriBuilder(_apiUrl);
            builder.Path = $"{builder.Path.TrimEnd('/')}/users/{userId}";

            var url = builder.Uri.ToString();
            var result = await _httpClientWrapper.GetAsync<BaseResponse<UserDto?>>(url, cancellationToken);
            return result;
        }
    }


}
