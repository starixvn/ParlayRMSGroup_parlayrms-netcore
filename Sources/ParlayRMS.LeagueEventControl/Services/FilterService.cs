using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Services
{
    public interface FilterService
    {
        Task<IEnumerable<LeagueModel>> GetLeagues();
    }

    public class FilterServiceImp : FilterService
    {
        private readonly IDynamicRepository dynamicRepository;

        public FilterServiceImp(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Task<IEnumerable<LeagueModel>> GetLeagues()
        {
            var criteria = new LeagueCriteria();
            return dynamicRepository.FetchAsync<LeagueModel>(criteria);
        }
    }
}
