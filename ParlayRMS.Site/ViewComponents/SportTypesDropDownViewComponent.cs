namespace ParlayRMS.Site.ViewComponents;

using Microsoft.AspNetCore.Mvc;

public class SportTypesDropDownViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string sportTypes)
    {
        return string.IsNullOrEmpty(sportTypes) 
            ? View("Default") 
            : View("BySportTypes", sportTypes);
    }
}