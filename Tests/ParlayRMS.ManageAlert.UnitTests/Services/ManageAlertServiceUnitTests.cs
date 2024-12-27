using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.ManageAlert.AutoMapperProfiles;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Services;

public class ManageAlertServiceUnitTests
{
    private readonly IDynamicRepository repository;
    private readonly ManageAlertService service;

    public ManageAlertServiceUnitTests()
    {
        repository = Substitute.For<IDynamicRepository>();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ManageAlertMappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        service = new ManageAlertServiceImp(repository, mapper);
    }

    [Fact]
    public async Task GetAlerts_Always_ReturnExpectedAlerts()
    {
        // Arrange
        var criteria = new ManageAlertCriteria
            { FromDate = "", ToDate = "", ActionType = "", AlertType = "", Selection = "", TicketStatus = "" };
        var manageAlerts = new List<ManageAlertModel>
        {
            new()
            {
                Action = "Take Note",
                AlertId = "1",
                AlertTime = new DateTime(),
                BetId = "123456",
                AlertType = "Similar Bet",
                BetType = 1,
                CheckedBy = null,
                CompanyLose = 1,
                CustId = 1,
                MemberStake = 10,
                Odds = 1,
                PotentialWinning = 1,
                RefNo = "1234",
                Selection = "2",
                SimilarTickets = null,
                SportType = "Soccer Only",
                TotalBets = "1",
                UserName = "user"
            }
        };
        repository.FetchMultipleAsync<ManageAlertModel, int>(Arg.Any<ManageAlertCriteria>()).Returns((manageAlerts, [1]));

        // Act
        var actualResults = await service.GetAlerts(criteria);

        // Assert
        Assert.Equal(1, actualResults.TotalItems);
        Assert.Equal(1, actualResults.Items.Count());
    }

    [Fact]
    public async Task GetSimilarTicketDetail_Always_ReturnExpectedList()
    {
        // Arrange
        var criteria = new ViewDetailCriteria { Selection = "", TicketStatus = "" };
        var similarTicketDetails = new List<SimilarTicketDetailModel>
        {
            new()
            {
                RefNo = "12345",
                CustId = 1,
                UserName = "user",
                BetType = 1,
                CustomerClass = 1,
                HouseAmt = 10,
                SiteGroup = "",
                Stake = 10,
                Status = "Running",
                TransId = "123456789",
                WinningAmt = 10
            }
        };
        repository.FetchAsync<SimilarTicketDetailModel>(criteria).Returns(similarTicketDetails);

        // Act
        var actualResults = await service.GetSimilarTicketDetail(criteria);

        // Assert
        Assert.Equal(1, actualResults.Count());
    }

    [Fact]
    public async Task GetPotentialWinningDetail_Always_ReturnExpectedList()
    {
        // Arrange
        var criteria = new ViewDetailCriteria { Selection = "", TicketStatus = "" };
        var potentialWinningDetails = new List<PotentialWinningDetailModel>
        {
            new()
            {
                RefNo = "12345",
                CustId = 1,
                UserName = "user",
                BetType = 1,
                HouseAmt = 10,
                Stake = 10,
                Status = "Running",
                TransId = "123456789",
                WinningAmt = 10,
                TotalSelection = 2,
                Odds = 2,
                SportType = "Other sports",
                HousePt = 10,
                Ip = "127.0.0.1",
                IsBoostBonus = false,
                IsParlayGenerator = false,
                TotalBet = 1,
                TransDate = new DateTime()
            }
        };
        repository.FetchAsync<PotentialWinningDetailModel>(criteria).Returns(potentialWinningDetails);

        // Act
        var actualResults = await service.GetPotentialWinningDetail(criteria);

        // Assert
        Assert.Equal(1, actualResults.Count());
    }

    [Fact]
    public async Task GetViewLogs_Always_ReturnExpectedList()
    {
        // Arrange
        var request = new ViewLogRequest();
        var viewLogs = new List<ViewLogModel>
        {
            new()
            {
                UserName = "user",
                ActionName = "Take Note",
                LogTime = new DateTime()
            }
        };
        repository.FetchAsync<ViewLogModel>(Arg.Any<ViewLogCriteria>()).Returns(viewLogs);

        // Act
        var actualResults = await service.GetViewLogs(request);

        // Assert
        Assert.Equal(1, actualResults.Count());
    }

    [Fact]
    public async Task TakeAction_Always_ReturnExpectedList()
    {
        // Arrange
        var request = new TakeActionRequest();
        repository.ExecuteAsync(Arg.Any<TakeActionCriteria>()).Returns(1);

        // Act
        var actualResults = await service.TakeAction(request);

        // Assert
        Assert.Equal(1, actualResults);
    }
}