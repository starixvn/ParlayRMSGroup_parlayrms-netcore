
namespace ParlayRMS.Legacy.Controllers;

using ExcelService.Writers;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Models.Functions;
using Services;
using System;
using System.Collections.Generic;
using Utilities;
using ViewModel.Report;
using X.PagedList;

public class LeagueBetTypeAnalysisController(LeaguesService leagueService, LeagueBetTypeAnalysisService service) : BaseController
{
    private const int SOCCER = 1;

    [HttpGet]
    [NapAuth(FunctionKey = FunctionKeys.LeagueBetTypeDataAnalysis, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult GroupsAndLeagues(int sportType, DateTime fromDate, DateTime toDate)
    {
        var leagueGroups = service.GetLeagueGroups(sportType);
        var leagueList = service.GetListLeague("", -2, sportType, fromDate, toDate);    
        return Json(new
        {
            Groups = leagueGroups,
            Leagues = leagueList
        });
    }

    [HttpPost]
    public ActionResult GetBettypes(int sportType)
    {
	    var bettypes = service.GetBettypes(sportType);
	    return Json(bettypes);
    }

	[HttpPost]
    public ActionResult Data(LeagueBetTypeAnalysisOptions options)
    {
        try
        {
            var model = service.GetLeagueBetTypeList(options);
            ViewBag.FilterOption = options;
            var page = options.Page <= 0 ? 1 : options.Page;
            ViewBag.FilterOption = options;
            ViewBag.Title = options.GetTitle();
            ViewBag.TotalRows = model.Count;
            ViewBag.Pager = Pager.Items(model.Count).PerPage(options.PageSize).Move(page).Segment(5).Center();
            var pageSizesValues = new List<int> { 100, 200, 500 };
            var pageSizes = new List<SelectListItem>();
            foreach (var item in pageSizesValues)
            {
                pageSizes.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString(), Selected = item == options.PageSize });
            }

            ViewBag.PageSizes = pageSizes;
            return PartialView(model.ToPagedList(page, options.PageSize));
        }
        catch (Exception ex)
        {
            return PartialView("_ErrorMessage", ex.Message);
        }
    }

    [HttpPost]
    public ActionResult ExportExcelForBetType(LeagueBetTypeAnalysisOptions options)
    {
        var records = service.GetLeagueBetTypeList(options);

        var templatePath = "ExcelTemplates/LeagueBetTypeAnalysis/BetTypeDataAnalysis.xls";
        var writer = new TemplateWriter(templatePath);

        var title = "BetType Report";
        writer.WriteRow(0, new object[] { title });

        var header = new object[]
        {
            ViewLang.No,
            ViewLang.Bettype,
            ViewLang.TotalTickets,
            ViewLang.WonTickets,
            ViewLang.DrawTickets,
            ViewLang.Ratio,
            ViewLang.ParlayWonTickets,
            ViewLang.Turnover,
            ViewLang.MemberWinLoss,
            ViewLang.HouseWinLoss
        };

        writer.WriteRow(1, header);

        var select = records.Select(x => new
        {
            x.No,
            x.BetType,
            TotalTickets = Formatter.FormatNumber3(x.TotalTickets),
            WonTickets = Formatter.FormatNumber3(x.WonTickets),
            DrawTickets = Formatter.FormatNumber3(x.DrawTickets),
            x.Ratio,
            ParlayWonTickets = Formatter.FormatNumber3(x.ParlayWonTickets),
            Turnover = x.TurnOver.ToString("#,##0.00"),
            MemberWinLoss = (Math.Floor(x.MemberWinloss * 100) / 100).ToString("#,##0.00"),
            HouseWinLoss = (Math.Floor(x.MainHouseWinloss * 100) / 100).ToString("#,##0.00")
        });

        writer.WriteRows(2, select);

        writer.SyncColumnWidths(10);

        var ms = writer.Render();
        
        return File(ms, "application/vnd.ms-excel", "LeagueBetTypeDataAnalysis.xls");
    }

