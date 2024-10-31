namespace ParlayRMS.Legacy.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Models.Functions;

public class ManageAlertController(INapSession napSession) : BaseController
{
    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageAlert, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        ViewBag.UserId = napSession.UserId;
        ViewBag.UserName = napSession.UserName;
        return View("~/wwwroot/manage-alert-letstalk/index.cshtml");
    }
}