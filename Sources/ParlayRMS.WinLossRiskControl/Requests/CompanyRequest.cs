using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests;

public class CompanyRequest : IRequest<List<CompanyModel>>
{
}