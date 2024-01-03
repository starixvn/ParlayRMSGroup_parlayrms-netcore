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
            return View("~/wwwroot/league-event-monitor/index.cshtml");
        }
    }
}
