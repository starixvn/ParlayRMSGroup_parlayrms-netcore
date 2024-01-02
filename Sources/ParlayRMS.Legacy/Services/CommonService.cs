namespace ParlayRMS.Legacy.Services
{
    using System;
    using System.Collections.Generic;
    using DataAccess;
    using DTO.Common;
    using Microsoft.Extensions.Caching.Memory;

    public class CommonService : BaseService
    {
        private static readonly IMemoryCache CacheInstance = new MemoryCache(
                new MemoryCacheOptions
                {
                     ExpirationScanFrequency = TimeSpan.FromSeconds(30),
                     CompactionPercentage = 0.15
                }
            );
        private readonly CommonDataAccess _commonDataAccess = new CommonDataAccess();

        public RMS_SelBalanceUpDay GetBalanceUpDayInfo()
        {
            const string key = "BalanceUpDayInfo";

            var cacheIsAvailable = CacheInstance.TryGetValue(key, out RMS_SelBalanceUpDay balanceUpDay);
            if (!cacheIsAvailable)
            {
                balanceUpDay = _commonDataAccess.RMS_SelBalanceUpDay();
                CacheInstance.Set(key, balanceUpDay, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) });
            }

            return balanceUpDay;
        }

        public RMS_Rpt_Parlay_SelMinTransdate GetMinTransDate()
        {
            const string key = "MinTransDate";
            var cacheIsAvailable = CacheInstance.TryGetValue(key, out RMS_Rpt_Parlay_SelMinTransdate minTransdate);
            if (!cacheIsAvailable)
            {
                minTransdate = _commonDataAccess.RMS_Rpt_Parlay_SelMinTransdate();
                CacheInstance.Set(key, minTransdate, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) });
            }

            return minTransdate;
        }

        public List<int> GetBettypeBelongsBetRadar()
        {
            const string key = "BettypeBelongsBetRadar";
            var cacheIsAvailable = CacheInstance.TryGetValue(key, out List<int> betTypes);
            if (!cacheIsAvailable)
            {
                betTypes = _commonDataAccess.GetBettypeBelongsBetRadar();
                CacheInstance.Set(key, betTypes, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
            }

            return betTypes;
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _commonDataAccess.GetCurrencies();
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _commonDataAccess.RMS_SiteByGroupSel();
        }
    }
}
