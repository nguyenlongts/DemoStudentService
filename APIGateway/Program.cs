using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(config);
var app = builder.Build();
await app.UseOcelot();
app.Run();
