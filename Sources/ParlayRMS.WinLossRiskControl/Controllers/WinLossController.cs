namespace ParlayRMS.WinLossRiskControl.Controllers;

using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Requests;
using System.Threading.Tasks;

[ApiController]
[Route("api/win-loss-risk-control")]
[EnableCors("AllowOrigin")]
public class WinLossController(IMediator mediator) : ControllerBase
{
    [Route("get-site")]
    public async Task<IActionResult> GetSite(WinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

    [Route("company-win-loss")]
    public async Task<IActionResult> GetCompany(SuperWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

    [Route("agent-win-loss")]
    public async Task<IActionResult> GetAgent(AgentWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));
}