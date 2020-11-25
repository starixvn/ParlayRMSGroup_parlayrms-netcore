using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Auth.Model;
using ParlayRMS.Auth.Services;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParlayRMS.Auth.Controllers
{
    public class PropagationAuthController : Controller
    {
        private readonly UserLoginService userLoginService;

        public PropagationAuthController(UserLoginService userLoginService)
        {
            this.userLoginService = userLoginService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string loginParameters, string redirectUrl)
        {
            var loginResult = await userLoginService.Login(loginParameters);

            if (loginResult.UserId > 0)
            {
                redirectUrl = WebUtility.UrlDecode(redirectUrl);
                await CreateAuthInfo(loginResult);

                return Redirect(redirectUrl);
            }

            return RedirectToAction("Login", "BackPropagationAuth");
        }

#if DEBUG
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FakeLogin()
        {
            var loginResult = new UserLoginResult { Username = "sa", UserId = 8 };
            await CreateAuthInfo(loginResult);

            return RedirectToAction("Index", "Home");
        }
#endif

        private async Task CreateAuthInfo(UserLoginResult loginResult)
        {
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginResult.Username),
                            new Claim(ClaimTypes.NameIdentifier, loginResult.UserId.ToString())
                        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
            await HttpContext.SignOutAsync();
            redirectUrl = WebUtility.UrlDecode(redirectUrl);
            return Redirect(redirectUrl);
        }
    }
}
