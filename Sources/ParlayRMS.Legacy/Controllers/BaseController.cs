namespace ParlayRMS.Legacy.Controllers;

using System.Text.Json;
using Criteria;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
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

    public override JsonResult Json(object data)
    {
        var settings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        };

        return new JsonResult(data, settings);
    }
}