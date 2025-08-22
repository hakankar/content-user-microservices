

using Application.Abstraction.InternalApiServices;
using Infrastructure.InternalApiServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services) {
            services.AddScoped<IUserApiService, UserApiService>();
        }
    }
}
