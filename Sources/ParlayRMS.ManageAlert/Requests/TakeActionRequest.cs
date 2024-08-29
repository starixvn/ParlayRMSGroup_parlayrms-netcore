using MediatR;

namespace ParlayRMS.ManageAlert.Requests;

public class TakeActionRequest : IRequest<int>
{
    public long AlertId { get; set; }

    public int UserId { get; set; }

    public bool IsChecked { get; set; }

    public byte ActionId { get; set; }
}