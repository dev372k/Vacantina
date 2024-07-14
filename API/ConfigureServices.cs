using Application.Implementations;
using Domain.Repositories;
using Domain.Repositories.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Shared;
using Shared.Commons;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace API;

public static class ConfigureServices
{
    public static void APIServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Repositories(configuration);
        services.Services(configuration);
        services.Database(configuration);
        services.Misc(configuration);
    }

    public static void Repositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICardRepo, CardRepo>();
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IBlogRepo, BlogRepo>();
        services.AddScoped<IAppRepo, AppRepo>();
    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(name: MiscilenousConstants._policy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

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
                 Title = "Vacantina",
                 Version = "v1",
                 Description = "The travel you want",
             });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration.GetSection("GoogleAuth:ClientId").Value!;
                googleOptions.ClientSecret = configuration.GetSection("GoogleAuth:ClientSecret").Value!;
            })
            .AddJwtBearer(options =>
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

    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStateHelper, StateHelper>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPaymentGateway, PaymentGateway>();
        services.AddMemoryCache();
    }

    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IMongoClient>(c =>
        {
            return new MongoClient(configuration.GetConnectionString("MongoDB"));
        });

        services.AddScoped(c => c.GetService<IMongoClient>().StartSession());
    }
}
