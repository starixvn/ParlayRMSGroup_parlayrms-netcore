using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.Services;

public interface ManageAlertService
{
	Task<ManageAlertViewModel> GetAlerts(ManageAlertCriteria criteria);

	Task<IEnumerable<SimilarTicketDetailModel>> GetSimilarTicketDetail(ViewDetailCriteria criteria);

	Task<IEnumerable<PotentialWinningDetailModel>> GetPotentialWinningDetail(ViewDetailCriteria criteria);

	Task<IEnumerable<ViewLogModel>> GetViewLogs(ViewLogRequest request);

	Task<int> TakeAction(TakeActionRequest request);
}

public class ManageAlertServiceImp(IDynamicRepository repository, IMapper mapper) : ManageAlertService
{
    public async Task<ManageAlertViewModel> GetAlerts(ManageAlertCriteria criteria)
	{
		var result = await repository.FetchMultipleAsync<ManageAlertModel, int>(criteria);
		return new ManageAlertViewModel
		{
			Items = result.FirstResultSet,
			TotalItems = result.SecondResultSet.FirstOrDefault()
		};
	}

	public Task<IEnumerable<SimilarTicketDetailModel>> GetSimilarTicketDetail(ViewDetailCriteria criteria)
	{
		return repository.FetchAsync<SimilarTicketDetailModel>(criteria);
	}

	public Task<IEnumerable<PotentialWinningDetailModel>> GetPotentialWinningDetail(ViewDetailCriteria criteria)
	{
		return repository.FetchAsync<PotentialWinningDetailModel>(criteria);
	}

	public Task<IEnumerable<ViewLogModel>> GetViewLogs(ViewLogRequest request)
	{
		var criteria = mapper.Map<ViewLogCriteria>(request);
		return repository.FetchAsync<ViewLogModel>(criteria);
	}

	public Task<int> TakeAction(TakeActionRequest request)
	{
		var criteria = mapper.Map<TakeActionCriteria>(request);
		return repository.ExecuteAsync(criteria);
	}
}