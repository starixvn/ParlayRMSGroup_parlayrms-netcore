using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.WinLossRiskControl.Services
{
    public interface WinLossService
    {
        Task<WinLossModel> GetSites(WinLossRequest request);

        Task<WinLossModel> GetCompanyWinLoss(CompanyWinLossRequest request);

        Task<AgentWinLossModel> GetAgentWinLoss(AgentWinLossRequest request);
    }

    public class WinLossServiceImp : WinLossService
    {
        private readonly IMapper mapper;
        private readonly IDynamicRepository repository;
        public WinLossServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<WinLossModel> GetSites(WinLossRequest request)
        {
            var criteria =  mapper.Map<WinLossCriteria>(request);
            var sites = await repository.FetchAsync<WinLossModel>(criteria);
            var listSiteName = sites.GroupBy(row => row.IsDeposit).Distinct();
            var data = new WinLossModel();
            foreach (var site in listSiteName) {
                var currentSite = new WinLossModel
                {
                    IsDeposit = site.Key
                };
                var rowsBySite = sites.Where(row => row.IsDeposit == currentSite.IsDeposit);      
                foreach (var row in rowsBySite) {
                    currentSite.Rows.Add(row);
                    currentSite.SumTotal(row);
                }

                data.Rows.Add(currentSite);
                data.SumTotal(currentSite);
            }

            return data;
        }

        public async Task<WinLossModel> GetCompanyWinLoss(CompanyWinLossRequest request) {
            var criteria =  mapper.Map<CompanyWinLossCriteria>(request);
            var list = await repository.FetchAsync<WinLossModel>(criteria);
            var data = new WinLossModel();
            foreach (var row in list) {
                data.Rows.Add(row);
                data.SumTotal(row);
            }

            return data;
        }

        public async Task<AgentWinLossModel> GetAgentWinLoss(AgentWinLossRequest request) {
            var criteria =  mapper.Map<AgentWinLossCriteria>(request);
            var result = await repository.FetchMultipleAsync<WinLossModel, int>(criteria);
            var data = new AgentWinLossModel();
            foreach (var row in result.Item1) {
                data.Rows.Add(row);
                data.SumTotal(row);
            }
            
            data.TotalRecords = result.Item2.FirstOrDefault();
            return data;
        }
    }
}