using Consul;
using CustomersService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer("Server=sqlserver;Database=ShopDb;User Id=sa;Password=your_password;"));

// Add Consul service registration
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(cfg =>
{
    cfg.Address = new Uri("http://consul:8500");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Register Service with Consul
var consulClient = app.Services.GetRequiredService<IConsulClient>();
var registration = new AgentServiceRegistration
{
    ID = $"{builder.Environment.ApplicationName}-{Guid.NewGuid()}",
    Name = builder.Environment.ApplicationName,
    Address = app.Configuration["ServiceConfig:Host"],
    Port = int.Parse(app.Configuration["ServiceConfig:Port"])
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();

