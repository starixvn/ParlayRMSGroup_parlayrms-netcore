using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fanex.Data.Repository;
using Fanex.RemotingRef;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Services
{
    public interface FilterService
    {
        Task<List<CompanyModel>> GetCompany();

        Task<List<SelectItemModel>> GetParlayTypes();

        Task<List<SportModel>> GetSports();

        Task<List<SelectItemModel>> GetComboes();
    }

    public class FilterServiceImp : FilterService
    {
        private const string LanguageCode = "en";
        private readonly IRemotingRefService remotingRefService;
        private readonly IDynamicRepository repository;

        public FilterServiceImp(IDynamicRepository repository, IRemotingRefService remotingRefService)
        {
            this.repository = repository;
            this.remotingRefService = remotingRefService;
        }

        public async Task<List<CompanyModel>> GetCompany()
        {
            var criteria = new CompanyCriteria();
            return (await repository.FetchAsync<CompanyModel>(criteria)).ToList();
        }

        public Task<List<SelectItemModel>> GetParlayTypes()
        {
            var palarTypes = new List<SelectItemModel>()
            {
                new SelectItemModel { Name = "Mix Parlay", Id = 1 },
                new SelectItemModel { Name = "God Of Fortune", Id = 5 },
                new SelectItemModel { Name = "Combo Parlay", Id = 2 },
                new SelectItemModel { Name = "Penalty Shootout Combo", Id = 4 },
                new SelectItemModel { Name = "Single Match Parlay", Id = 3 }
            };

            return Task.FromResult(palarTypes);
        }

        public Task<List<SportModel>> GetSports()
        {
            var otherSports = new int[] { 2, -5, 3, 4, 5, 6, 7, 8, 9, 11, 43, 44 };
            var listSports = new List<SportModel>();
            listSports.Add(CreateSport(1, "Soccer only", 0));
            listSports.Add(CreateSport(-4, "Virtual Soccer", 0));
            listSports.Add(CreateSport(-3, "Combined sports", 0));
            foreach (var item in otherSports)
            {
                if (item == -5)
                {
                    listSports.Add(CreateSport(item, "Virtual Basketball", 1));
                }
                else
                {
                    listSports.Add(CreateSport(item, 1));
                }
            }
            listSports.Add(CreateSport(-2, "Other sports", 1));
            listSports.Add(CreateSport(-1, "Combined other sports", 1));

            return Task.FromResult(listSports);
        }

        private SportModel CreateSport(int sportId, int groupId)
        {
            return new SportModel
            {
                SportId = sportId,
                SportName = remotingRefService.GetSportName(sportId.ToString(), LanguageCode),
                SportGroup = groupId
            };
        }

        private SportModel CreateSport(int sportId, string sportName, int groupId)
        {
            return new SportModel
            {
                SportId = sportId,
                SportName = sportName,
                SportGroup = groupId
            };
        }

        public Task<List<SelectItemModel>> GetComboes()
        {
            var comboes = new List<SelectItemModel>()
            {
                new SelectItemModel { Name = "2", Id = 2 },
                new SelectItemModel { Name = "3", Id = 3 },
                new SelectItemModel { Name = "4", Id = 4 },
                new SelectItemModel { Name = "5", Id = 5 },
                new SelectItemModel { Name = "6", Id = 6 },
                new SelectItemModel { Name = "7", Id = 7 },
                new SelectItemModel { Name = "8", Id = 8 },
                new SelectItemModel { Name = "9", Id = 9 },
                new SelectItemModel { Name = "10 UP", Id = 10 }
            };

            return Task.FromResult(comboes);
        }
    }
}