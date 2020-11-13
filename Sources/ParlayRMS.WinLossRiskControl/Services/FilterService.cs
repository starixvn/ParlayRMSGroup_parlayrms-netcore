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
        Task<List<ParlayTypeModel>> GetParlayTypes();
        Task<List<SportModel>> GetSports();
    } 
    
    public class  FilterServiceImp : FilterService
    {
        private const string LanguageCode = "en";
        private readonly IRemotingRefService remotingRefService;
        private readonly IDynamicRepository repository;
        public FilterServiceImp(IDynamicRepository repository, IRemotingRefService remotingRefService)
        {
            this.repository = repository;
            this.remotingRefService = remotingRefService;
        }

        public async Task<List<CompanyModel>> GetCompany() {
            var criteria = new CompanyCriteria();
            return (await repository.FetchAsync<CompanyModel>(criteria)).ToList();
        }
        public Task<List<ParlayTypeModel>> GetParlayTypes() {
            var palarTypes = new List<ParlayTypeModel>()
            {
                new ParlayTypeModel { Name = "Mix Parlay", Id = 1 },
                new ParlayTypeModel { Name = "Combo Parlay", Id = 2 },
                new ParlayTypeModel { Name = "Penalty Shootout Combo", Id = 3 },
                new ParlayTypeModel { Name = "Single Match Parlay", Id = 4 }
            };

            return Task.FromResult(palarTypes);
        }
        public Task<List<SportModel>> GetSports() {
            var otherSports = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 11, 43, 44 };
            var listSports = new List<SportModel>();
            listSports.Add(CreateSport(1, "Soccer only", 0));
            listSports.Add(CreateSport(-3, "Combined sports", 0));
            foreach (var item in otherSports)
            {
                listSports.Add(CreateSport(item, 1));
            }
            listSports.Add(CreateSport(-2, "Other sports", 1));
            listSports.Add(CreateSport(-1, "Combined other sports", 1));

            return Task.FromResult(listSports);
        }

        private SportModel CreateSport(int sportId, int groupId)
        {
            return new SportModel {
                SportId = sportId,
                SportName = remotingRefService.GetSportName(sportId.ToString(), LanguageCode),
                SportGroup = groupId
            };
        }

        private SportModel CreateSport(int sportId, string sportName, int groupId)
        {
            return new SportModel {
                SportId = sportId,
                SportName = sportName,
                SportGroup = groupId
            };
        }
    }
}