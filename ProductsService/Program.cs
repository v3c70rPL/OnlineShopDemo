using Consul;
using MassTransit;
using MongoDB.Driver;
using ProductsService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// MongoDB Configuration
builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb://mongo:27017"));
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("ProductDb");
});

// Add Consul service registration
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(cfg =>
{
    cfg.Address = new Uri("http://consul:8500");
}));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<FetchProductDataConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("fetch-product-data-queue", e =>
        {
            e.ConfigureConsumer<FetchProductDataConsumer>(context);
        });
    });
});


builder.Services.AddSingleton<IProductService, ProductService>();

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
