using Epplus.Mapper.Annotations;

namespace ParlayRMS.ManageAlert.Models;

public class SimilarTicketDetailExcelModel
{
	[Cell("A2")]
	public int No { get; set; }

	[Cell("B2")]
	public string TransId { get; set; }

	[Cell("C2")]
	public string SiteGroup { get; set; }

	[Cell("D2")]
	public string UserName { get; set; }

	[Cell("E2")]
	public int? CustomerClass { get; set; }

	[Cell("F2")]
	public decimal Stake { get; set; }

	[Cell("G2")]
	public decimal WinningAmt { get; set; }

	[Cell("H2")]
	public decimal HouseAmt { get; set; }

	[Cell("I2")]
	public string Status { get; set; }
}

public class PotentialWinningDetailExcelModel
{
	[Cell("A2")]
	public int No { get; set; }

	public DateTime TransDate { get; set; }

	[Cell("B2")]
	public string TransDateString => TransDate.ToString("M/d/yyyy hh:mm:ss tt");

	[Cell("C2")]
	public string TransId { get; set; }

	[Cell("D2")]
	public decimal Odds { get; set; }

	[Cell("E2")]
	public decimal Stake { get; set; }

	[Cell("F2")]
	public decimal WinningAmt { get; set; }

	[Cell("G2")]
	public decimal HouseAmt { get; set; }

	[Cell("H2")]
	public string Ip { get; set; }

	[Cell("I2")]
	public decimal HousePt { get; set; }

	[Cell("J2")]
	public string TotalBetString { get; set; }

	[Cell("K2")]
	public byte TotalSelection { get; set; }

	[Cell("L2")]
	public string SportType { get; set; }

	[Cell("M2")]
	public string Status { get; set; }
}
