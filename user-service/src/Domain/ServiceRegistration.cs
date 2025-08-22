using Domain.BoundedContexts.UserContext.UserAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceRegistration
    {
        public static void AddDomainServices(this IServiceCollection services) {

            services.AddScoped<IUserManagement, UserManagement>();
        }
    }
}
