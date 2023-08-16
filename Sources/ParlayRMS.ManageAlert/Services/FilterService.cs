using Fanex.RemotingRef;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Services;

public interface FilterService
{
	IEnumerable<SportModel> GetSportTypes();
}

public class FilterServiceImp : FilterService
{
	private readonly IRemotingRefService remotingRefService;
	public FilterServiceImp(IRemotingRefService remotingRefService)
	{
		this.remotingRefService = remotingRefService;
	}

	public IEnumerable<SportModel> GetSportTypes()
	{
		var sportTypes = new List<SportModel>
		{
			new() { SportId = 1, SportName = "Soccer only", SportGroup = 0 },
			new() { SportId = -4, SportName = "Virtual Soccer", SportGroup = 0 },
			new() { SportId = -3, SportName = "Combined sports", SportGroup = 0 }
		};
		var otherSports = new[] { 2, -5, 3, 4, 5, 6, 7, 8, 9, 11, 43, 44 };
		sportTypes.AddRange(otherSports.Select(item => item == -5 ?
			new SportModel { SportId = -5, SportName = "Virtual Basketball", SportGroup = 1 } :
			new SportModel { SportId = item, SportName = remotingRefService.GetSportName(item.ToString(), "en"), SportGroup = 1 }
		));
		sportTypes.Add(new SportModel { SportId = -2, SportName = "Other sports", SportGroup = 1 });
		sportTypes.Add(new SportModel { SportId = -1, SportName = "Combined other sports", SportGroup = 0 });
		return sportTypes;
	}
}
