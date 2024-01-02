namespace ParlayRMS.Legacy.Controllers
{
    using Fanex.NAP.Client.Attributes;
    using Fanex.NAP.Client.Helpers.Business;
    using Fanex.NAP.Client.Helpers.Web;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using ParlayRMS.Models.Functions;

    public class ManageAlertController(INapSession napSession) : BaseController
    {
        [HttpGet]
        [NapAuth(FunctionKey = FunctionKeys.ManageAlert, Policy = Constant.Policy.Read)]
        public ActionResult Index()
        {
            var aspNetCoreAuth = ConfigurationManager.AppSettings["AspNetCoreAuth"];
            ViewBag.AspNetCoreApiUrl = !string.IsNullOrEmpty(aspNetCoreAuth) ? $"{aspNetCoreAuth}/" : string.Empty;
            ViewBag.WebUrl = Request.RawUrl.Replace("ManageAlert", "");
            ViewBag.UserId = napSession.UserId;
            ViewBag.UserName = napSession.UserName;
            return View("~/wwwroot/manage-alert-letstalk/index.cshtml");
        }
    }
}
