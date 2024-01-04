namespace ParlayRMS.Legacy.Controllers;

using System.Linq;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ParlayRMS.Models.Functions;
using Services;
using Utilities;
using ViewModel.ManageAgent;
using X.PagedList;

public class ManageAgentController(
    INapSession napSession, 
    INapSettings napSettings, 
    IHttpContextAccessor accessor,
    IUserService _userService,
    AgentService _agentService) : BaseController
{
    private readonly int _pageSize = AppSetting.PageSize;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        ViewBag.HasWritePermission = _userService.IsHavingPermission(
            napSession.UserId,
            napSettings.SiteId,
            FunctionKeys.ManageAgent,
            Constant.Policy.Host
        );
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageAgent, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View();
    }
    
    [NapAuth(FunctionKey = FunctionKeys.ManageAgent, Policy = Constant.Policy.Read)]
    public ActionResult AgentList(AgentListModel model)
    {
        var agentList = _agentService.GetDangerousAgents(model.UserName);
        var page = model.Page ?? 1;

        ViewBag.Pager = Pager.Items(agentList.Count()).PerPage(AppSetting.PageSize).Move(page).Segment(5).Center();
        return PartialView(agentList.ToPagedList(page, _pageSize));
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageAgent, Policy = Constant.Policy.Host)]
    public ActionResult AddAgent()
    {
        var model = new AddAgentModel();
        return PartialView(model);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageAgent, Policy = Constant.Policy.Host)]
    public ActionResult AddAgent(AddAgentModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = _agentService.AddDangerousAgent(model.UserName, napSession.UserId,ip );
        return Json(result);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageAgent, Policy = Constant.Policy.Host)]
    public ActionResult DeleteAgent(DeleteAgentModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }
        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = _agentService.DeleteDangerousAgent(model.AgentId, model.UserName, napSession.UserId, ip);
        return Json(result);
    }
}