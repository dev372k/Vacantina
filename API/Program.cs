using API.Extensions;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.OAuth;
using Serilog;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ServicesRegistry(builder.Configuration);

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Warning()
    .WriteTo.File(MiscellaneousConstants._logpath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(MiscellaneousConstants._policy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
