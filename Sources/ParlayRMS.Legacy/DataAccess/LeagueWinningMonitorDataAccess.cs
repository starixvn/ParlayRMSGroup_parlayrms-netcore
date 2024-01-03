namespace ParlayRMS.Legacy.DataAccess;

using System;
using System.Collections.Generic;
using Criteria.LeagueWinningMonitor;
using DTO.LeagueWinningMonitor;
using Fanex.Data;
using Fanex.Data.Repository;
using Fizzler;
using Utilities;

public class LeagueWinningMonitorDataAccess
{
    private readonly IDynamicRepository repository = new DynamicRepository();


    public IEnumerable<RmsRptLeagueWinningMonitor> GetData(
        string combos,
        string parlayTypes,
        string sportsTypes,
        string levels,
        decimal winningAmount,
        int selectTop,
        byte group,
        byte reportBy)
    {

            var criteria = new Rpt_League_Winning_Monitor_Criteria
            {
                Selection = combos,
                ParlayType = parlayTypes,
                SportType = sportsTypes,
                Level = levels,
                MaxPayout = winningAmount,
                LimitRowCount = selectTop,
                LeagueGroupId = group,
                ReportType = reportBy,
                LeagueId = 0,
                CustId = 0
            };
            return repository.Fetch<RmsRptLeagueWinningMonitor>(criteria);
        }

    public IEnumerable<RmsRptLeagueWinningMonitorDetail> GetDetailList(
        string combos,
        string parlayTypes,
        string sportsTypes,
        string levels,
        decimal winningAmount,
        int selectTop,
        byte group,
        byte reportBy,
        int leagueId,
        int custId)
    {
            var criteria = new Rpt_League_Winning_Monitor_Criteria
            {
                Selection = combos,
                ParlayType = parlayTypes,
                SportType = sportsTypes,
                Level = levels,
                MaxPayout = winningAmount,
                LimitRowCount = selectTop,
                LeagueGroupId = group,
                ReportType = reportBy,
                LeagueId = leagueId,
                CustId = custId
            };

            var detailList = repository.Fetch<RmsRptLeagueWinningMonitorDetail>(criteria).ToList();
            detailList.ForEach(d =>
            {
                var transDate = DateTime.Parse(d.TransDate).ToString("MM/dd/yyyy h:mm:ss tt");
                d.TransDate = transDate;
                d.SportTypeName = CacheRef.GetSportNameDropDown(d.SportType);
                d.Level = string.IsNullOrEmpty(d.Level) || d.Level == "0" ? "None" : d.Level;
            });
            
            return detailList;
        }
}