namespace ParlayRMS.Legacy.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using Criteria.LeagueBetTypeAnalysis;
using DTO.LeagueBetTypeAnalysis;
using DTO.Leagues;
using Fanex.Data.Repository;
using Utilities;

public class LeagueBetTypeAnalysisDataAccess(IDynamicRepository repository)
{
    public List<RMS_Rpt_LeagueBettype_Analysis> RMS_Rpt_LeagueBettype_Analysis(
        DateTime fromDate,
        DateTime toDate,
        string parlayTypes,
        string leagueGroups,
        string leagues,
        int site,
        double ratio,
        int reportBy,
        int sportType)
    {
        var criteria = new Rpt_LeagueBettype_Analysis_Criteria
        {
            FromDate = fromDate,
            ToDate = toDate,
            ParlayType = parlayTypes,
            LeagueGroups = leagueGroups,
            Leagues = leagues,
            Site = site,
            Ratio = ratio,
            ReportBy = reportBy,
            SportType = sportType
        };

        var list = repository.Fetch<RMS_Rpt_LeagueBettype_Analysis>(criteria).ToList();
        var index = 1;
        list.ForEach(d =>
        {
            d.No = index++;
            d.Bettype = reportBy == 2 ? CacheRef.GetBettype(d.BetTypeId) : "";
            d.League = reportBy == 1 ? CacheRef.GetLeagueName(d.LeagueId) : "";
            d.GroupName = reportBy == 1 ? d.GroupName : string.Empty;
            d.Ratio = GetRatio(d.WonTickets, d.TotalTickets);
        });

        return list;
    }

    private string GetRatio(int wonTickets, int totalTickets)
    {
        if (wonTickets == 0 || totalTickets == 0)
        {
            return "0%";
        }
        var ratio = (double)wonTickets / totalTickets * 100;
        return Math.Round(ratio) + "%";
    }

    public IEnumerable<LeagueGroup> GetLeagueGroups(int sportType)
    {
        var criteria = new LeagueByGroupSel_Criteria { SportType = sportType };
        return repository.Fetch<LeagueGroup>(criteria);
    }

    public List<RMS_LeagueSel> RMS_LeagueSel(string leagueName, int groupId, int sportType, DateTime fromDate, DateTime toDate)
    {
        var criteria = new LeagueSel_Criteria
        {
            LeagueName = leagueName,
            FromDate = fromDate,
            ToDate = toDate,
            SportType = sportType,
            GroupId = groupId
        };
        var counter = 1;
        var leagues = repository.Fetch<RMS_LeagueSel>(criteria).ToList();
        leagues.ForEach(r => { r.No = counter++; });

        return leagues;
    }
}