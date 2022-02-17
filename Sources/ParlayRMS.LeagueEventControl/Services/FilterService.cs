using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Services
{
    public interface FilterService
    {
        Task<IEnumerable<LeagueModel>> GetLeagues(LeagueRequest request);
    }

    public class FilterServiceImp : FilterService
    {
        private readonly IDynamicRepository dynamicRepository;

        private readonly IMapper mapper;

        public FilterServiceImp(IDynamicRepository dynamicRepository, IMapper mapper)
        {
            this.dynamicRepository = dynamicRepository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<LeagueModel>> GetLeagues(LeagueRequest request)
        {
            var criteria = mapper.Map<LeagueCriteria>(request);
            return dynamicRepository.FetchAsync<LeagueModel>(criteria);
        }
    }
}
