using Domain.BoundedContexts.UserContext.UserAggregate;
using Domain.Extensions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence.Contexts;
using Persistence.Repositories;
using System.Data;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
            var connnectionString = configuration.GetConnectionString("Postgre");
            if (connnectionString == null)
                throw new NpgsqlException("ConnectionString:Postgre undefined or ConnectionString info is null or empty.");
            
            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(connnectionString);
            });

            services.AddTransient<IDbConnection>(db => new NpgsqlConnection(connnectionString));
            services.AddScoped<IApplicationDbContext,AppDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
