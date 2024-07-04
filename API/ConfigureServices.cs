using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
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
        services.AddScoped<IUserRepo, UserRepo>();
    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddCors(opt =>
        {
            opt.AddPolicy(name: PathConstants._policy, builder =>
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
                 Title = "topE",
                 Version = "v1",
                 Description = "",
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
    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        //services.AddScoped<IFileService, FileService>();
        //services.AddScoped<ICacheService, CacheService>();
        services.AddMemoryCache();
    }
    
    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IMongoClient>(c =>
        {
            var login = "";
            var password = Uri.EscapeDataString("");
            var server = "";

            return new MongoClient($"mongodb+srv://{login}:{password}@{server}/test?retryWrites=true&w=majority");
        });

        services.AddScoped(c => c.GetService<IMongoClient>().StartSession());
    }
}
