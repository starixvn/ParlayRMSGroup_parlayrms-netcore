namespace ParlayRMS.Legacy.DataAccess;

using Criteria.SimilarBets;
using DTO.SimilarBets;
using Fanex.Data.Repository;
using ParlayRMS.Legacy.Criteria.ParlayMonitor;
using ParlayRMS.Legacy.DTO.PotentialWinning;

public class SimilarBetsDataAccess(IDynamicRepository repository)
{
    private const byte runningStatus = 0;
    public RMS_Similar_Parlay_Betlist RMS_Similar_Parlay_Betlist(long transId, bool isSimilar)
    {
        var criteria = new Similar_ComboParlay_Betlist_Criteria
        {
            TransId = transId,
            IsSimilar = isSimilar
        };

        var data = repository.FetchMultiple<RMS_Similar_Parlay_Betlist, RMS_Similar_Parlay_Betlist_Detail>(criteria);
        if (data.Item1.Count() == 0) return null;
        var similar = data.Item1.FirstOrDefault();
        var details = data.Item2;
        var isRunningStatus = similar.StatusID == runningStatus;
        var result = new RMS_Similar_Parlay_Betlist
        {
            Transdate = similar.Transdate,
            Transid = similar.Transid,
            Refno = similar.Refno,
            CustId = similar.CustId,
            Username = similar.Username,
            Odds = similar.Odds,
            Stake = similar.Stake,
            Max_payout = similar.Max_payout,
            TotalBet = similar.TotalBet,
            TotalSelection = similar.TotalSelection,
            NumberWon = similar.NumberWon,
            Level = similar.Level,
            Cnt_bets = similar.Cnt_bets,
            SportType = similar.SportType,
            BetType = similar.BetType,
            IsGOF = similar.IsGOF,
            StatusID = similar.StatusID,
            PotentialWinningAmount = isRunningStatus ? similar.PotentialWinningAmount : 0,
            HouseLoseAmount = isRunningStatus ? 0 : similar.HouseLoseAmount,
            CustomerClass = similar.CustomerClass ?? "None",
            SiteGroup = similar.SiteGroup,
            IsParlayGenerator = similar.IsParlayGenerator,
            IsBoostBonus = similar.IsBoostBonus,
            ParlayType = similar.ParlayType,
            Details = details.Select(row => new RMS_Similar_Parlay_Betlist_Detail
            {
                TransId = row.TransId,
                Refno = row.Refno,
                Username = row.Username,
                CustId = row.CustId,
                Level = row.Level,
                Stake = row.Stake,
                Max_Payout = row.Max_Payout,
                PotentialWinningAmount = isRunningStatus ? row.PotentialWinningAmount : 0,
                HouseLoseAmount = isRunningStatus ? 0 : row.HouseLoseAmount,
                Similar_Selection = row.Similar_Selection,
                CustomerClass = row.CustomerClass ?? "None",
                SiteGroup = row.SiteGroup,
                Ip = row.Ip,
                TransDateString = similar.Transdate.ToString("MM/dd/yyyy h:mm:ss tt"),
                IsTrackedUser = !string.IsNullOrWhiteSpace(row.TrackingUser),
                TrackingUser = string.IsNullOrWhiteSpace(row.TrackingUser) ? "None" : row.TrackingUser,
                ParlayType = row.ParlayType
            })
        };
        return result;
    }

    public UserTracking RMS_Similar_Parlay_UserTracking(int userid, string username, string transid)
    {
        var command = new ComboParlay_PotentialWinning_UserTracking_Command
        {
            Username = username,
            UserId = userid,
            TransId = transid
        };

        repository.Execute(command);

        var userTracking = new UserTracking
        {
            StatusId = command.StatusId,
            Message = command.Message
        };

        return userTracking;
    }
}