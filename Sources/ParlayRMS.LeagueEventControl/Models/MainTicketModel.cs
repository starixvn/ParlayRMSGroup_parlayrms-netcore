namespace ParlayRMS.LeagueEventControl.Models;

public class MainTicketModel
{
    public int No { get; set; }

    public DateTime TransDate { get; set; }

    public string TransDateString { get; set; }

    public long TransId { get; set; }

    public string Username { get; set; }

    public string CustomerClass { get; set; }

    public string Currency { get; set; }

    public decimal Odds { get; set; }

    public decimal Stake { get; set; }

    public decimal PotentialWinningAmt { get; set; }

    public decimal MaxPayOut { get; set; }

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

            if (IsSMP)
            {
                result += " (S)";
            }

            return result;
        }
    }

    public int TotalSelection { get; set; }

    public int SportType { get; set; }

    public string SportTypeName { get; set; }

    public string IP { get; set; }

    public bool IsParlayGenerator { get; set; }

    public bool IsBoostBonus { get; set; }

    public bool IsSMP { get; set; }
}