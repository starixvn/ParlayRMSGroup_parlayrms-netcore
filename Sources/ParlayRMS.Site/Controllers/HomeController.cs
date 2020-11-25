using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("win-loss-risk-control")]
        public IActionResult WinLossRiskControl()
        {
            return View("~/wwwroot/win-loss-risk-control/index.cshtml");
        }
    }
}