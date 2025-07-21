using ParlayRMS.Legacy.Models.Members;
using ParlayRMS.Legacy.Models;

namespace ParlayRMS.Legacy.Utilities;

public static class ModelsHelper
{
    private static readonly List<MemberLevel> memberLevels = [];

    static ModelsHelper()
    {
            var levelDict = new Dictionary<int, string>
            {
                { 10, "level10" },
                { 9, "level9" },
                { 8, "level8" },
                { 7, "level7" },
                { 6, "level6" },
                { 5, "level5" },
                { 4, "level4" },
                { 3, "level3" },
                { 2, "level2" },
                { 1, "level1" },
                { 0, "levelNone" }
            };

            foreach (var entry in levelDict)
            {
                memberLevels.Add(new MemberLevel
                {
                    LevelId = DbConverter.ToInt32(entry.Key),
                    LevelColor = entry.Value,
                    LevelValue = DbConverter.ToInt32(entry.Key) == 0 ? "None" : ViewLang.Level + " " + entry.Key
                });
            }
        }

    public static Dictionary<int, string> LeagueGroupColors = new Dictionary<int, string>
    {
        { 0, "#000000" },
        { 1, "#5b9ad5" },
        { 2, "#f4b083" },
        { 3, "#548235" },
        { 4, "#cf51c9" },
        { 5, "#843c0c" },
        { 6, "#6f30a0" },
        { 7, "#bf9000" },
        { 9, "#2244bf"},
        { 10, "#fbbcc7"},
        { 11, "#772677"},
        { 13, "#757575"},
        { 12, "#1d8191" },
        { 8, "#ff0000" }
    };
    public static string GetColorByLeagueGroup(int groupId)
    {
            return LeagueGroupColors.TryGetValue(groupId, out var color) ? color : string.Empty;
        }

    public static Dictionary<int, string> LeagueGroupNames = new Dictionary<int, string>
    {
        { 0, "All Group" },
        { -1, "None" },
        { 1, "MJ" },
        { 2, "M1" },
        { 3, "M2" },
        { 4, "M3" },
        { 5, "M4 (a)" },
        { 6, "M4 (b)" },
        { 7, "M4 (x)" },
        { 9, "Virtual"},
        { 10, "CUP"},
        { 11, "CORNER"},
        { 13, "BOOKING"},
        { 12, "WORLD CUP" },
        { 8, "Blacklisted" }
    };
    public static string GetNameByLeagueGroup(int groupId)
    {
            return LeagueGroupNames.TryGetValue(groupId, out var groupName) ? groupName : string.Empty;
        }

    public static string GetClassByMemberLevel(int levelId)
    {
            foreach (var level in memberLevels)
            {
                if (level.LevelId == levelId)
                {
                    return level.LevelColor;
                }
            }

            return string.Empty;
        }

    public static object GetReportType(ReportType reportType)
    {
            switch (reportType)
            {
                case ReportType.Combo:
                    return "Combo";

                case ReportType.SportType:
                    return "Sport type";

                case ReportType.ParlayType:
                    return "Parlay type";

            default:
                    throw new NotImplementedException("Report type :" + reportType);
            }
        }

    public static List<MemberLevel> GetMemberLevels()
    {
            return memberLevels;
        }

    public static string GetSportType(string sportTypes)
    {
            var list = sportTypes.Split(',');
            switch (list.Length)
            {
                case 17:
                    return ViewLang.All;
                case 1:
                    {
                        var sportType = Convert.ToInt32(list[0]);
                        return GetSportType(sportType);
                    }
                default:
                    return $"{list.Length} options";
            }
        }

    public static string GetSportType(int sportType)
    {
            switch (sportType)
            {
                case -1:
                    return "Combined other sports";

                case -2:
                    return "Other sports";

                case -3:
                    return "Combined sports";

                case -4:
                    return "Virtual Soccer";

            default:
                    return CacheRef.GetSportName(sportType);
            }
        }

    public static string GetSportType(SportsType sportType)
    {
            switch (sportType)
            {
                case SportsType.AllSports:
                    return ViewLang.Allsports;

                case SportsType.SoccerOnly:
                    return ViewLang.Socceronly;

                case SportsType.OtherSports:
                    return ViewLang.Othersports;

                case SportsType.CombinedSports:
                    return ViewLang.Combinedsports;
            }

            return "Undefined";
        }

    public static string GetParlayTitle(string parlayTypes)
    {
            if (parlayTypes == "1,7,6,2,4,3")
            {
                return ViewLang.All;
            }

            return parlayTypes.Replace("1", ViewLang.MixparLay)
                .Replace("7", ViewLang.ParlayBoost)
                .Replace("6", ViewLang.ParlayGenerator)
                .Replace("2", ViewLang.ComboParlay)
                .Replace("3", ViewLang.SingleMatchParlay)
                .Replace("4", ViewLang.PenaltyShootoutCombo)
                .Replace(",", ", ");
        }

    public static string GetSpecialParlayTitle(string parlayTypes)
    {
            if (parlayTypes == "1,6,2,4,3")
            {
                return ViewLang.All;
            }

            return parlayTypes.Replace("1", ViewLang.MixparLay)
                .Replace("6", ViewLang.ParlayGenerator)
                .Replace("2", ViewLang.ComboParlay)
                .Replace("3", ViewLang.SingleMatchParlay)
                .Replace("4", ViewLang.PenaltyShootoutCombo)
                .Replace(",", ", ");
        }

    public static string GetSimpleParlayTitle(string parlayTypes)
    {
            if (parlayTypes == "1,2,3")
            {
                return ViewLang.All;
            }

            return parlayTypes.Replace("1", ViewLang.MixparLay)
                .Replace("2", ViewLang.ComboParlay)
                .Replace("3", ViewLang.SingleMatchParlay)
                .Replace(",", ", ");
        }

    public static string GetSiteTitle(int site)
    {
            switch (site)
            {
                case 1:
                    return "Deposit";
                case 0:
                    return "Credit";
                default:
                    return ViewLang.All;
            }
        }

    public static string GetCustomerClassTitle(int? customerClass)
    {
            return customerClass == null ? "All" : customerClass.ToString();
        }

    public static string GetLiveTypeTitle(int site)
    {
        switch (site)
        {
            case 1:
                return "Live";
            case 0:
                return "Non Live";
            case 2:
                return "Mix";
            default:
                return ViewLang.All;
        }
    }

    public static string GetLeagueGroupTitle(string leagueGroups)
    {
        var list = leagueGroups.Split(',');
        if (list.Length == 14) return ViewLang.All;
        var title = new List<string>();
        foreach (var item in list)
        {
            title.Add(GetNameByLeagueGroup(Convert.ToInt32(item)));
        }

        return string.Join(",", title);
    }
}