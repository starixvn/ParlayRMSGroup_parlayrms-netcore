using Newtonsoft.Json;
using Xunit;

namespace ParlayRMS.WinLossRiskControl.Test;

public static class AssertExtension
{
	public static void AreEqual<T>(T expected, T actual)
	{
		Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
	}
}