    [HttpPost]
    public ActionResult ExportExcelForLeague(LeagueBetTypeAnalysisOptions options)
    {
        var records = service.GetLeagueBetTypeList(options);

        var templatePath = "ExcelTemplates/LeagueBetTypeAnalysis/LeagueDataAnalysis.xls";
        var writer = new TemplateWriter(templatePath);

        var title = ViewLang.LeagueReport;
        writer.WriteRow(0, new object[] { title });

        var header = new object[]
        {
            ViewLang.No,
            ViewLang.Group,
            ViewLang.LeagueName,
            ViewLang.TotalTickets,
            ViewLang.WonTickets,
            ViewLang.DrawTickets,
            ViewLang.Ratio,
            ViewLang.ParlayWonTickets,
            ViewLang.Turnover,
            ViewLang.MemberWinLoss,
            ViewLang.HouseWinLoss
        };

        writer.WriteRow(1, header);

        var select = records.Select(x => new
        {
            x.No,
            Group = x.GroupName,
            x.League,
            TotalTickets = Formatter.FormatNumber3(x.TotalTickets),
            WonTickets = Formatter.FormatNumber3(x.WonTickets),
            DrawTickets = Formatter.FormatNumber3(x.DrawTickets),
            x.Ratio,
            ParlayWonTickets = Formatter.FormatNumber3(x.ParlayWonTickets),
            Turnover = x.TurnOver.ToString("#,##0.00"),
            MemberWinLoss = (Math.Floor(x.MemberWinloss * 100) / 100).ToString("#,##0.00"),
            HouseWinLoss = (Math.Floor(x.MainHouseWinloss * 100) / 100).ToString("#,##0.00")
        });

        writer.WriteRows(2, select);

        writer.SyncColumnWidths(11);

        var ms = writer.Render();
        return File(ms, "application/vnd.ms-excel", "LeagueBetTypeDataAnalysis.xls");
    }

    [HttpPost]
    public ActionResult ExportExcelForLeagueDetails(LeagueBetTypeAnalysisOptions options)
    {
        var records = service.GetLeagueBetTypeList(options);

        var templatePath = "ExcelTemplates/LeagueBetTypeAnalysis/LeagueDetail.xls";
        var writer = new TemplateWriter(templatePath);

        var title = !string.IsNullOrEmpty(options.LeagueTitle) ? $"League Details Report of {options.LeagueTitle}" : "League Details Report";
        writer.WriteRow(0, new object[] { title });

        var header = new object[]
        {
            ViewLang.No,
            ViewLang.Event,
            ViewLang.Bettype,
            ViewLang.BetChoice,
            ViewLang.TotalTickets,
            ViewLang.WonTickets,
            ViewLang.DrawTickets,
            ViewLang.Ratio,
            ViewLang.ParlayWonTickets,
            ViewLang.Turnover,
            ViewLang.MemberWinLoss,
            ViewLang.HouseWinLoss
        };

        writer.WriteRow(1, header);

        var select = records.Select(x => new
        {
            x.No,
            x.Event,
            x.BetType,
            x.BetChoice,
            TotalTickets = Formatter.FormatNumber3(x.TotalTickets),
            WonTickets = Formatter.FormatNumber3(x.WonTickets),
            DrawTickets = Formatter.FormatNumber3(x.DrawTickets),
            x.Ratio,
            ParlayWonTickets = Formatter.FormatNumber3(x.ParlayWonTickets),
            Turnover = x.TurnOver.ToString("#,##0.00"),
            MemberWinLoss = (Math.Floor(x.MemberWinloss * 100) / 100).ToString("#,##0.00"),
            HouseWinLoss = (Math.Floor(x.MainHouseWinloss * 100) / 100).ToString("#,##0.00")
        });

        writer.WriteRows(2, select);

        writer.SyncColumnWidths(12);

        var ms = writer.Render();
        return File(ms, "application/vnd.ms-excel", "LeagueDetails.xls");
    }

