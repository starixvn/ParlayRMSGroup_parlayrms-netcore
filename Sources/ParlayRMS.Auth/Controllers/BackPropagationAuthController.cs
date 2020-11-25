using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace ParlayRMS.Auth
{
    public class BackPropagationAuthController : Controller
    {
        private readonly IConfiguration configuration;

        public BackPropagationAuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return Redirect($"{GetLegacyUrl()}/Auth/Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogOut()
        {
            return Redirect($"{GetLegacyUrl()}/Auth/LogOut");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WhoAmI()
        {
            return Content($"{Request.PathBase}");
        }

        private string GetLegacyUrl()
        {
            var applicationPath = Request.PathBase.ToString();
            applicationPath = string.IsNullOrEmpty(applicationPath) ? string.Empty : applicationPath.Replace("/", "");
            applicationPath = Regex.Replace(applicationPath, "NetCore", string.Empty, RegexOptions.IgnoreCase);

            var legacyAuthUrl = configuration.GetSection("AuthSettings:LegacyUrl").Value;
            legacyAuthUrl = string.IsNullOrEmpty(legacyAuthUrl) ? string.Empty : legacyAuthUrl.Replace("{ApplicationPath}", applicationPath);

            return legacyAuthUrl;
        }
    }
}
