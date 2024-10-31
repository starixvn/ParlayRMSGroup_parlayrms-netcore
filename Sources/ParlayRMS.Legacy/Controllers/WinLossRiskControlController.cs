namespace ParlayRMS.Legacy.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Models.Functions;


public class WinLossRiskControlController(INapSession napSession) : BaseController
{
    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.WinLossRiskControl, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        ViewBag.UserId = napSession.UserId;
        return View("~/wwwroot/win-loss-risk-control/index.cshtml");
    }
}