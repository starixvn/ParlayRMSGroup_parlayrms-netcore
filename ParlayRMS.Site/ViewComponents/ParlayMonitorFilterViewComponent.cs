namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Models;
using ParlayRMS.Legacy.Utilities;
using Legacy;

public class ParlayMonitorFilterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var combos = new List<SelectListItem>();
        for (var i = 1; i <= 9; i++)
        {
            combos.Add(new SelectListItem
            {
                Text = i.ToString(),
                Value = i.ToString(),
                Selected = i >= 1 && i <= 6
            });
        }

        combos.Add(new SelectListItem { Text = ViewLang.TenUp, Value = "10" });
        ViewBag.Combos = combos;

        var parlayTypes = new List<SelectListItem>
        {
            new() { Text = ViewLang.MixparLay, Value = ((int)ParlayType.MixParlay).ToString(), Selected = true },
            new() { Text = ViewLang.ParlayBoost, Value = ((int)ParlayType.ParlayBoost).ToString(), Selected = true },
            new() { Text = ViewLang.ParlayGenerator, Value = ((int)ParlayType.ParlayGenerator).ToString(), Selected = true },
            new() { Text = ViewLang.ComboParlay, Value = ((int)ParlayType.ComboParlay).ToString() },
            new() { Text = ViewLang.PenaltyShootoutCombo, Value = ((int)ParlayType.PenaltyShootoutCombo).ToString() },
            new() { Text = ViewLang.SingleMatchParlay, Value = ((int)ParlayType.SingleMatchParlay).ToString() },
            new() { Text = ViewLang.HybridParlay, Value = ((int)ParlayType.HybridParlay).ToString() }
        };
        ViewBag.ParlayTypes = parlayTypes;

        var levels = new List<SelectListItem> { new() { Text = ViewLang.None, Value = "0" } };
        for (var i = 1; i <= 10; i++)
        {
            levels.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }

        ViewBag.Levels = levels;

        var values = new List<int> { 10, 20, 50, 100, 200, 1000, 2000, 3000 };
        var selectTops = values.Select(item => new SelectListItem { Text = item.ToString(), Value = item.ToString(), Selected = item == 200 }).ToList();

        ViewBag.SelectTops = selectTops;

        var markets = new List<SelectListItem>
        {
            new() { Text = ViewLang.Today_only, Value = ((int)Market.TodayOnly).ToString(), Selected = true },
            new() { Text = ViewLang.Early_only, Value = ((int)Market.EarlyOnly).ToString(), Selected = true },
            new() { Text = ViewLang.Combined, Value = ((int)Market.Combined).ToString(), Selected = true }
        };
        ViewBag.Markets = markets;

        values = [60, 120];
        var refreshTimes = values.Select(item => new SelectListItem { Text = item.ToString(), Value = (item * 1000).ToString(), Selected = item == 60 }).ToList();

        ViewBag.RefreshTimes = refreshTimes;
        ViewBag.StakeAfterPT = Formatter.FormatNumber3(AppSetting.MixParlayStakeAfterPT);
        ViewBag.NetWinStakeAfterPT = Formatter.FormatNumber3(AppSetting.NetWinStakeAfterPT);

        return View("Default");
    }
}