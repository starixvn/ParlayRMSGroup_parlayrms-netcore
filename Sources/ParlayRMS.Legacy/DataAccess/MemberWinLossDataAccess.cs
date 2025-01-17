namespace ParlayRMS.Legacy.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using Criteria.MemberWinLoss;
using DTO.MemberWinLoss;
using Fanex.Data.Repository;

public class MemberWinLossDataAccess(IDynamicRepository repository)
{
    public IEnumerable<RMS_Rpt_Parlay_MemberWinLoss> RMS_Rpt_Parlay_MemberWinLoss(
        DateTime fromDate,
        DateTime toDate,
        DateTime moveBetsDay,
        DateTime balanceUpDay,
        string parlayTypes,
        string sportsType,
        byte liveType,
        string combos,
        int custId,
        byte reportBy)
    {
            var criteria = new Rpt_ComboParlay_Winloss_Mem_Criteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                MoveBetsDay = moveBetsDay,
                BalanceUpDay = balanceUpDay,
                CustId = custId,
                SportType = sportsType,
                LiveStatus = liveType,
                Selection = combos,
                Parlaytype = parlayTypes,
                ReportBy = reportBy
            };

            var winlosses = repository.Fetch<RMS_Rpt_Parlay_MemberWinLoss>(criteria).ToList();

            winlosses.ForEach(w =>
            {
                w.CustomerClass = string.IsNullOrEmpty(w.CustomerClass) ? "None" : w.CustomerClass;
            });
            
            return winlosses;
        }

    public IEnumerable<RMS_Rpt_Parlay_MemberWinLoss> RMS_Rpt_Parlay_AgentWinLoss(
        DateTime fromDate,
        DateTime toDate,
        DateTime moveBetsDay,
        DateTime balanceUpDay,
        string parlayTypes,
        string sportsType,
        byte liveType,
        string combos,
        string userName,
        byte reportBy)
    {
            var criteria = new Rpt_ComboParlay_Winloss_Ag_Criteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                MoveBetsDay = moveBetsDay,
                BalanceUpDay = balanceUpDay,
                UserName = userName,
                SportType = sportsType,
                LiveStatus = liveType,
                Selection = combos,
                Parlaytype = parlayTypes,
                ReportBy = reportBy
            };

            var winlosses = repository.Fetch<RMS_Rpt_Parlay_MemberWinLoss>(criteria).ToList();

            winlosses.ForEach(w =>
            {
                w.CustomerClass = string.IsNullOrEmpty(w.CustomerClass) ? "None" : w.CustomerClass;
            });

            return winlosses;
        }

    public IEnumerable<RMS_Rpt_Parlay_MemberWinLoss_Detail> RMS_Rpt_ComboParlay_Winloss_Details(
        DateTime fromDate,
        DateTime toDate,
        DateTime moveBetsDay,
        string parlayTypes,
        string sportsType,
        byte liveType,
        string combos,
        int custId)
    {
            var criteria = new Rpt_ComboParlay_Winloss_Details_Criteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                MoveBetsDay = moveBetsDay,
                Parlaytype = parlayTypes,
                Selection = combos,
                CustId = custId,
                SportType = sportsType,
                LiveStatus = liveType
            };

            return  repository.Fetch<RMS_Rpt_Parlay_MemberWinLoss_Detail>(criteria);
        }
}