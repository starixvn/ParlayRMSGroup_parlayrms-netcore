using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests;

public class SportRequest : IRequest<List<SportModel>>
{
}