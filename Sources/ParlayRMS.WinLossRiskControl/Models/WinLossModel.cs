namespace ParlayRMS.WinLossRiskControl.Models;

public class WinLossModel
{
	private string username;

	public string customerClass;

    public string Username
	{
		get
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				return IsDeposit switch
				{
					true => "Licensee Group",
					false => "MBC Group",
					_ => "Grand Total"
				};
			}

			return username;
		}
		set => username = value;
	}

	public int CustId { get; set; }

	public bool? IsDeposit { get; set; }

	public int RoleId { get; set; }

	public int NoMember { get; set; }

	public int BetCount { get; set; }

	public decimal MemberWinLoss { get; set; }

	public decimal SmaWinLoss { get; set; }

	public decimal ExtraPT { get; set; }

	public decimal Commission { get; set; }

	public decimal RealIncome { get; set; }

	public decimal RealTurnover { get; set; }

	public string Site { get; set; }

	public string CustomerClass => !string.IsNullOrWhiteSpace(customerClass) ? customerClass : "None";

	public int TotalRecords { get; set; }

	public decimal Margin
    {
        get => RealTurnover > 0 ? RealIncome / RealTurnover * 100 : 0;
		set => _ = value;
    }

    public List<WinLossModel> Rows { get; set; } = [];

	public void SumTotal(WinLossModel site)
	{
		NoMember += site.NoMember;
		BetCount += site.BetCount;
		MemberWinLoss += site.MemberWinLoss;
		SmaWinLoss += site.SmaWinLoss;
		ExtraPT += site.ExtraPT;
		Commission += site.Commission;
		RealIncome += site.RealIncome;
		RealTurnover += site.RealTurnover;
		Margin = RealTurnover > 0 ? RealIncome / RealTurnover * 100 : 0;
	}
}