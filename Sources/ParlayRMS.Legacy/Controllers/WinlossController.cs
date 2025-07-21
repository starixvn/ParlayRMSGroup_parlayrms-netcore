namespace ParlayRMS.Legacy.Controllers;

using ExcelService.Writers;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using Models;
using ParlayRMS.Models.Functions;
using Services;
using Utilities;
using ViewModel.Report;
using Web;

public class WinlossController(WinlossService _winlossService) : BaseController
{
    [NapAuth(FunctionKey = FunctionKeys.Winloss, Policy = Constant.Policy.Read)]
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Data(WinlossOptions options)
    {
        if (options.FromDate > options.ToDate)
        {
            ModelState.AddModelError("FromDate", "FromDate must be less than ToDate");
        }

        var lastMonth = DateTime.Now.AddMonths(-3).Date;
        if (options.FromDate.Date < lastMonth)
        {
            ModelState.AddModelError("FromDate",
                $"FromDate must be greater or equal than {lastMonth.ToShortDateString()}");
        }

        if (!ModelState.IsValid)
        {
            return PartialView("_ErrorMessage", GetModelErrorMessage());
        }

        var model = _winlossService.GetWinloss(options);
        ViewBag.Title = options.GetTitle();
        ViewBag.FilterOption = options;
        return PartialView(model);
    }

    [HttpGet]
    public IActionResult Excel(WinlossOptions options)
    {
        var model = _winlossService.GetWinloss(options);
        var templatePath = "ExcelTemplates/Winloss.xls";
        var writer = new TemplateWriter(templatePath);

        var title = options.GetTitle();
        writer.WriteRow(0, new object[] { title });

        var header1 = new object[]
        {
            ViewLang.Date,
            options.ReportType switch
            {
                ReportType.Combo => ViewLang.Combo,
                ReportType.SportType => "Sport Type",
                _ => "Parlay Type"
            },
            ViewLang.Ticket,
            string.Empty,
            ViewLang.StakeAfterPT,
            string.Empty,
            ViewLang.WinLoss,
            string.Empty,
            ViewLang.OurPT,
            string.Empty,
            string.Empty,
            ViewLang.Margin
        };

        var header2 = new object[]
        {
            string.Empty,
            string.Empty,
            ViewLang.Won,
            ViewLang.Lose,
            ViewLang.Won,
            ViewLang.Lose,
            ViewLang.Won,
            ViewLang.Lose,
            ViewLang.Won,
            ViewLang.Lose,
            ViewLang.NetWinloss
        };

        var footer = new object[]
        {
            ViewLang.Total,
            string.Empty,
            model.Total.Tickets_Won,
            model.Total.Tickets_Lose,
            model.Total.Stake_Won,
            model.Total.Stake_Lose,
            model.Total.Member_Won,
            model.Total.Member_Lose,
            model.Total.OurPT_Won,
            model.Total.OurPT_Lose,
            model.Total.OurPT_NetWinloss,
            model.Total.Margin * 100
        };

        writer.WriteRows(1, [header1, header2]);

        foreach (var record in model.Records)
        {
            var totalRow = record.Records.Count;
            var rowData = new object[]
            {
                ViewLang.Subtotal,
                string.Empty,
                record.Total.Tickets_Won,
                record.Total.Tickets_Lose,
                record.Total.Stake_Won,
                record.Total.Stake_Lose,
                record.Total.Member_Won,
                record.Total.Member_Lose,
                record.Total.OurPT_Won,
                record.Total.OurPT_Lose,
                record.Total.OurPT_NetWinloss,
                record.Total.Margin * 100
            };

            foreach (var item in record.Records)
            {
                var values = new object[]
                {
                    item.WinlostDate,
                    options.ReportType switch
                    {
                        ReportType.Combo => item.Combo,
                        ReportType.SportType => CacheRef.GetSportNameDropDown(item.SportType),
                        _ => item.ParlayType
                    },
                    item.Tickets_Won,
                    item.Tickets_Lose,
                    item.Stake_Won,
                    item.Stake_Lose,
                    item.Member_Won,
                    item.Member_Lose,
                    item.OurPT_Won,
                    item.OurPT_Lose,
                    item.OurPT_NetWinloss,
                    item.Margin * 100
                };
                writer.WriteRow(3, values);
            }

            writer.WriteCellRange(writer.RowId - totalRow, totalRow, 0, 1, record.Records[0].WinlostDate);
            writer.WriteRow(4, rowData);
        }

        writer.WriteRow(5, footer);

        writer.SyncColumnWidths(12);

        return new TemplateWriterResult(writer, "Winloss.xls");
    }
}