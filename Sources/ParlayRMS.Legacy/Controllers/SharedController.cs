namespace ParlayRMS.Legacy.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SharedController : BaseController
    {

        public ActionResult MessageBox(string title, string content)
        {
            ViewBag.Title = title;
            ViewBag.Content = content;

            return PartialView();
        }

        public ActionResult _Pager(Pager model)
        {
            return PartialView(model);
        }
    }
}
