using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Handlers
{
    public class FilterHandler : IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>
    {
        private readonly FilterService filterService;

        public FilterHandler(FilterService filterService)
        {
            this.filterService = filterService;
        }

        public Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken) => filterService.GetLeagues(request);
    }
}
