using ParlayRMS.LeagueEventControl.Models;
using VaultSharp.V1.SystemBackend;

namespace ParlayRMS.LeagueEventControl.UnitTests;

public static class DataHelper
{
    public static IEnumerable<LeagueModel> GetLeagues()
    {
        return new List<LeagueModel>
        {
            new()
            {
                LeagueId = 104338,
                LeagueName = "SABA NBA 2K21 Friendly Virtual"
            }
        };
    }

    public static IEnumerable<BettypeModel> GetBettypes()
    {
        return new List<BettypeModel>
        {
            new()
            {
                Bettype = 1,
                GroupId = 1,
                TypeNamee = "Handicap"
            },
            new()
            {
                Bettype = 2,
                GroupId = 1,
                TypeNamee = "Odd/Even"
            }
        };
    }

    public static IEnumerable<LeagueDataModel> GetLeagueData()
    {
        return new List<LeagueDataModel>
        {
            new()
            {
                AwayTotalStake = 0,
                LeagueName = "ENGLISH PREMIER LEAGUE - CORNERS",
                AwayTotalTicket = 0,
                HomeTotalStake = 24.5552m,
                HomeTotalTicket = 1,
                LeagueId = 45742,
                OtherTotalStake = 0,
                OtherTotalTicket = 0
            },
            new()
            {
                AwayTotalStake = 0,
                LeagueName = "BULGARIA FIRST PROFESSIONAL LEAGUE",
                AwayTotalTicket = 0,
                HomeTotalStake = 24.5552m,
                HomeTotalTicket = 1,
                LeagueId = 57749,
                OtherTotalStake = 0,
                OtherTotalTicket = 0
            }
        };
    }

    public static IEnumerable<EventModel> GetEvents()
    {
        return new List<EventModel>
        {
            new()
            {
                LeagueName = "*ENGLISH PREMIER LEAGUE - CORNERS",
                HomeTotalTicket = 1,
                HomeTotalStake = 24.5552m,
                HomeAvgOdds = 1.82m,
                HomeName = "Manchester United No.of Corners",
                AwayTotalStake = 0.00m,
                AwayTotalTicket = 0,
                Bettype = 3,
                MatchId = 83718288,
                IsOther = false,
                Market = "Over/Under",
                AwayAvgOdds = 0.00m,
                AwayName = "Arsenal No.of Corners"
            }
        };
    }

    public static IEnumerable<EventDetailModel> GetEventsDetail()
    {
        return new List<EventDetailModel>
        {
            new()
            {
                AvgOdds = 1.82m,
                Bettype = 3,
                Market = "Over/Under",
                Stake = 24.5552m,
                Ticket = 1
            }
        };
    }

    public static IEnumerable<MainTicketModel> GetMainTicket()
    {
        return new List<MainTicketModel>
        {
            new()
            {
                Username = "12BETAUD02001",
                TotalSelection = 3,
                Stake = 74,
                Odds = 6.49m,
                SportType = 1,
                IsSMP = false,
                Currency = "AUD",
                CustomerClass = "400",
                PotentialWinningAmt = 404,
                TotalBet = 1,
                IsBoostBonus = false,
                TransId = 1000000002241155742,
                IsParlayGenerator = false
            }
        };
    }

    public static IEnumerable<EventGroupModel> GetEventGroup()
    {
        return new List<EventGroupModel>
        {
            new()
            {
                EventList =
                [
                    new EventModel
                    {
                        Bettype = 0,
                        Market = null,
                        LeagueName = "*ENGLISH PREMIER LEAGUE - CORNERS",
                        HomeTotalTicket = 1,
                        AwayTotalTicket = 0,
                        AwayTotalStake = 0,
                        HomeTotalStake = 24.5552m,
                        Event = null,
                        AwayAvgOdds = 0,
                        AwayBettype = 0,
                        AwayMarket = null,
                        AwayName = "Arsenal No.of Corners",
                        HomeAvgOdds = 0,
                        HomeName = "Manchester United No.of Corners",
                        IsOther = false,
                        MatchId = 0
                    },

                    new EventModel
                    {
                        Bettype = 0,
                        Market = null,
                        LeagueName = "*ENGLISH PREMIER LEAGUE - CORNERS",
                        HomeTotalTicket = 1,
                        AwayTotalTicket = 0,
                        AwayTotalStake = 0,
                        HomeTotalStake = 24.5552m,
                        Event = null,
                        AwayAvgOdds = 0,
                        AwayBettype = 3,
                        AwayMarket = "Over/Under",
                        AwayName = "Arsenal No.of Corners",
                        HomeAvgOdds = 1.82m,
                        HomeName = "Manchester United No.of Corners",
                        IsOther = false,
                        MatchId = 83718288
                    }
                ],
                FixedBettypes = 2,
                MatchId = 83718288,
                RowsTotal = 2,
                TotalStake = 24.5552m
            }
        };
    }
}