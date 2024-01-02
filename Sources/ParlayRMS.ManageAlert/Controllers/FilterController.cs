using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Controllers;

[ApiController]
[Route("api/manage-alert/filter")]
[EnableCors("AllowOrigin")]
public class FilterController(FilterService service) : ControllerBase
{
    [Route("sports")]
	public IActionResult GetSports()
	{
		var result = service.GetSportTypes();
		return Ok(result);
	}
}
