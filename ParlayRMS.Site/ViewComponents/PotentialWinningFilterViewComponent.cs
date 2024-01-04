namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Models;
using ParlayRMS.Legacy.Utilities;
using ParlayRMS.Legacy;

public class PotentialWinningFilterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var combos = new List<SelectListItem>();
        for (var i = 2; i <= 9; i++)
        {
            combos.Add(new SelectListItem
            {
                Text = i.ToString(),
                Value = i.ToString(),
                Selected = true
            });
        }

        combos.Add(new SelectListItem { Text = ViewLang.TenUp, Value = "10", Selected = true });
        ViewBag.Combos = combos;

        var parlayTypes = new List<SelectListItem>
        {
            new SelectListItem { Text = ViewLang.MixparLay, Value = ((int)ParlayType.MixParlay).ToString() },
            new SelectListItem { Text = ViewLang.ParlayGenerator, Value = ((int)ParlayType.ParlayGenerator).ToString() },
            new SelectListItem { Text = ViewLang.ComboParlay, Value = ((int)ParlayType.ComboParlay).ToString() },
            new SelectListItem { Text = ViewLang.PenaltyShootoutCombo, Value = ((int)ParlayType.PenaltyShootoutCombo).ToString() },
            new SelectListItem { Text = ViewLang.SingleMatchParlay, Value = ((int)ParlayType.SingleMatchParlay).ToString() }
        };
        ViewBag.ParlayTypes = parlayTypes;

        var levels = new List<SelectListItem> { new SelectListItem { Text = ViewLang.None, Value = "0" } };
        for (var i = 1; i <= 10; i++)
        {
            levels.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }

        ViewBag.Levels = levels;

        var values = AppSetting.SelectTop;
        var selectTops = new List<SelectListItem>();
        foreach (var item in values)
        {
            selectTops.Add(new SelectListItem { Text = item, Value = item, Selected = item == "100" });
        }

        ViewBag.SelectTops = selectTops;
        ViewBag.WinningAmount = 20000;

        var refreshTime = new List<int> { 60, 120 };
        var refreshTimes = new List<SelectListItem>();
        foreach (var item in refreshTime)
        {
            refreshTimes.Add(new SelectListItem { Text = item.ToString(), Value = (item * 1000).ToString(), Selected = item == 60 });
        }

        ViewBag.RefreshTimes = refreshTimes;

        return View("Default");
    }
}