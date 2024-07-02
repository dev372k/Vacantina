using API.Models;
using Domain;
using Domain.Repositories;
using Infrastructure.Abstractions.Implementations;
using Infrastructure.Abstractions.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Constants;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            services.Repositories(configuration);
            services.Services(configuration);
            services.Database(configuration);
            services.Misc(configuration);

            return services;
        }

        public static void Repositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
        }

        public static void Misc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddMemoryCache();
            services.AddScoped<IStateHelper, StateHelper>();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.SwaggerDoc("v1",
                 new OpenApiInfo
                 {
                     Title = "LMS",
                     Version = "v1",
                     Description = "Learning Management System",
                 });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            configuration.GetSection("SecretKeys:JWT").Value!))
                };
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: MiscellaneousConstants._policy, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            services.AddHttpContextAccessor();
        }

        public static void Services(this IServiceCollection services, IConfiguration configuration)
        {
            // Scoped
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();
        }

        public static void Database(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"));
            });
        }
    }
}