    [HttpPost]
    public ActionResult ExportExcelForBetTypeDetails(LeagueBetTypeAnalysisOptions options)
    {
        var records = service.GetLeagueBetTypeList(options);

        var templatePath = "ExcelTemplates/LeagueBetTypeAnalysis/BetTypeDetail.xls";
        var writer = new TemplateWriter(templatePath);

        var title = !string.IsNullOrEmpty(options.BetTypeTitle) ? $"BetType Details Report of {options.BetTypeTitle}" : "BetType Details Report";
        writer.WriteRow(0, new object[] { title });

        var header = new object[]
        {
            ViewLang.No,
            ViewLang.Group,
            ViewLang.LeagueName,
            ViewLang.Event,
            ViewLang.TotalTickets,
            ViewLang.WonTickets,
            ViewLang.DrawTickets,
            ViewLang.Ratio,
            ViewLang.ParlayWonTickets,
            ViewLang.Turnover,
            ViewLang.MemberWinLoss,
            ViewLang.HouseWinLoss
        };

        writer.WriteRow(1, header);

        var select = records.Select(x => new
        {
            x.No,
            x.GroupName,
            x.LeagueName,
            x.Event,
            TotalTickets = Formatter.FormatNumber3(x.TotalTickets),
            WonTickets = Formatter.FormatNumber3(x.WonTickets),
            DrawTickets = Formatter.FormatNumber3(x.DrawTickets),
            x.Ratio,
            ParlayWonTickets = Formatter.FormatNumber3(x.ParlayWonTickets),
            Turnover = x.TurnOver.ToString("#,##0.00"),
            MemberWinLoss = (Math.Floor(x.MemberWinloss * 100) / 100).ToString("#,##0.00"),
            HouseWinLoss = (Math.Floor(x.MainHouseWinloss * 100) / 100).ToString("#,##0.00")
        });

        writer.WriteRows(2, select);

        writer.SyncColumnWidths(12);

        var ms = writer.Render();
        return File(ms, "application/vnd.ms-excel", "BetTypeDetails.xls");
    }

    [HttpPost]
    public ActionResult ExportExcelForWonTickets(LeagueBetTypeAnalysisDetailOptions options)
    {
	    var records = service.GetWonTickets(options);

	    var templatePath = "ExcelTemplates/LeagueBetTypeAnalysis/ParlayWonTickets.xls";
	    var writer = new TemplateWriter(templatePath);

	    writer.WriteRow(0, new object[] { options.Title });

	    var header = new object[]
	    {
		    ViewLang.No,
		    ViewLang.TransTime,
		    ViewLang.BetID,
		    ViewLang.Username,
		    ViewLang.CustomerClass,
		    ViewLang.Currency,
		    ViewLang.Odds,
		    ViewLang.AverageStakeAfterPT,
		    ViewLang.WinningAmount,
		    ViewLang.NetWinAfterPT,
		    ViewLang.TotalComboBets,
		    ViewLang.TotalSelections,
            ViewLang.SportType,
            ViewLang.IP
	    };

	    writer.WriteRow(1, header);
	    var index = 0;
	    var select = records.Select(x => new
	    {
		    No = ++index,
		    TransDate = x.TransDate.ToString(""),
		    BetID = x.TransId.ToString(),
		    x.UserName,
		    CustomerClass = x.CustomerClass == null ? "None" : x.CustomerClass.ToString(),
		    x.CurrencyName,
		    x.Odds,
		    x.StakeAfterPT,
		    x.WinningAmount,
            x.NetWinAfterPT,
		    x.TotalBet,
            x.TotalSelection,
            SportType = CacheRef.GetSportNameDropDown(x.SportType),
            x.IP
		});

	    writer.WriteRows(2, select);

	    writer.SyncColumnWidths(12);

	    var ms = writer.Render();
	    return File(ms, "application/vnd.ms-excel", "ParlayWonTickets.xls");
    }
}