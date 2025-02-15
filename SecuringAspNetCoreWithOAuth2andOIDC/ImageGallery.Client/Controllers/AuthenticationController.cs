using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Client.Controllers;

public class AuthenticationController : Controller
{
    [Authorize]
    public async Task Logout()
    {
        // Clears the local cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
     
        // Redirects to the IDP linked to scheme "OpenIdConnectDefault.AuthenticationScheme" (oidc)
        // so it can clear the cookie/session on the IDP side
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}