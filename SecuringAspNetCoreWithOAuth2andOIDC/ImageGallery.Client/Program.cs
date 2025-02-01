using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(configure =>
        configure.JsonSerializerOptions.PropertyNamingPolicy = null);

// clears the default claim naming mapping (e.g. name --> nameidentifier)  
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// registers the required services for access token management coming from the IdentityModel.aspnetcore library
builder.Services.AddAccessTokenManagement();

// create an HttpClient used for accessing the API
builder.Services.AddHttpClient("APIClient", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ImageGalleryAPIRoot"]);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    })
    .AddUserAccessTokenHandler(); // ensures the token is passed on each request to the API

builder.Services.AddAuthentication(options =>
    {
        // the signin scheme must match the scheme used by OpenIdConnect
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => { options.AccessDeniedPath = "/Authentication/AccessDenied"; })
    .AddOpenIdConnect( // manages the whole OIDC flow, authentication, token handling, etc.
        OpenIdConnectDefaults.AuthenticationScheme,
        options =>
        {
            // the signin scheme must match the scheme used by CookieAuthentication
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            // should be set to the URL of the IdentityServer, used to look up the metadata of the discovery document
            options.Authority = "https://localhost:5001/";

            // the client ID and secret of the client application, registered in the IdentityServer
            // ensure that the client application can make an authenticated request to the IdentityServer
            options.ClientId = "imagegalleryclient";
            options.ClientSecret = "secret";

            // Code flow (Authorization Code Flow) / Code Grant
            options.ResponseType = "code";

            // Required for grant type 'code' (Proof Key for Code Exchange)
            options.UsePkce = true;

            options.Scope.Add("openid"); // optional, done by default
            options.Scope.Add("profile"); // optional, done by default
            options.Scope.Add("roles");
            options.Scope.Add("imagegalleryapi.fullaccess");
            options.Scope.Add("country");

            // maps the 'role' claim to the 'role' claim since its not a standard claim
            options.ClaimActions.MapJsonKey("role", "role");
            options.ClaimActions.MapJsonKey("country", "country");

            // the URL where the client application will be redirected to after the authentication
            options.CallbackPath = new PathString("/signin-oidc"); // optional, done by default
            options.SignedOutCallbackPath = new PathString("/signout-callback-oidc"); // optional, done by default

            options.GetClaimsFromUserInfoEndpoint = true;

            // this allows to add / change / remove filters for claims coming from the IDP
            options.ClaimActions.Remove("aud"); // remove the audience claim filter (so 'aud' is added to the claims)
            options.ClaimActions.DeleteClaim("sid"); // remove the session ID claim
            options.ClaimActions.DeleteClaim("idp"); // remove the IDP claim

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                RoleClaimType = "role",
                NameClaimType = "given_name"
            };

            options.SaveTokens = true;
        });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("CanAddImage", ImageGallery.Authorization.AuthorizationPolicies.CanAddImage()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();