using Footballito.Application;
using Footballito.Persistence;
using Footballito.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSingleton<FootballContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
 
app.Run();