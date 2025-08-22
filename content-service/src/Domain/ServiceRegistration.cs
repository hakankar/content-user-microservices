using Domain.BoundedContexts.ContentContext.ContentAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceRegistration
    {
        public static void AddDomainServices(this IServiceCollection services) {

            services.AddScoped<IContentManagement, ContentManagement>();
        }
    }
}
