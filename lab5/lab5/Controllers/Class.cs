using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Lab5.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly OktaApiService _oktaApiService;

        public ProfileController(IConfiguration configuration)
        {
            _oktaApiService = new OktaApiService(configuration["Okta:apiToken"] ?? "004ZWk9BJnas6TSaJKR432NMj1-6eX-kWSzeHBszTv", configuration);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _oktaApiService.GetUserAsync(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);

            var userClaims = User.Claims ?? new List<Claim>();

            return View(user);
        }
    }
}