using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Serilog;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .WriteTo.Console()
    .Enrich.FromLogContext());

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and Consul
builder.Services
    .AddOcelot()
    .AddConsul();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOcelot().Wait();

app.Run();