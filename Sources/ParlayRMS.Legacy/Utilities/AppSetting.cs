namespace ParlayRMS.Legacy.Utilities;

public class AppSetting
{
    public static string[] SelectTop { get; } = { "10", "20", "50", "100", "200", "500", "1000" };

    public static int PageSize => 100;

    public static int HouseLoseAmount => -500;

    public static int HouseLoseAmountFrom => 500;

    public static int HouseLoseAmountTo => 200000;

    public static int MixParlayStakeAfterPT => 5000;

    public static int NetWinStakeAfterPT => 5000;

    public static string MemberWinLossLimitDate => "01/01/2013";

    public static string LimitDataFromDate => "10/31/2012";
}