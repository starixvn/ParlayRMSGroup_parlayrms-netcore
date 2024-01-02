namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;

public class MenuViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Default");
    }
}