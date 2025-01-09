using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.UnitTests;

public static class DataHelper
{
    public static IEnumerable<ManageAlertModel> GetManageAlerts()
    {
        return new List<ManageAlertModel>
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
    }

    public static IEnumerable<SimilarTicketDetailModel> GetSimilarTicketDetails()
    {
        return new List<SimilarTicketDetailModel>
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
    }

    public static IEnumerable<PotentialWinningDetailModel> GetPotentialWinningDetails()
    {
        return new List<PotentialWinningDetailModel>
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
    }

    public static IEnumerable<ViewLogModel> GetViewLogs()
    {
        return new List<ViewLogModel>
        {
            new()
            {
                UserName = "user",
                ActionName = "Take Note",
                LogTime = new DateTime()
            }
        };
    }
}