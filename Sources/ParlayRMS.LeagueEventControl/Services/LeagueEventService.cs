using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Services;

public interface LeagueEventService
{
	Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request);
	Task<IEnumerable<EventGroupModel>> GetEvents(EventRequest request);
	Task<List<EventDetailModel>> GetEventDetail(EventDetailRequest request);
	Task<IEnumerable<MainTicketModel>> GetMainTickets(ExcelRequest request);
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

	public Task<IEnumerable<EventGroupModel>> GetEvents(EventRequest request)
	{
		var criteria = mapper.Map<EventCriteria>(request);
		var rawData = dynamicRepository.Fetch<EventModel>(criteria);
		var eventGroup = UpdateModelView(rawData);
		return Task.FromResult(eventGroup);
	}

	public Task<List<EventDetailModel>> GetEventDetail(EventDetailRequest request)
	{
		var result = new List<EventDetailModel>();
		var criteria = mapper.Map<EventDetailCriteria>(request);
		var data = dynamicRepository.Fetch<EventDetailModel>(criteria);
		var (stakeTotal, ticketTotal) = data.Aggregate((Stake: 0m, Ticket: 0), (sums, item) => (sums.Stake + item.Stake, sums.Ticket + item.Ticket));
		result.Add(new EventDetailModel { Stake = stakeTotal, Ticket = ticketTotal });
		result.AddRange(data.OrderByDescending(x => x.Stake));
		return Task.FromResult(result);
	}

	public Task<IEnumerable<MainTicketModel>> GetMainTickets(ExcelRequest request)
	{
		var criteria = mapper.Map<EventDetailCriteria>(request);
		var result = dynamicRepository.Fetch<MainTicketModel>(criteria);
		return Task.FromResult(result);
	}

	private static IEnumerable<EventGroupModel> UpdateModelView(IEnumerable<EventModel> rawData)
	{
		var eventGroup = rawData.GroupBy(x => x.MatchId).Select(x => new EventGroupModel { MatchId = x.Key, EventList = x.ToList() });
		var result = new List<EventGroupModel>();
		byte index = 1;
		const string Others = "Others";

		foreach (var item in eventGroup)
		{
			var totalStake = 0m;
			var totalTicket = 0;
			var subHomeTotalStake = 0m;
			var subAwayTotalStake = 0m;
			var subHomeTotalTicket = 0;
			var subAwayTotalTicket = 0;
			var eventGroupModel = new EventGroupModel();
			var awayEventModelList = new List<AwayEventModel>();
			var homeName = item.EventList.FirstOrDefault()?.HomeName;
			var awayName = item.EventList.FirstOrDefault()?.AwayName;
			foreach (var eventItem in item.EventList)
			{
				totalStake += eventItem.HomeTotalStake + eventItem.AwayTotalStake;
				totalTicket += eventItem.HomeTotalTicket + eventItem.AwayTotalTicket;
				if (eventItem.IsOther) continue;
				subHomeTotalStake += eventItem.HomeTotalStake;
				subHomeTotalTicket += eventItem.HomeTotalTicket;
				subAwayTotalStake += eventItem.AwayTotalStake;
				subAwayTotalTicket += eventItem.AwayTotalTicket;
				eventItem.AwayMarket = eventItem.Market;
				awayEventModelList.Add(new AwayEventModel
				{
					AwayAvgOdds = eventItem.AwayAvgOdds,
					AwayMarket = eventItem.AwayMarket,
					AwayTotalStake = eventItem.AwayTotalStake,
					AwayTotalTicket = eventItem.AwayTotalTicket,
					AwayBettype = eventItem.Bettype
				});
				index++;
			}
			eventGroupModel.MatchId = item.MatchId;
			eventGroupModel.EventList = new List<EventModel>
			{
				new()
				{
					HomeTotalStake = subHomeTotalStake,
					HomeTotalTicket = subHomeTotalTicket,
					AwayTotalStake = subAwayTotalStake,
					AwayTotalTicket = subAwayTotalTicket,
					HomeName = homeName,
					AwayName = awayName
				}
			};
			var sortedFixedBettypeList = SortFixedBettypeList(item.EventList, awayEventModelList);
			eventGroupModel.EventList.AddRange(sortedFixedBettypeList);
			var moreThanTwoWayBettypeList = item.EventList.Where(x => x.IsOther && x.Market != Others).Select(x => new EventModel
				{
					HomeName = x.Market,
					HomeTotalStake = x.HomeTotalStake + x.AwayTotalStake,
					HomeTotalTicket = x.HomeTotalTicket + x.AwayTotalTicket,
					HomeAvgOdds = x.HomeAvgOdds + x.AwayAvgOdds,
					Bettype = x.Bettype,
					IsOther = true
				}
			);
			if (moreThanTwoWayBettypeList.Any())
			{
				eventGroupModel.EventList.AddRange(moreThanTwoWayBettypeList.OrderByDescending(x => x.HomeTotalStake));
			}
			var othersItem = item.EventList.FirstOrDefault(x => x.Market == Others);
			if (othersItem != null)
			{
				eventGroupModel.EventList.Add(new EventModel {
					HomeName = Others,
					HomeTotalStake = othersItem.HomeTotalStake + othersItem.AwayTotalStake,
					HomeTotalTicket = othersItem.HomeTotalTicket + othersItem.AwayTotalTicket,
					IsOther = true,
					AwayName = $"{homeName} - vs - {awayName}"
				});
			}
			eventGroupModel.EventList.Add(new EventModel { HomeName = "Total", HomeTotalStake = totalStake, HomeTotalTicket = totalTicket });
			eventGroupModel.FixedBettypes = index; 
			result.Add(eventGroupModel);
			index = 1;
		}
		return result;
	}

	private static IEnumerable<EventModel> SortFixedBettypeList(IEnumerable<EventModel> eventList, List<AwayEventModel> awayEventModelList)
	{
		var fixedBettypeList = eventList.Where(x => !x.IsOther).OrderByDescending(x => x.HomeTotalStake).ToList();
		awayEventModelList = awayEventModelList.OrderByDescending(x => x.AwayTotalStake).ToList();
		for (var i = 0; i < fixedBettypeList.Count; i++)
		{
			fixedBettypeList[i].AwayMarket = awayEventModelList[i].AwayMarket;
			fixedBettypeList[i].AwayAvgOdds = awayEventModelList[i].AwayAvgOdds;
			fixedBettypeList[i].AwayTotalStake = awayEventModelList[i].AwayTotalStake;
			fixedBettypeList[i].AwayTotalTicket = awayEventModelList[i].AwayTotalTicket;
			fixedBettypeList[i].AwayBettype = awayEventModelList[i].AwayBettype;
		}
		return fixedBettypeList;
	}
}