using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers
{
    public class FilterHandler : IRequestHandler<SportRequest, List<SportModel>>,
                        IRequestHandler<ParlayTypeRequest, List<ParlayTypeModel>>,
                        IRequestHandler<CompanyRequest, List<CompanyModel>>
    {
        private readonly FilterService filterService;
        public FilterHandler(FilterService filterService)
        {
            this.filterService = filterService;
        }

        public Task<List<SportModel>> Handle(SportRequest request, CancellationToken cancellationToken) => filterService.GetSports();
        public Task<List<ParlayTypeModel>> Handle(ParlayTypeRequest request, CancellationToken cancellationToken) => filterService.GetParlayTypes();
        public async Task<List<CompanyModel>> Handle(CompanyRequest request, CancellationToken cancellationToken) => await filterService.GetCompany();
    }
}