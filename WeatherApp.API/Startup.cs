using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherApp.Core.Interfaces.Services;
using WeatherApp.Core.Interfaces.External;
using WeatherApp.Core.Interfaces.Infrastructure;
using WeatherApp.Core.Interfaces.Repositories;
using WeatherApp.Core.Services;
using WeatherApp.Core.Services.External;
using WeatherApp.Core.Services.Infrastructure;
using WeatherApp.Core.Repositories;
using System;

namespace WeatherApp.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Register Cache Service
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            // Register HTTP Client
            services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapService>(client =>
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/");
            });

            // Register Services
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();

            // Register Repositories
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();

            // Register Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WeatherApp API",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherApp API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}