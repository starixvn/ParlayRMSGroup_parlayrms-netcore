using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Services
{
    public interface LeagueEventService
    {
        Task<IEnumerable<LeagueDataModel>> GetLeaguesData(LeagueDataRequest request);
        Task<IEnumerable<EventGroupModel>> GetEvents(EventRequest request);
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

        private static IEnumerable<EventGroupModel> UpdateModelView(IEnumerable<EventModel> rawData)
        {
            var eventGroup = rawData.GroupBy(x => x.MatchId).Select(x => new EventGroupModel { MatchId = x.Key, EventList = x.ToList() });
            var result = new List<EventGroupModel>();
            byte index = 0;
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
                foreach (var eventItem in item.EventList)
                {
                    totalStake += eventItem.HomeTotalStake + eventItem.AwayTotalStake;
                    totalTicket += eventItem.HomeTotalTicket + eventItem.AwayTotalTicket;
                    if (eventItem.HomeName != "Others")
                    {
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
                            AwayTotalTicket = eventItem.AwayTotalTicket
                        });
                        index++;
                    }
                }
                
                var fixedBettypeList = item.EventList.Where(x => x.HomeName != "Others").OrderByDescending(x => x.HomeTotalStake).ToList();
                awayEventModelList = awayEventModelList.OrderByDescending(x => x.AwayTotalStake).ToList();
                for (int i = 0; i < fixedBettypeList.Count; i++)
                {
                    fixedBettypeList[i].AwayMarket = awayEventModelList[i].AwayMarket;
                    fixedBettypeList[i].AwayAvgOdds = awayEventModelList[i].AwayAvgOdds;
                    fixedBettypeList[i].AwayTotalStake = awayEventModelList[i].AwayTotalStake;
                    fixedBettypeList[i].AwayTotalTicket = awayEventModelList[i].AwayTotalTicket;
                }    
                var othersItem = item.EventList.FirstOrDefault(x => x.HomeName == "Others");
                eventGroupModel.MatchId = item.MatchId;
                eventGroupModel.EventList = new();
                eventGroupModel.EventList.Add(new EventModel
                {
                    HomeTotalStake = subHomeTotalStake,
                    HomeTotalTicket = subHomeTotalTicket,
                    AwayTotalStake = subAwayTotalStake,
                    AwayTotalTicket = subAwayTotalTicket
                });
                eventGroupModel.EventList.AddRange(fixedBettypeList);
                eventGroupModel.EventList.Add(othersItem);
                eventGroupModel.EventList.Add(new EventModel { HomeName = "Total", HomeTotalStake = totalStake, HomeTotalTicket = totalTicket });
                eventGroupModel.FixedBettypes = index; 
                result.Add(eventGroupModel);
                index = 0;
            }
            return result;
        }
    }
}
