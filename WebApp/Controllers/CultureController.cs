using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                // Ensure this name matches what ASP.NET Core Localization uses by default
                string cookieName = CookieRequestCultureProvider.DefaultCookieName;
                string cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));

                Response.Cookies.Append(
                    cookieName,
                    cookieValue,
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, Path = "/" } // Make it essential and root path
                );
                Console.WriteLine($"Culture cookie '{cookieName}' set to '{cookieValue}' for culture '{culture}'"); // Logging
            }
            else
            {
                Console.WriteLine("Culture parameter was null in CultureController."); // Logging
            }

            if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/"; // Fallback redirect
                Console.WriteLine($"Redirect URI was invalid or empty, falling back to '{redirectUri}'."); // Logging
            }
            Console.WriteLine($"Redirecting to: {redirectUri}"); // Logging
            return LocalRedirect(redirectUri);
        }
    }
}
