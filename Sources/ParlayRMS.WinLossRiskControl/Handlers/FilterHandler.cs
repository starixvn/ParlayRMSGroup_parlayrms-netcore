using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class FilterHandler(FilterService service) : IRequestHandler<SportRequest, List<SportModel>>,
    IRequestHandler<ParlayTypeRequest, List<SelectItemModel>>,
    IRequestHandler<CompanyRequest, List<CompanyModel>>,
    IRequestHandler<ComboRequest, List<SelectItemModel>>
{
    public Task<List<SportModel>> Handle(SportRequest request, CancellationToken cancellationToken) => service.GetSports();
	public Task<List<SelectItemModel>> Handle(ParlayTypeRequest request, CancellationToken cancellationToken) => service.GetParlayTypes();
	public Task<List<CompanyModel>> Handle(CompanyRequest request, CancellationToken cancellationToken) => service.GetCompany();
	public Task<List<SelectItemModel>> Handle(ComboRequest request, CancellationToken cancellationToken) => service.GetComboes();
}