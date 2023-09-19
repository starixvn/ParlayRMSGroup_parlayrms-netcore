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

public class ManageAlertServiceImp : ManageAlertService
{
	private readonly IDynamicRepository dynamicRepository;
	private readonly IMapper mapper;

	public ManageAlertServiceImp(IDynamicRepository dynamicRepository, IMapper mapper)
	{
		this.dynamicRepository = dynamicRepository;
		this.mapper = mapper;
	}

	public async Task<ManageAlertViewModel> GetAlerts(ManageAlertCriteria criteria)
	{
		var result = await dynamicRepository.FetchMultipleAsync<ManageAlertModel, int>(criteria);
		return new ManageAlertViewModel
		{
			Items = result.firstResultSet,
			TotalItems = result.secondResultSet.FirstOrDefault()
		};
	}

	public Task<IEnumerable<SimilarTicketDetailModel>> GetSimilarTicketDetail(ViewDetailCriteria criteria)
	{
		return dynamicRepository.FetchAsync<SimilarTicketDetailModel>(criteria);
	}

	public Task<IEnumerable<PotentialWinningDetailModel>> GetPotentialWinningDetail(ViewDetailCriteria criteria)
	{
		return dynamicRepository.FetchAsync<PotentialWinningDetailModel>(criteria);
	}

	public Task<IEnumerable<ViewLogModel>> GetViewLogs(ViewLogRequest request)
	{
		var criteria = mapper.Map<ViewLogCriteria>(request);
		return dynamicRepository.FetchAsync<ViewLogModel>(criteria);
	}

	public Task<int> TakeAction(TakeActionRequest request)
	{
		var criteria = mapper.Map<TakeActionCriteria>(request);
		return dynamicRepository.ExecuteAsync(criteria);
	}
}