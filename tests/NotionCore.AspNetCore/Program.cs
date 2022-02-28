using NotionCore.AspNetCore;
using NotionCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<AppDbContext>(options =>
{
    options.UseNotion("");
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();