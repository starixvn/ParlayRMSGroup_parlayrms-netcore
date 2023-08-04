using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using System.Collections.Generic;

namespace ParlayRMS.WinLossRiskControl.Requests;

public class ComboRequest : IRequest<List<SelectItemModel>>
{
}