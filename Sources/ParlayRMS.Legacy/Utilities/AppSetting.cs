namespace ParlayRMS.Legacy.Utilities
{
    public class AppSetting
    {
        public static string[] SelectTop { get; } = { "10", "20", "50", "100", "200", "500", "1000" };

        public static int PageSize => 100;

        public static int HouseLoseAmountFrom => 1000;

        public static int HouseLoseAmountTo => 10000;

        public static int MixParlayStakeAfterPT => 5000;

        public static int NetWinStakeAfterPT => 5000;

        public static string MemberWinLossLimitDate => "01/01/2013";
    }
}
