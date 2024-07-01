using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.Services;

public interface FilterService
{
    Task<IEnumerable<LeagueModel>> GetLeagues(LeagueRequest request);

    Task<IEnumerable<BettypeModel>> GetBettypes(BettypeRequest request);
}

public class FilterServiceImp(IDynamicRepository repository, IMapper mapper) : FilterService
{
    public Task<IEnumerable<LeagueModel>> GetLeagues(LeagueRequest request)
    {
        var criteria = mapper.Map<LeagueCriteria>(request);
        return repository.FetchAsync<LeagueModel>(criteria);
    }

    public Task<IEnumerable<BettypeModel>> GetBettypes(BettypeRequest request)
    {
        var criteria = mapper.Map<BettypeCriteria>(request);
        return repository.FetchAsync<BettypeModel>(criteria);
    }
}