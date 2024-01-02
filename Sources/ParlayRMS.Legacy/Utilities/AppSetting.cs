namespace ParlayRMS.Legacy.Utilities
{
    public class AppSetting
    {
        private static string[] selectTop = new[] { "10", "20", "50", "100", "200", "500", "1000" };

        public static string[] SelectTop => selectTop;

        public static int PageSize => 100;
    }
}
