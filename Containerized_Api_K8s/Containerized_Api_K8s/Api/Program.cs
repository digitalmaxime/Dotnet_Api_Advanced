using Containerized_Api_K8s;
using Microsoft.EntityFrameworkCore;

// *********************************************************************************************************
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DbContextClass>
    (o => o.UseInMemoryDatabase("NET_Core_WebAPI_Kubernetes_Demo"));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *********************************************************************************************************
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContextClass>();

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", (ctx) =>
{
    ctx.Response.Redirect("/swagger");
    return Task.FromResult(0);
});

// *********************************************************************************************************
app.Run();