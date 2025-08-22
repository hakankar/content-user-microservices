using Api.Common;
using Api.Filters;
using Application;
using Application.Options;
using Domain.Common;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Persistence;


namespace Api
{
    public static class AppBuilder
    {
        public static WebApplication Create(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(option =>
            {
                option.Filters.Add<ValidationExceptionFilterAttribute>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddOptions<InternalApiServicesOptions>().Bind(builder.Configuration.GetSection(InternalApiServicesOptions.Key));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();


                    if (contextFeature != null)
                    {
                        var ex = contextFeature.Error;

                        int status;
                        string message = ex.Message;

                        if (ex is CustomException customEx)
                        {
                            status = customEx.Type.ToStatusCode();
                        }
                        else
                        {
                            status = StatusCodes.Status500InternalServerError;
                            message = "Server Error";
                        }

                        context.Response.StatusCode = status;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            status,
                            error = message
                        });
                    }

                });
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
