using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Services
{
    public interface LeagueEventService
    {
        Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request);
    }

    public class LeagueEventServiceImp : LeagueEventService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IMapper mapper;

        public LeagueEventServiceImp(IDynamicRepository dynamicRepository, IMapper mapper)
        {
            this.dynamicRepository = dynamicRepository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request)
        {
            var criteria = mapper.Map<LeagueDataCriteria>(request);
            return dynamicRepository.FetchAsync<LeagueDataModel>(criteria);
        }
    }
}
