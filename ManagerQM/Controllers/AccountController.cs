using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        // Redirects to Google login page
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/Account/GoogleResponse"
        }, "Google");
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync("Cookies");

        var claims = result.Principal.Identities.First().Claims;
        var email = claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
        var name = claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

        // Here you can save user info in DB if not already saved
        // Example:
        // if(!_context.Users.Any(u => u.Email == email)) { ... save new user ... }

        ViewBag.Email = email;
        ViewBag.Name = name;

        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
