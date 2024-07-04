using API;
using API.Middlewares;
using Serilog;
using Shared.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Warning()
    .WriteTo.File(MiscilenousConstants.LOGPATH, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.APIServicesRegistry(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MiscilenousConstants._policy);
app.UseMiddleware<CustomMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
