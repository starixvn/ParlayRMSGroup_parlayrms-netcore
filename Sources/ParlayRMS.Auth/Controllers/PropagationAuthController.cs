using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Auth.Services;
using System.Net;
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

            if (loginResult)
            {
                redirectUrl = WebUtility.UrlDecode(redirectUrl);
                return Redirect(redirectUrl);
            }

            return RedirectToAction("Login", "BackPropagationAuth");
        }
    }
}
