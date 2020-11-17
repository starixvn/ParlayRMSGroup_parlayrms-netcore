using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/wwwroot/win-loss-risk-control/index.cshtml");
        }
    }
}