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
                        IRequestHandler<ParlayTypeRequest, List<SelectItemModel>>,
                        IRequestHandler<CompanyRequest, List<CompanyModel>>,
                        IRequestHandler<ComboRequest, List<SelectItemModel>>
    {
        private readonly FilterService filterService;
        public FilterHandler(FilterService filterService)
        {
            this.filterService = filterService;
        }

        public Task<List<SportModel>> Handle(SportRequest request, CancellationToken cancellationToken) => filterService.GetSports();
        public Task<List<SelectItemModel>> Handle(ParlayTypeRequest request, CancellationToken cancellationToken) => filterService.GetParlayTypes();
        public async Task<List<CompanyModel>> Handle(CompanyRequest request, CancellationToken cancellationToken) => await filterService.GetCompany();
        public Task<List<SelectItemModel>> Handle(ComboRequest request, CancellationToken cancellationToken) => filterService.GetComboes();
    }
}