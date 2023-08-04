using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Site.Controllers;

public class HomeController : Controller
{
	[AllowAnonymous]
	public IActionResult Index()
	{
		return View();
	}
	[AllowAnonymous]
	public IActionResult ExpiredSession()
	{
		return View();

	}
}