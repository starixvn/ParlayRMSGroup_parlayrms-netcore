namespace ParlayRMS.Site.ViewComponents;

using Legacy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Models;
using ParlayRMS.Legacy.Utilities;
using Legacy;

public class LeagueWinningMonitorFilterViewComponent : ViewComponent
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
                Selected = i <= 6
            });
        }

        combos.Add(new SelectListItem { Text = ViewLang.TenUp, Value = "10" });
        ViewBag.Combos = combos;

        var parlayTypes = new List<SelectListItem>
        {
            new() { Text = ViewLang.MixparLay, Value = ((int)ParlayType.MixParlay).ToString() },
            new() { Text = ViewLang.ParlayGenerator, Value = ((int)ParlayType.ParlayGenerator).ToString() },
            new() { Text = ViewLang.ComboParlay, Value = ((int)ParlayType.ComboParlay).ToString() },
            new() { Text = ViewLang.SingleMatchParlay, Value = ((int)ParlayType.SingleMatchParlay).ToString() },
            new() { Text = ViewLang.HybridParlay, Value = ((int)ParlayType.HybridParlay).ToString() }
        };
        ViewBag.ParlayTypes = parlayTypes;

        var groups = new List<SelectListItem>();
        groups.AddRange(Config.LeagueGroups);
        groups.Add(new SelectListItem { Text = "Fixed Max Payout", Value = "99", Selected = true });
        ViewBag.Groups = groups;

        var levels = new List<SelectListItem>
        {
            new() { Text = ViewLang.None, Value = "0", Selected = true }
        };
        for (var i = 1; i <= 10; i++)
        {
            levels.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = true });
        }

        ViewBag.Levels = levels;

        var values = AppSetting.SelectTop;
        var selectTops = values.Select(item => new SelectListItem { Text = item, Value = item, Selected = item == "200" }).ToList();
        selectTops.Add(new SelectListItem { Text = "2000", Value = "2000" });
        selectTops.Add(new SelectListItem { Text = "3000", Value = "3000" });
        ViewBag.SelectTops = selectTops;
        ViewBag.WinningAmount = 20000;

        var refreshTime = new List<int> { 60, 120 };
        var refreshTimes = refreshTime.Select(item => new SelectListItem { Text = item.ToString(), Value = (item * 1000).ToString(), Selected = item == 60 }).ToList();

        ViewBag.RefreshTimes = refreshTimes;

        return View("Default");
    }
}