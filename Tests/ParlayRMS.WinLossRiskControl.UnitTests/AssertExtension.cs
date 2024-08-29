using Newtonsoft.Json;

namespace ParlayRMS.WinLossRiskControl.UnitTests;

public class AssertExtension
{
    public static void AreEqual<T>(T expected, T actual)
    {
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
    }
}