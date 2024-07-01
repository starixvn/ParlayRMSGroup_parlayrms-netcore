using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests;

public class AgentWinLossRequest : IRequest<WinLossModel>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string ParlayType { get; set; }

    public string Selection { get; set; }

    public string SportType { get; set; }

    public string Company { get; set; }

    public int CustId { get; set; }

    public int RoleId { get; set; }

    public string Site { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public byte LiveStatus { get; set; }
}