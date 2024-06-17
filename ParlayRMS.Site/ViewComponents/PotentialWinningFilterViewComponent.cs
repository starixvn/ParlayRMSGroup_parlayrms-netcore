using ParlayRMS.Legacy.ViewModel;

namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Models;
using ParlayRMS.Legacy.Utilities;
using Legacy;

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
        ViewBag.ParlayTypes = Config.ParlayTypes;

        var levels = new List<SelectListItem> { new() { Text = ViewLang.None, Value = "0" } };
        for (var i = 1; i <= 10; i++)
        {
            levels.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }

        ViewBag.Levels = levels;

        var values = AppSetting.SelectTop;
        var selectTops = values.Select(item => new SelectListItem { Text = item, Value = item, Selected = item == "100" }).ToList();

        ViewBag.SelectTops = selectTops;
        ViewBag.WinningAmount = 20000;

        var refreshTime = new List<int> { 60, 120 };
        var refreshTimes = refreshTime.Select(item => new SelectListItem { Text = item.ToString(), Value = (item * 1000).ToString(), Selected = item == 60 }).ToList();

        ViewBag.RefreshTimes = refreshTimes;

        return View("Default");
    }
}