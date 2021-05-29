using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PropertyHandler.Api.Configurations;

namespace PropertyHandler.Api
{
    public class Startup
    {
        private readonly string _enableCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ResolveDependencies(Configuration);

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PropertyHandler.Api", Version = "v1" }));
            services.AddLogging(logging => logging.AddKissLog());
            services.AddCors(opt => opt.AddPolicy(_enableCors, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PropertyHandler.Api v1"));
            }

            app.UseKissLog(Configuration);

            app.UseHttpsRedirection();
            app.UseCors(_enableCors);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
