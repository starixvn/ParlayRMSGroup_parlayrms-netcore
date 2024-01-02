namespace ParlayRMS.Legacy.ViewModel.PalayMonitor
{
    using System.ComponentModel.DataAnnotations;

    public class DataModel
    {
        public string Combos { get; set; }

        public string ParlayTypes { get; set; }

        public string SportsTypes { get; set; }

        public string Levels { get; set; }

        public string Markets { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal StakeAfterPT { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal NetWinAfterPT { get; set; }

        public int ReportType { get; set; }

        [Required]
        public int SelectTop { get; set; }

        [Required, Range(0, long.MaxValue)]
        public long MaxTransId { get; set; }

        public bool IsCornerParlay { get; set; }

        public decimal? Odds { get; set; }

        public string LeagueGroups { get; set; }

        public int? CustomerClass { get; set; }

        public string Ip { get; set; }

        public byte LiveType { get; set; }

        public string Company { get; set; }

        public string UserName { get; set; }
    }
}
