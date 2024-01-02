namespace ParlayRMS.Legacy.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : BaseController
    {
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
