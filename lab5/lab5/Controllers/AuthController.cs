using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;

namespace Lab5.Controllers
{
    public class AuthController : Controller
    {
        private const string DefaultRedirectUri = "~/";
        private const string LoginPath = "Login";
        private const string LogoutPath = "Logout";

        [HttpGet(LoginPath)]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            var redirectUri = returnUrl ?? Url.Content(DefaultRedirectUri);

            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }

            return Challenge();
        }

        [HttpGet(LogoutPath)]
        public async Task<ActionResult> Logout([FromQuery] string returnUrl)
        {
            var redirectUri = returnUrl ?? Url.Content(DefaultRedirectUri);

            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }

            await HttpContext.SignOutAsync();

            return LocalRedirect(redirectUri);
        }
    }
}