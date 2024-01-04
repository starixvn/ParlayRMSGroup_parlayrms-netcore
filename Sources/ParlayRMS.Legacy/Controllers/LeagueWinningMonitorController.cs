namespace ParlayRMS.Legacy.Controllers;

using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Models.Functions;
using Services;
using ViewModel.LeagueWinningMonitor;

public class LeagueWinningMonitorController(LeagueWinningMonitorService leagueWinningMonitor) : BaseController
{
    [NapAuth(FunctionKey = FunctionKeys.LeagueWinningMonitor, Policy = Constant.Policy.Read)]
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Data(LeagueWinningMonitorModel model)
    {
        ViewBag.ReportBy = model.ReportBy;
        var data = leagueWinningMonitor.GetLeagueWinningMonitorList(model.Combos, model.ParlayTypes, model.SportsTypes, model.Levels, model.WinningAmount, model.SelectTop, model.Group, model.ReportBy);

        return PartialView(data);
    }

    [HttpPost]
    public ActionResult ViewDetailList(LeagueWinningMonitorModel model)
    {
        ViewBag.Filter = model;
        var data = leagueWinningMonitor.GetLeagueWinningMonitorListDetail(model.Combos, model.ParlayTypes, model.SportsTypes, model.Levels, model.WinningAmount, model.SelectTop, model.Group, model.ReportBy, model.LeagueId, model.CustId);

        return PartialView(data);
    }
}