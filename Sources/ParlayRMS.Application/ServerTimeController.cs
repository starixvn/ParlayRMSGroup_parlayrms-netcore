using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Application;

[AllowAnonymous]
[ApiController]
[Route("api/application")]
public class ServerTimeController : ControllerBase
{
	[Route("today"), HttpPost, HttpGet]
	public ActionResult Today() => Ok(DateTime.Today);

	[Route("now"), HttpPost, HttpGet]
	public ActionResult Now() => Ok(DateTime.Now);

}