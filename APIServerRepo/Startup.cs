using APIServer.Application.Core.Configs;
using APIServer.Core.Commands;
using APIServer.Core.Query;
using APIServerRepo.Swagger;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;

namespace APIServerRepo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services
                .AddControllers(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Get client IP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Cors
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.SetPreflightMaxAge(TimeSpan.FromDays(1));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });
            DIConfig.ConfigureServices(services, Configuration);
            services.AddHealthChecks();
            services
                .AddScoped<ICommandBus>(it =>
                {
                    var commandBus = new InProcessCommandBus(t => it.GetService(t) as ICommandHandler);
                    return commandBus;
                })
                .AddScoped<IQueryBus>(it =>
                {
                    var queryBus = new InProcessQueryBus(t => it.GetService(t) as IQueryHandler);
                    return queryBus;
                });

            // Add Swagger UI.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recruitment API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "apiKey"
                });

                c.OperationFilter<SwaggerAddHeaderParameter>();
            });

            services.AddMemoryCache();
            // IpRateLimiting
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimit"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAll");
            // swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            app.UseIpRateLimiting();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
