using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.Services;

public interface LeagueEventService
{
	Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request);
	Task<IEnumerable<EventGroupModel>> GetEvents(EventRequest request);
	Task<List<EventDetailModel>> GetEventDetail(EventDetailRequest request);
	Task<IEnumerable<MainTicketModel>> GetMainTickets(ExcelRequest request);
}

public class LeagueEventServiceImp(IDynamicRepository repository, IMapper mapper) : LeagueEventService
{
    private const string Others = "Others";

    public Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request)
	{
		var criteria = mapper.Map<LeagueDataCriteria>(request);
		return repository.FetchAsync<LeagueDataModel>(criteria);
	}

	public Task<IEnumerable<EventGroupModel>> GetEvents(EventRequest request)
	{
		var criteria = mapper.Map<EventCriteria>(request);
		var rawData = repository.Fetch<EventModel>(criteria);
		var eventGroup = UpdateModelView(rawData);
		return Task.FromResult(eventGroup);
	}

	public Task<List<EventDetailModel>> GetEventDetail(EventDetailRequest request)
	{
		var result = new List<EventDetailModel>();
		var criteria = mapper.Map<EventDetailCriteria>(request);
		var data = repository.Fetch<EventDetailModel>(criteria);
		var (stakeTotal, ticketTotal) = data.Aggregate((Stake: 0m, Ticket: 0), (sums, item) => (sums.Stake + item.Stake, sums.Ticket + item.Ticket));
		result.Add(new EventDetailModel { Stake = stakeTotal, Ticket = ticketTotal });
		result.AddRange(data.OrderByDescending(x => x.Stake));
		return Task.FromResult(result);
	}

	public Task<IEnumerable<MainTicketModel>> GetMainTickets(ExcelRequest request)
	{
		var criteria = mapper.Map<EventDetailCriteria>(request);
		var result = repository.Fetch<MainTicketModel>(criteria);
		return Task.FromResult(result);
	}

	private static IEnumerable<EventGroupModel> UpdateModelView(IEnumerable<EventModel> rawData)
	{
		var eventGroup = rawData.GroupBy(x => x.MatchId).Select(x => new EventGroupModel { MatchId = x.Key, EventList = x.ToList() });
		var result = new List<EventGroupModel>();

		foreach (var item in eventGroup)
		{
			var totalStake = 0m;
			var subHomeTotalStake = 0m;
			var subAwayTotalStake = 0m;
			var subHomeTotalTicket = 0;
			var subAwayTotalTicket = 0;
			var eventGroupModel = new EventGroupModel();
			var awayEventModelList = new List<AwayEventModel>();
			var homeName = item.EventList.FirstOrDefault()?.HomeName;
			var awayName = item.EventList.FirstOrDefault()?.AwayName;
			var leagueName = item.EventList.FirstOrDefault()?.LeagueName;
			foreach (var eventItem in item.EventList)
			{
				totalStake += eventItem.HomeTotalStake + eventItem.AwayTotalStake;
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
					AwayName = awayName,
					LeagueName = leagueName
				}
			};
			var sortedFixedBetTypeList = SortFixedBetTypeList(item.EventList, awayEventModelList);
			eventGroupModel.EventList.AddRange(sortedFixedBetTypeList);
			AddMoreThanTwoWayBetTypeList(eventGroupModel.EventList, item.EventList, homeName, awayName);
			AddOthers(eventGroupModel.EventList, item.EventList, homeName, awayName);
			eventGroupModel.FixedBettypes = sortedFixedBetTypeList.Count() + 1;
			eventGroupModel.RowsTotal = eventGroupModel.EventList.Count;
			eventGroupModel.TotalStake = totalStake;
			result.Add(eventGroupModel);
		}
		return result.OrderByDescending(x => x.TotalStake);
	}

	private static IEnumerable<EventModel> SortFixedBetTypeList(IEnumerable<EventModel> eventList, List<AwayEventModel> awayEventModelList)
	{
		var fixedBetTypeList = eventList.Where(x => !x.IsOther).OrderByDescending(x => x.HomeTotalStake).ToList();
		awayEventModelList = awayEventModelList.OrderByDescending(x => x.AwayTotalStake).ToList();
		for (var i = 0; i < fixedBetTypeList.Count; i++)
		{
			fixedBetTypeList[i].AwayMarket = awayEventModelList[i].AwayMarket;
			fixedBetTypeList[i].AwayAvgOdds = awayEventModelList[i].AwayAvgOdds;
			fixedBetTypeList[i].AwayTotalStake = awayEventModelList[i].AwayTotalStake;
			fixedBetTypeList[i].AwayTotalTicket = awayEventModelList[i].AwayTotalTicket;
			fixedBetTypeList[i].AwayBettype = awayEventModelList[i].AwayBettype;
		}
		return fixedBetTypeList.Where(betType => betType.HomeTotalTicket > 0 || betType.AwayTotalTicket > 0);
	}

	private static void AddMoreThanTwoWayBetTypeList(List<EventModel> source, IEnumerable<EventModel> data, string homeName, string awayName)
	{
		var moreThanTwoWayBetTypeList = data.Where(x => x.IsOther && x.Market != Others).Select(x => new EventModel
			{
				HomeName = x.Market,
				HomeTotalStake = x.HomeTotalStake + x.AwayTotalStake,
				HomeTotalTicket = x.HomeTotalTicket + x.AwayTotalTicket,
				HomeAvgOdds = x.HomeAvgOdds + x.AwayAvgOdds,
				Bettype = x.Bettype,
				IsOther = true,
				Event = $"{homeName} - vs - {awayName}"
		}
		);
		if (moreThanTwoWayBetTypeList.Any())
		{
			source.AddRange(moreThanTwoWayBetTypeList.OrderByDescending(x => x.HomeTotalStake));
		}
	}

	private static void AddOthers(ICollection<EventModel> source, IEnumerable<EventModel> data, string homeName, string awayName)
	{
		var othersItem = data.FirstOrDefault(x => x.Market == Others);
		if (othersItem != null)
		{
			source.Add(new EventModel
			{
				HomeName = Others,
				HomeTotalStake = othersItem.HomeTotalStake + othersItem.AwayTotalStake,
				HomeTotalTicket = othersItem.HomeTotalTicket + othersItem.AwayTotalTicket,
				IsOther = true,
				Event = $"{homeName} - vs - {awayName}"
			});
		}
	}
}