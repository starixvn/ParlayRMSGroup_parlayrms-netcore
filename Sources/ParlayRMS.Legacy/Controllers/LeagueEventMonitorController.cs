namespace ParlayRMS.Legacy.Controllers
{
    using Fanex.NAP.Client.Attributes;
    using Fanex.NAP.Client.Helpers.Business;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using ParlayRMS.Models.Functions;

    public class LeagueEventMonitorController : BaseController
    {
        [HttpGet]
        [NapAuth(FunctionKey = FunctionKeys.LeagueEventMonitor, Policy = Constant.Policy.Read)]
        public ActionResult Index()
        {
            var aspNetCoreAuth = ConfigurationManager.AppSettings["AspNetCoreAuth"];
            ViewBag.AspNetCoreApiUrl = !string.IsNullOrEmpty(aspNetCoreAuth) ? $"{aspNetCoreAuth}/" : string.Empty;
            return View("~/wwwroot/league-event-monitor/index.cshtml");
        }
    }
}
