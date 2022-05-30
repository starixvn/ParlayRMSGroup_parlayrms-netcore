using AutoMapper;
using Fanex.RemotingRef;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.LeagueEventControl.Excels;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Models.Excels;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParlayRMS.LeagueEventControl.Handlers
{
    public class ExcelHandler : IRequestHandler<ExcelRequest, ExcelPackage>
    {
        private const string LanguageCode = "en";

        private readonly LeagueEventService leagueEventService;

        private readonly ExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        private readonly IRemotingRefService remotingRefService;

        public ExcelHandler(LeagueEventService leagueEventService, ExcelBuilder excelBuilder, IMapper mapper, IRemotingRefService remotingRefService)
        {
            this.leagueEventService = leagueEventService;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
            this.remotingRefService = remotingRefService;
        }

        public async Task<ExcelPackage> Handle(ExcelRequest request, CancellationToken cancellationToken)
        {
            var excelSetting = new ExcelSetting
            {
                Title = request.Title,
                FileName = "league-event-monitor.xlsx"
            };

            var data = await leagueEventService.GetMainTickets(request);
            UpdateViewModel(data);
            var list = mapper.Map<List<MainTicketExcelModel>>(data);
            return excelBuilder.CreateExcelPackage(list, excelSetting);
        }

        private void UpdateViewModel(IEnumerable<MainTicketModel> list)
        {
            int index = 1;
            foreach(var item in list)
            {
                item.No = index;
                item.TransDateString = item.TransDate.ToString("MM/dd/yyyy h:mm:ss tt");
                item.Stake = Math.Round(item.Stake, 2);
                item.MaxPayOut = Math.Floor(item.MaxPayOut);
                item.SportTypeName = GetSportName(item.SportType);
                item.CustomerClass = string.IsNullOrEmpty(item.CustomerClass) ? "None" : item.CustomerClass;
                index++;
            }    
        }

        public string GetSportName(int sportId)
        {
            return sportId switch
            {
                -1 => "Combined other sports",
                -2 => "Other sports",
                -3 => "Combined sports",
                -4 => "Virtual Soccer",
                -5 => "Virtual Basketball",
                _ => remotingRefService.GetSportName(sportId.ToString(), LanguageCode),
            };
        }
    }
}
