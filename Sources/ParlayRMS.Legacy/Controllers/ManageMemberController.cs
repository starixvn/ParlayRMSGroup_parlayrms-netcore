namespace ParlayRMS.Legacy.Controllers;

using System.Linq;
using ExcelService.Writers;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ParlayRMS.Models.Functions;
using Services;
using Utilities;
using ViewModel.ManageMember;
using X.PagedList.Extensions;

public class ManageMemberController(
    INapSession napSession,
    INapSettings napSettings,
    IUserService _userService,
    MembersService _customerService,
    IHttpContextAccessor accessor
) : BaseController
{
    private readonly int _pageSize = AppSetting.PageSize;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.HasWritePermission = _userService.IsHavingPermission(
            napSession.UserId,
            napSettings.SiteId,
            FunctionKeys.ManageMember,
            Constant.Policy.Host);
    }

    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Read)]
    public ActionResult MemberList(MemberListModel model)
    {
        var specialMemberList = _customerService.GetListCustomer(model.UserName, model.Level);
        var page = model.Page ?? 1;
        ViewBag.Pager = Pager.Items(specialMemberList.Count()).PerPage(AppSetting.PageSize).Move(page).Segment(5).Center();
        return View(specialMemberList.ToPagedList(page, _pageSize));
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Host)]
    public ActionResult AddMember()
    {
        var model = new AddMemberModel();
        return PartialView(model);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Host)]
    public ActionResult AddMember(AddMemberModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }
        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = _customerService.AddCustomer(model.UserName, model.Level, napSession.UserId, ip);
        return Json(result);
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Host)]
    public ActionResult UpdateLevel(UpdateLevelModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = _customerService.UpdateLevelCustomer(model.CustId, model.Level, napSession.UserId, ip);
        return Json(result);
    }

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Read)]
    public ActionResult ExportExcel(ExportExcelModel model)
    {
        var specialMemberList = _customerService.GetListCustomer(model.UserName, model.Level);
        var templatePath = "ExcelTemplates/SpecialMember.xls";
        var writer = new TemplateWriter(templatePath);

        const string title = "Special Members";
        writer.WriteRow(0, new object[] { title });

        var header = new[]
        {
            "No",
            ViewLang.Site,
            ViewLang.Username,
            ViewLang.Level
        };
        writer.WriteRow(1, header);

        var select = specialMemberList.Select(x => new
        {
            x.No,
            site = x.Site,
            userName = x.Username,
            level = x.Level == 0 ? ViewLang.None : ViewLang.Level + " " + x.Level
        });
        writer.WriteRows(2, select);
        writer.SyncColumnWidths(4);

        var ms = writer.Render();
        return File(ms, "application/vnd.ms-excel", "SpecialMember.xls");
    }

    [HttpPost]
    [NapAuth(FunctionKey = FunctionKeys.ManageMember, Policy = Constant.Policy.Host)]
    public ActionResult ChangeLevel(ChangeLevelModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }
        var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var result = _customerService.ChangeLevelCustomer(model.CustId, model.UserName, model.Level, napSession.UserId, ip);
        return Json(result);
    }
}