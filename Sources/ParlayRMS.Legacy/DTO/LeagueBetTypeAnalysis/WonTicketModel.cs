namespace ParlayRMS.Legacy.DTO.LeagueBetTypeAnalysis;

public class WonTicketModel
{
	public DateTime TransDate { get; set; }

	public long TransId { get; set; }

	public string UserName { get; set; }

	public int CustomerClass { get; set; }

	public string CurrencyName { get; set; }

	public decimal Odds { get; set; }

	public decimal StakeAfterPT { get; set; }

	public decimal WinningAmount { get; set; }

	public decimal NetWinAfterPT { get; set; }

	public int TotalBet { get; set; }

	public int TotalSelection { get; set; }

	public short SportType { get; set; }

	public string IP { get; set; }
}
