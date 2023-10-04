using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Controllers;

[ApiController]
[Route("api/manage-alert/filter")]
[EnableCors("AllowOrigin")]
public class FilterController : ControllerBase
{
	private readonly FilterService filterService;
	public FilterController(FilterService filterService)
	{
		this.filterService = filterService;
	}

	[Route("sports")]
	public IActionResult GetSports()
	{
		var result = filterService.GetSportTypes();
		return Ok(result);
	}
}
