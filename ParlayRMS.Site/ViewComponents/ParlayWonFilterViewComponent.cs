namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParlayRMS.Legacy.Utilities;
using Legacy;
using Legacy.Services;

public class ParlayWonFilterViewComponent(CommonService commonService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var levels = new List<SelectListItem> { new() { Text = ViewLang.None, Value = "0" } };
        for (var i = 1; i <= 10; i++)
        {
            levels.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        }
        ViewBag.Levels = levels;

        var currenciesList = commonService.GetCurrencies();
        var currencies = currenciesList.Select(currency => new SelectListItem { Text = currency.CurrencyCode, Value = currency.Id.ToString() }).ToList();
        ViewBag.Currencies = currencies;

        ViewBag.HouseLoseAmountFrom = Formatter.FormatNumber3(AppSetting.HouseLoseAmountFrom);
        ViewBag.HouseLoseAmountTo = Formatter.FormatNumber3(AppSetting.HouseLoseAmountTo);

        return View("Default");
    }
}