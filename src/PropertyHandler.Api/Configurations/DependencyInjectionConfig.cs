using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Interfaces.Services;
using PropertyHandler.Core.Notifications;
using PropertyHandler.Core.Sql;
using PropertyHandler.Infra.Repository;


namespace PropertyHandler.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) => Logger.Factory.Get());


            services.AddSingleton<ISql>(new Sql(configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            return services;
        }
    }
}
