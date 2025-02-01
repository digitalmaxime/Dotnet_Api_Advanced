using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MyCompany.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(), // Standard defined openid scope
            new IdentityResources.Profile(), // Standard defined openid scope
            new IdentityResource("roles", "Your role(s)", new List<string> { "role" }),
            new IdentityResource("country", "The country you live in", new List<string> { "country" })
        };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        /*
         * 3rd parameter is the list of user claims that should be included in the access token.
         * ApiRessource's 3rd parameter is an overload of the constructor that takes a list of user claims
         * that should be returned when requesting the related scope
         */
        new ApiResource("imagegalleryapi", "Image Gallery API", new[] { "role" })
        {
            Scopes = { "imagegalleryapi.fullaccess" }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("imagegalleryapi.fullaccess")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery",
                ClientId = "imagegalleryclient",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    "https://localhost:7184/signin-oidc",
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7184/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "imagegalleryapi.fullaccess",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RequireConsent = true
            }
        };
}