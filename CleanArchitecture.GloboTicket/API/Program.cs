using API.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

if (builder.Environment.IsDevelopment())
{
    await app.ResetDatabaseAsync();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("./swagger/index.html", permanent: false);
    return Task.FromResult(0);
});

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}