using ImageGallery.API.DbContexts;
using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<GalleryContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ImageGalleryDBConnectionString"]);
});

// register the repository
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();

// register AutoMapper-related services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
 * Since .NET 8, the token handling is done by the JsonWebToken handler instead of the old JwtSecurityToken handler.
 */
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = "imagegalleryapi";
        options.Authority =
            "https://localhost:5001"; // IdentityServer URL, used for locating the signing keys and meta data
        options.TokenValidationParameters = new()
        {
            ValidTypes = new[] { "at+jwt" },
            RoleClaimType = "role",
            NameClaimType = "given_name"
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();