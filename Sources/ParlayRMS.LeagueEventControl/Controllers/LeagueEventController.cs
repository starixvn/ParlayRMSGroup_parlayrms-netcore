using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.LeagueEventControl.Epplus;
using ParlayRMS.LeagueEventControl.Requests;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Controllers;

[ApiController]
[Route("api/league-event-control/[controller]")]
[EnableCors("AllowOrigin")]
public class LeagueEventController : ControllerBase
{
	private readonly IMediator mediator;

	public LeagueEventController(IMediator mediator)
	{
		this.mediator = mediator;
	}

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