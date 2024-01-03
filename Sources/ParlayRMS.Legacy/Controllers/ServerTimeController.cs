namespace ParlayRMS.Legacy.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ServerTimeController : Controller
{
    [HttpPost]
    public ActionResult Today()
    {
        return Json(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
    }
}