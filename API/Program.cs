using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(c =>
{
    var login = "";
    var password = Uri.EscapeDataString("");
    var server = "";

    return new MongoClient($"mongodb+srv://{login}:{password}@{server}/test?retryWrites=true&w=majority");
});
builder.Services.AddScoped(c => c.GetService<IMongoClient>().StartSession());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
