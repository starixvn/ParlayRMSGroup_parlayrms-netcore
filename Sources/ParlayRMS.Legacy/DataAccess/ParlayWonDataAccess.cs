namespace ParlayRMS.Legacy.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using Criteria.ParlayWon;
using DTO.ParlayWon;
using Fanex.Data.Repository;
using ParlayRMS.Legacy.Models;

public class ParlayWonDataAccess(IDynamicRepository repository)
{
    public IEnumerable<RMS_Rpt_Parlay_ParlayWon> RMS_Rpt_Parlay_ParlayWon(
        DateTime fromDate,
        DateTime toDate,
        DateTime moveBetsDay,
        string parlayTypes,
        string sportsType,
        string combos,
        string level,
        decimal houseAmountFrom,
        int sortById,
        bool isSortAsc,
        int pageIndex,
        int pageSize,
        string currency,
        byte site,
        byte liveType,
        int? customerClass,
        string ip,
        string leagueGroup,
        out int totalRows)
    {
            var criteria = new Rpt_ParlayWon_Criteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                MoveBetsDay = moveBetsDay,
                ParlayType = parlayTypes,
                Selection = combos,
                Sporttype = sportsType,
                Level = level,
                FromHouseAmt = houseAmountFrom,
                SortById = sortById,
                IsSortAsc = isSortAsc,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Currency = currency,
                Site = site,
                LiveStatus = liveType,
                CustomerClass = customerClass,
                Ip = ip,
                LeagueGroup = leagueGroup
            };

            var list = repository.Fetch<RMS_Rpt_Parlay_ParlayWon>(criteria).ToList();
            list.ForEach(l =>
            {
                l.CustomerClass = string.IsNullOrWhiteSpace(l.CustomerClass)?"None":l.CustomerClass;
                l.TotalBet = GetTotalBet(l.TotalBet, l.IsParlayGenerator, l.IsBoostBonus, l.BetType, l.ParlayType);
            });

            totalRows = criteria.TotalRows;

            return list;
        }

    private string GetTotalBet(string totalBet, bool isParlayGenerator, bool isBoostBonus, int betType, int parlayType)
    {
	        var result = totalBet;
	        if (isParlayGenerator)
	        {
		        result += " (PG)";
	        }
	        if (isBoostBonus)
	        {
		        result += " (B)";
	        }
            if (betType == (int)BetType.SingleMatchParlay)
            {
                result += " (S)";
            }
            if (parlayType == (int)ParlayType.HybridParlay)
            {
                result += " (BB+)";
            }
        return result;
        }
}