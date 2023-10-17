using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests;

public class ComboRequest : IRequest<List<SelectItemModel>>
{
}