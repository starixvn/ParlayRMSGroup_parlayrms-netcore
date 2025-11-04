namespace ParlayRMS.Legacy.ViewModel.Report;

public class BetDetailsOptions
{
    public long TransId { get; set; }

    public long OriginalTransId { get; set; }

    public long RefNo { get; set; }

    public long OriginalRefNo { get; set; }
        
    public string Username { get; set; }

    public int ParlayType { get; set; }
}