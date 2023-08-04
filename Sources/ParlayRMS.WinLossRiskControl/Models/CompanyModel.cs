namespace ParlayRMS.WinLossRiskControl.Models;

public class CompanyModel
{
	public string Site { get; set; }

	public bool IsDeposit { get; set; }

	public bool Checked { get; set; } = true;
}