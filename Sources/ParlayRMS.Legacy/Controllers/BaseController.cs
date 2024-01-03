namespace ParlayRMS.Legacy.Controllers;

using Criteria;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    public int GetSportTypeIdByName(string name)
    {
            int sport;
            if (name == ViewLang.Allsports)
            {
                sport = 0;
            }
            else if (name == ViewLang.Socceronly)
            {
                sport = 1;
            }
            else if (name == ViewLang.Othersports)
            {
                sport = 2;
            }
            else
            {
                sport = 3;
            }

            return sport;
        }

    protected JsonResult Json(Result result)
    {
            return base.Json(new { code = result.Code, message = result.Message });
        }

    protected JsonResult InvalidModelJson()
    {
            return Json(new { code = -1, message = GetModelErrorMessage() });
        }


    protected string GetModelErrorMessage()
    {
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    return error.ErrorMessage;
                }
            }

            return "Undefined Error";
        }

    protected string[] GetModelErrorMessages()
    {
            return ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToArray();
        }
}