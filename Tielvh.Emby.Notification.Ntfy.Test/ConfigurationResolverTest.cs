using Randomizer.Types;
using Tielvh.Emby.Notification.Ntfy.Configuration;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class ConfigurationResolverTest
{
    private static readonly RandomStringGenerator RandomStringGenerator = new();
    private const string Url = "https://tielvh.uno";
    private static readonly string Token = RandomStringGenerator.GenerateValue();
    private const string Topic = "notifications";
    private const string HostUrl = "https://emby.tielvh.uno";

    private static readonly IDictionary<string, string> FullOptions = new Dictionary<string, string>
    {
        { ConfigurationResolver.ConfigurationKeys.HostUrl, HostUrl },
        { ConfigurationResolver.ConfigurationKeys.Topic, Topic },
        { ConfigurationResolver.ConfigurationKeys.Url, Url },
        { ConfigurationResolver.ConfigurationKeys.AccessToken, Token }
    };

    private static readonly IDictionary<string, string> MinimalOptions = new Dictionary<string, string>
    {
        { ConfigurationResolver.ConfigurationKeys.HostUrl, HostUrl },
        { ConfigurationResolver.ConfigurationKeys.Topic, Topic },
        { ConfigurationResolver.ConfigurationKeys.Url, string.Empty },
        { ConfigurationResolver.ConfigurationKeys.AccessToken, string.Empty }
    };

    [Test]
    public void GivenFullOptions_WhenResolvingUrl_OptionsUrlIsUsed()
    {
        var configurationResolver = new ConfigurationResolver(FullOptions);

        var url = configurationResolver.Url;

        StringAssert.Contains(Url, url);
    }

    [Test]
    public void GivenMinimalOptions_WhenResolvingUrl_DefaultUrlIsUsed()
    {
        var configurationResolver = new ConfigurationResolver(MinimalOptions);

        var endpoint = configurationResolver.Url;

        StringAssert.Contains(ConfigurationResolver.DefaultNtfyInstanceUrl, endpoint);
    }
}