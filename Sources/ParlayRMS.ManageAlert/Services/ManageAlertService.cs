using AutoMapper;
using Fanex.Data.Repository;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.Services
{
    public interface ManageAlertService
    {
        Task<IEnumerable<ManageAlertModel>> GetAlerts(ManageAlertRequest  request);

        Task<IEnumerable<SimilarTicketDetailModel>> GetSimilarTicketDetail(SimilarTicketDetailRequest request);

        Task<IEnumerable<PotentialWinningDetailModel>> GetPotentialWinningDetail(PotentialWinningDetailRequest request);

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

        public Task<IEnumerable<ManageAlertModel>> GetAlerts(ManageAlertRequest request)
        {
            var criteria = mapper.Map<ManageAlertCriteria>(request);
            return dynamicRepository.FetchAsync<ManageAlertModel>(criteria);
        }

        public Task<IEnumerable<SimilarTicketDetailModel>> GetSimilarTicketDetail(SimilarTicketDetailRequest request)
        {
            var criteria = mapper.Map<ViewDetailCriteria>(request);
            return dynamicRepository.FetchAsync<SimilarTicketDetailModel>(criteria);
        }

        public Task<IEnumerable<PotentialWinningDetailModel>> GetPotentialWinningDetail(PotentialWinningDetailRequest request)
        {
            var criteria = mapper.Map<ViewDetailCriteria>(request);
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
}
