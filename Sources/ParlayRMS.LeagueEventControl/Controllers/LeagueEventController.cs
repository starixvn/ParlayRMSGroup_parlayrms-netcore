using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.LeagueEventControl.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Controllers
{
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
    }
}
