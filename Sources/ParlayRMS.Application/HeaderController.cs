using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Application.Models;
using System.Collections.Generic;

namespace ParlayRMS.Application;

[AllowAnonymous]
[ApiController]
public class HeaderController : ControllerBase
{
	[Route("api/application/header"), HttpPost]
	public ActionResult Index()
	{
		var header = new Header
		{
			Menus = new List<Menu>()
		};

		return Ok(header);
	}
}