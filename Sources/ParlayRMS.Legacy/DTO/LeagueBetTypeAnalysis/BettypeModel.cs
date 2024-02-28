namespace ParlayRMS.Legacy.DTO.LeagueBetTypeAnalysis
{
	public class BettypeModel
	{
		public int GroupId { get; set; }

		public int Bettype { get; set; }

		public string TypeNamee { get; set; }
	}

	public class GroupedBettypeModel
	{
		public int GroupId { get; set; }

		public IEnumerable<BettypeModel> Items { get; set; }
	}
}
