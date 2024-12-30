using AutoMapper;
using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.LeagueEventControl.Excels;
using ParlayRMS.LeagueEventControl.Handlers;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Models.Excels;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Handlers;

public class ExcelHandlerUnitTests
{
    private readonly LeagueEventService service = Substitute.For<LeagueEventService>();

    private readonly ExcelBuilder excelBuilder = new ExcelBuilderImp();

    private readonly IRemotingRefService refService = Substitute.For<IRemotingRefService>();

    private readonly IMapper mapper = new MapperConfiguration(cf =>
    {
        cf.CreateMap<MainTicketModel, MainTicketExcelModel>();
    }).CreateMapper();

    [Fact]
    public async Task Handler_Always_ReturnExpectedExcelPackage()
    {
        // Arrange
        service.GetMainTickets(Arg.Any<ExcelRequest>()).Returns(Task.FromResult(DataHelper.GetMainTicket()));
        refService.GetSportName(Arg.Any<string>(), Arg.Any<string>()).Returns("Soccer");

        // Act
        var excelPackage = await new ExcelHandler(service, excelBuilder, mapper, refService).Handle(new ExcelRequest(), CancellationToken.None);

        // Assert
        Assert.Equal("League_EventMonitor", excelPackage.Workbook.Worksheets[0].Name);
        Assert.Equal(1, excelPackage.Workbook.Worksheets[0].Cells["A3"].Value);
        Assert.Equal("1000000002241155742", excelPackage.Workbook.Worksheets[0].Cells["C3"].Value);
        Assert.Equal("Soccer", excelPackage.Workbook.Worksheets[0].Cells["M3"].Value);
    }
}