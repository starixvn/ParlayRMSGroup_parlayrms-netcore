namespace ParlayRMS.ManageAlert.Models;

public class ViewDetailModel
{
	public string TransId { get; set; }

	public decimal Stake { get; set; }

	public decimal WinningAmt { get; set; }

	public decimal HouseAmt { get; set; }

	public string Status { get; set; }

	public string RefNo { get; set; }

	public int CustId { get; set; }

	public string UserName { get; set; }

	public int BetType { get; set; }
}

public class SimilarTicketDetailModel : ViewDetailModel
{
	public string SiteGroup { get; set; }

	public int? CustomerClass { get; set; }
}

public class PotentialWinningDetailModel : ViewDetailModel
{
	public DateTime TransDate { get; set; }

	public string TransDateString => TransDate.ToString("M/d/yyyy hh:mm:ss tt");

	public decimal Odds { get; set; }

	public string Ip { get; set; }

	public decimal HousePt { get; set; }

	public int TotalBet { get; set; }

	public string TotalBetString
	{
		get
		{
			var result = TotalBet.ToString();
			if (IsParlayGenerator)
			{
				result += " (PG)";
			}

			if (IsBoostBonus)
			{
				result += " (B)";
			}

			return result;
		}
	}

	public byte TotalSelection { get; set; }

	public string SportType { get; set; }

	public bool IsParlayGenerator { get; set; }

	public bool IsBoostBonus { get; set; }
}