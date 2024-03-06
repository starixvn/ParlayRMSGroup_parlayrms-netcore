namespace ParlayRMS.Legacy.DataAccess;

using System.Collections.Generic;
using Criteria.BetDetails;
using DTO.BetDetails;
using DTO.BetList;
using Fanex.Data.Repository;

public class BetDetailsDataAccess (IDynamicRepository repository)
{
    public IEnumerable<RMS_Parlay_TransDetails> RMS_Rpt_Parlay_Bet_Details(long transId, long refNo)
    {
            var criteria = new Rpt_Parlay_TopWinners_Bet_Details_Criteria
            {
                TransId = transId,
                RefNo = refNo
            };

            return repository.Fetch<RMS_Parlay_TransDetails>(criteria);
        }

    public BetInfo GetBetInfo(long transId)
    {
            var criteria = new Rpt_ComboParlay_TopWinner_BetInfo_Criteria
            {
                TransId = transId
            };

            return repository.Get<BetInfo>(criteria);
        }
}