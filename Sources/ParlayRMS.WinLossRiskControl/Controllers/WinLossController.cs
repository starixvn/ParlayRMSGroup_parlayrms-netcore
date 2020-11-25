using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.WinLossRiskControl.Requests;
using System.Threading.Tasks;

namespace ParlayRMS.WinLossRiskControl
{
    [ApiController]
    [Route("api/win-loss-risk-control")]
    public class WinLossController : ControllerBase
    {
        private readonly IMediator mediator;
        public WinLossController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("get-site")]
        public async Task<ActionResult> GetSite(WinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

        [Route("company-win-loss")]
        public async Task<ActionResult> GetCompany(CompanyWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

        [Route("agent-win-loss")]
        public async Task<ActionResult> GetAgent(AgentWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

        [Route("agent-win-loss-excel")]
        public async Task<ActionResult> ExcelAgent(AgentWinLossExcelRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

    }
}
