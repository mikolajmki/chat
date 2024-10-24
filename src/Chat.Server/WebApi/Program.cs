using Application;
using Infrastructure;
using Mapster;
using Presentation.Hubs;
using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddMapster();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddMapsterConfig();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(c =>
    {
        c.AllowAnyOrigin();
        c.AllowAnyMethod();
    });
}

app.UseHttpsRedirection();

app.MapHub<ChatHub>("/api/ChatHub");

app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program { }
