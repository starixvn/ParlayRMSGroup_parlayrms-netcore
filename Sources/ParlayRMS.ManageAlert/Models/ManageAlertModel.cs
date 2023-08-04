namespace ParlayRMS.ManageAlert.Models;

public class ManageAlertModel
{
	public string AlertId { get; set; }

	public DateTime AlertTime { get; set; }

	public string? BetId { get; set; }

	public string RefNo { get; set; }

	public int CustId { get; set; }

	public int BetType { get; set; }

	public required string AlertType { get; set; }

	public string? SportType { get; set; }

	public string? UserName { get; set; }

	public string? SimilarTickets { get; set; }

	public required string Selection { get; set; }

	public decimal Odds { get; set; }

	public string? TotalBets { get; set; }

	public decimal MemberStake { get; set; }

	public decimal PotentialWinning { get; set;}

	public decimal CompanyLose { get; set; }

	public string? CheckedBy { get; set; }

	public required string Action { get; set; }
}

public class ManageAlertViewModel
{
	public IEnumerable<ManageAlertModel> Items { get; set; }

	public int TotalItems { get; set; }
}