using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.LeagueEventControl.Epplus;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.Controllers;

[ApiController]
[Route("api/league-event-control/[controller]")]
[EnableCors("AllowOrigin")]
public class LeagueEventController(IMediator mediator) : ControllerBase
{
    [Route("league-data")]
	public async Task<IActionResult> League(LeagueDataRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("event")]
	public async Task<IActionResult> Event(EventRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("event-detail")]
	public async Task<IActionResult> EventDetail(EventDetailRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("export-excel")]
	public async Task<IActionResult> ExportExcel(ExcelRequest request)
	{
		var excelPackage = await mediator.Send(request);
		return new EpplusExcelResult(excelPackage);
	}
}