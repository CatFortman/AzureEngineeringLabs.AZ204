using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

public class AuthorizationController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthorizationController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("OpenIddict request cannot be retrieved.");

        // 1. If user is NOT logged in → redirect to Identity login
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Challenge(
                    new AuthenticationProperties
                    {
                        RedirectUri = Request.Path + QueryString.Create(Request.Query.ToList())
                    },
                    IdentityConstants.ApplicationScheme
                );
        }

        // 2. User is logged in → load user from DB
        var user = await _userManager.GetUserAsync(User)
            ?? throw new InvalidOperationException("User not found.");

        // 3. Create a new identity for OpenIddict
        var identity = new ClaimsIdentity(
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        // Required subject claim
        identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id);

        // Optional claims
        if (!string.IsNullOrEmpty(user.Email))
        {
            identity.AddClaim(OpenIddictConstants.Claims.Email, user.Email);
        }

        var principal = new ClaimsPrincipal(identity);

        // 4. Auto-approve all requested scopes
        principal.SetScopes(request.GetScopes());

        // (Optional but recommended)
        principal.SetResources("resource_server");

        // 5. Let OpenIddict handle issuing the authorization code
        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}