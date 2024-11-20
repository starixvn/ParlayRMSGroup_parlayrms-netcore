namespace ParlayRMS.Legacy.Controllers;

using DTO.ParlayMonitor;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Models.Functions;
using Services;
using ViewModel.Monitoring;
using ViewModel.PalayMonitor;

public class ParlayMonitorController(INapSession napSession,
    INapSettings napSettings,
    ParlayMonitorService _parlayMonitorService,
    IUserService _userService,
    CommonService _commonService

) : BaseController
{
    [NapAuth(FunctionKey = FunctionKeys.ParlayMonitor, Policy = Constant.Policy.Read)]
    [HttpGet]
    public ActionResult Index()
    {
        ViewBag.HasReadPermission = _userService.IsHavingPermission(
            napSession.UserId,
            napSettings.SiteId, 
            FunctionKeys.ViewIP,
            Constant.Policy.Read);
        return View();
    }

    [HttpPost]
    public ActionResult Data(DataModel model)
    {
        ModelState.Remove("Odds");
        ModelState.Remove("CustomerClass");
        ModelState.Remove("Ip");
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var isValidCustomerClass = !(model.CustomerClass.HasValue && model.CustomerClass.Value > short.MaxValue);

        var data = isValidCustomerClass ? _parlayMonitorService.GetParlayMonitorTickets(
            model.Combos,
            model.ParlayTypes,
            model.SportsTypes,
            model.Levels,
            model.StakeAfterPT,
            model.NetWinAfterPT,
            model.ReportType,
            model.SelectTop,
            model.MaxTransId,
            model.Markets,
            model.IsCornerParlay,
            model.Odds,
            model.LeagueGroups,
            model.CustomerClass,
            model.Company,
            model.Ip,
            model.LiveType,
            model.UserName) : new List<RMS_Monitor_Parlay_Betlist>();
        var maxTransId = model.MaxTransId;
        if (data.Any())
        {
            maxTransId = Math.Max(model.MaxTransId, long.Parse(data.First().TransId));
        }

        return Json(new
        {
            code = 0,
            Records = data,
            Title = string.Empty,
            MaxTransId = maxTransId.ToString()
        });
    }

    [HttpPost]
    public ActionResult CheckBy(UserTrackingCriteria criteria)
    {
        var data = _parlayMonitorService.UpdateCheckedUser(criteria.UserId, criteria.Username, criteria.TransId);
        return Json(data);
    }

    [HttpPost]
    public ActionResult Company()
    {
        var data = _commonService.GetCompanies();
        return Json(data.GroupBy(x => x.IsDeposit).Select(y => new { GroupId = y.Key, Items = y.Select(z => z.Site) }).ToList());
    }
}