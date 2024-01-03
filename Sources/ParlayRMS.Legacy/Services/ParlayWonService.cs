namespace ParlayRMS.Legacy.Services;

using System.Collections.Generic;
using DataAccess;
using DTO.ParlayWon;
using ViewModel.Report;

public class ParlayWonService(ParlayWonDataAccess _dataAccess, CommonService _commonService)
{
    public IEnumerable<RMS_Rpt_Parlay_ParlayWon> GetParlayWonList(ParlayWonOptions options, out int totalRows)
    {
        var info = _commonService.GetBalanceUpDayInfo();

        return _dataAccess.RMS_Rpt_Parlay_ParlayWon(
            options.FromDate,
            options.ToDate,
            info.MoveBetsDay,
            options.ParlayTypes,
            options.SportsType,
            options.Combos,
            options.Levels,
            options.HouseAmountFrom,
            options.HouseAmountTo,
            options.SortById,
            options.IsSortAsc,
            options.PageIndex,
            options.PageSize,
            options.Currencies,
            options.Site,
            options.LiveType,
            options.CustomerClass,
            options.Ip,
            out totalRows);
    }
}