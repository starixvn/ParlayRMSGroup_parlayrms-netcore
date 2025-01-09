using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Services;

public class WinLossServiceUnitTests
{
    private readonly IDynamicRepository repository;
    private readonly WinLossService service;

    public WinLossServiceUnitTests()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new WinLossMappingProfile());
        });

        var mapper = mockMapper.CreateMapper();
        repository = Substitute.For<IDynamicRepository>();
        service = new WinLossServiceImp(repository, mapper);
    }

    [Fact]
    public async Task GetSites_Always_ReturnsExpectedList()
    {
        // Arrange
        var request = new WinLossRequest();
        var fakeData = DataHelper.SiteWinLoss;
        repository.FetchAsync<WinLossModel>(Arg.Any<WinLossCriteria>()).Returns(fakeData);

        // Act
        var actualResults = await service.SitesWinLoss(request);

        // Assert
        var expectedList = DataHelper.ExpectedSiteWinLoss();
        AssertExtension.AreEqual(expectedList, actualResults);
    }

    [Fact]
    public async Task GetCompanyWinLoss_Always_ReturnsExpectedList()
    {
        // Arrange
        var request = new SuperWinLossRequest();
        var fakeData = DataHelper.CompanyWinLoss;
        repository.FetchAsync<WinLossModel>(Arg.Any<CompanyWinLossCriteria>()).Returns(fakeData);

        // Act
        var actualResults = await service.SuperWinLoss(request);

        // Assert
        var expectedList = DataHelper.ExpectedCompanyWinLoss();
        AssertExtension.AreEqual(expectedList, actualResults);
    }

    [Fact]
    public async Task AgentWinLossByMaster_Always_ReturnsExpectedList()
    {
        // Arrange
        var request = new AgentWinLossRequest();
        var fakeData = new ValueTuple<IEnumerable<WinLossModel>, IEnumerable<int>>
        {
            Item1 = DataHelper.AgentWinLoss,
            Item2 = new List<int> { 3 }
        };

        repository.FetchMultipleAsync<WinLossModel, int>(Arg.Any<AgentWinLossCriteria>()).Returns(fakeData);

        // Act
        var actualResults = await service.AgentWinLossByMaster(request);

        // Assert
        var expectedList = DataHelper.ExpectedAgentWinLoss();
        AssertExtension.AreEqual(expectedList, actualResults);
    }

    [Fact]
    public async Task AgentWinLossBySite_Always_ReturnsExpectedList()
    {
        // Arrange
        var request = new AgentWinLossRequest();

        repository.FetchAsync<WinLossModel>(Arg.Any<CompanyWinLossCriteria>()).Returns(DataHelper.AgentWinLoss);

        // Act
        var actualResults = await service.AgentWinLossBySite(request);

        // Assert
        var expectedList = new WinLossModel
        {
            Username = "Grand Total",
            NoMember = 3,
            BetCount = 6,
            MemberWinLoss = 9,
            SmaWinLoss = 12,
            ExtraPT = 15,
            Commission = 6,
            RealIncome = 17,
            RealTurnover = 22,
            Margin = Convert.ToDecimal("77.272727272727272727272727270"),
            Rows = DataHelper.AgentWinLoss
        };
        AssertExtension.AreEqual(expectedList, actualResults);
    }
}