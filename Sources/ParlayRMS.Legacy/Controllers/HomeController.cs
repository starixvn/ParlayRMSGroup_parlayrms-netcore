namespace ParlayRMS.Legacy.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    public ActionResult Index()
    {
            return View();
        }

    public ActionResult Test()
    {
            return View();
        }
}