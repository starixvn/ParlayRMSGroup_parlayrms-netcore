namespace ParlayRMS.Legacy.Services;

using System.Collections.Generic;
using DataAccess;
using DTO.TopWinners;
using ViewModel.Report;

public class TopWinnersService (TopWinnersDataAccess _topwinnersDataAccess, CommonService _commonService)
{
    public IEnumerable<RMS_Rpt_Parlay_TopWinner> GetTopWinners(TopWinnersOptions options)
    {
            var info = _commonService.GetBalanceUpDayInfo();
            return _topwinnersDataAccess.RMS_Rpt_Parlay_TopWinners(
                options.FromDate,
                options.ToDate,
                info.MoveBetsDay,
                info.BalanceUpDay,
                options.SportsType,
                options.SelectTop,
                options.Combos,
                options.ParlayTypes,
                options.LiveType,
                options.Currencies,
                options.Site,
                options.CustomerClass);
        }

    public IEnumerable<RMS_Rpt_Parlay_TopWinner_Detail> GetTopWinnerDetails(TopWinnerDetailsOptions options)
    {
            var info = _commonService.GetBalanceUpDayInfo();

            return _topwinnersDataAccess.RMS_Rpt_Parlay_TopWinners_Details(options.CustId, options.FromDate, options.ToDate, info.MoveBetsDay, options.SportsType, options.Combos, options.ParlayTypes, options.LiveType, options.Currencies);
        }
}