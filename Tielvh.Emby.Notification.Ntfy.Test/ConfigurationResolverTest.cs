using Randomizer.Types;
using Tielvh.Emby.Notification.Ntfy.Configuration;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class ConfigurationResolverTest
{
    private static readonly RandomStringGenerator RandomStringGenerator = new();

    [Test]
    public void GivenFullOptions_WhenResolvingEndpoint_OptionsUrlIsUsed()
    {
        var options = new Dictionary<string, string>
        {
            { "Url", "https://foo.bar" },
            { "Token", RandomStringGenerator.GenerateValue() },
            { "Topic", "notifications" }
        };
        var configurationResolver = new ConfigurationResolver(options);

        var endpoint = configurationResolver.NtfyEndpoint;

        StringAssert.Contains("https://foo.bar", endpoint);
    }

    [Test]
    public void GivenMinimalOptions_WhenResolvingEndpoint_DefaultUrlIsUsed()
    {
        var options = new Dictionary<string, string>
        {
            { "Url", "" },
            { "Token", "" },
            { "Topic", "notifications" }
        };
        var configurationResolver = new ConfigurationResolver(options);

        var endpoint = configurationResolver.NtfyEndpoint;

        StringAssert.Contains("https://ntfy.sh", endpoint);
    }
}