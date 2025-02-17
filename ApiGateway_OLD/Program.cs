using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .WriteTo.Console()
    .Enrich.FromLogContext());

// Load Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and Consul
builder.Services
    .AddOcelot(builder.Configuration)
    .AddConsul(); // Register Consul with Ocelot

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

// Use Ocelot Middleware
await app.UseOcelot();

app.Run();
