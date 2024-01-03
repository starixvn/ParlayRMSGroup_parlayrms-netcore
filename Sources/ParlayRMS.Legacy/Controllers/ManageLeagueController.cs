namespace ParlayRMS.Legacy.Controllers;

using System.Linq;
using ExcelService.Writers;
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
using ViewModel.ManageLeague;
using X.PagedList;

public class ManageLeagueController(
    INapSettings napSettings,
    INapSession napSession,
    IUserService _userService,
    LeaguesService leagueService,
    IHttpContextAccessor accessor
) : BaseController
{
    private readonly int _pageSize = AppSetting.PageSize;
    private const int SOCCER = 1;


    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Read)]
    public ActionResult LeagueList(LeagueListModel model)
    {
        var specialLeagueList = leagueService.GetListLeague(model.League, model.Group, SOCCER);
        var page = model.Page <= 0 ? 1 : model.Page;
        ViewBag.Pager = Pager.Items(specialLeagueList.Count).PerPage(AppSetting.PageSize).Move(page).Segment(5).Center();
        return View(specialLeagueList.ToPagedList(page, _pageSize));
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Host)]
    public ActionResult AddLeague()
    {
        var model = new AddLeagueModel();
        return PartialView(model);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Host)]
    public ActionResult AddLeague(AddLeagueModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }
        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = leagueService.AddLeague(model.League, model.Group, napSession.UserId, ip);
        return Json(result);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Host)]
    public ActionResult UpdateGroup(UpdateGroupModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }
        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = leagueService.UpdateLeague(model.League, model.Group, napSession.UserId, ip);
        return Json(result);
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageLeague, Policy = Constant.Policy.Read)]
    public ActionResult ExportExcel(ExportExcelModel model)
    {
        var specialLeagueList = leagueService.GetListLeague(model.League, model.Group, SOCCER);
        var templatePath = "ExcelTemplates\\SpecialLeague.xls";
        var writer = new TemplateWriter(templatePath);

        const string title = "Special Leagues";
        writer.WriteRow(0, new object[] { title });

        var header = new[]
        {
            "No",
            "League Name",
            "Group"
        };
        writer.WriteRow(1, header);

        var select = specialLeagueList.Select(x => new
        {
            x.No,
            x.LeagueName,
            x.GroupName
        });
        writer.WriteRows(2, select);
        writer.SyncColumnWidths(3);

        var ms = writer.Render("SpecialLeague");
        ms.Seek(0, 0);
        return File(ms, "application/vnd.ms-excel", "SpecialLeague.xls");
    }

    public ActionResult SearchBar(SearchBarModel model)
    {
        return PartialView(model);
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);

        ViewBag.HasWritePermission = napSession.UserId > 0 && _userService.IsHavingPermission(
            napSession.UserId, 
            napSettings.SiteId,
            FunctionKeys.ManageLeague,
            Constant.Policy.Host);
    }
}