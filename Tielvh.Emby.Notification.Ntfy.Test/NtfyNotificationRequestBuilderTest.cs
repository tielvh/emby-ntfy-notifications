using Tielvh.Emby.Notification.Ntfy.Configuration;
using Tielvh.Emby.Notification.Ntfy.Notification;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class NtfyNotificationRequestBuilderTest
{
    private const string Title = "title";
    private const string Topic = "topic";
    private const string Instance = "https://tielvh.uno";

    [Test]
    public void GivenInvalidInstanceFormat_WhenAddingInstance_UriFormatExceptionIsThrown()
    {
        const string invalidInstance = "bogus";
        var builder = new NtfyNotificationRequestBuilder();

        Assert.Throws<UriFormatException>(() => builder.WithInstance(invalidInstance));
    }

    [Test]
    public void GivenNoInstanceScheme_WhenAddingInstance_UriFormatExceptionIsThrown()
    {
        const string instanceWithInvalidScheme = "tielvh.uno";
        var builder = new NtfyNotificationRequestBuilder();

        Assert.Throws<UriFormatException>(() => builder.WithInstance(instanceWithInvalidScheme));
    }

    [Test]
    public void GivenInstanceNotSet_WhenBuilding_InvalidOperationExceptionIsThrown()
    {
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithTopic(Topic);

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Test]
    public void GivenTopicNotSet_WhenBuilding_InvalidOperationExceptionIsThrown()
    {
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance);

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Test]
    public void GivenTitleNotSet_WhenBuilding_InvalidOperationExceptionIsThrown()
    {
        var builder = new NtfyNotificationRequestBuilder()
            .WithInstance(Instance)
            .WithTopic(Topic);

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Test]
    public void GivenInstanceDefaultAndAccessTokenSet_WhenBuilding_InvalidOperationExceptionIsThrown()
    {
        const string accessToken = "abc123";
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(ConfigurationResolver.DefaultNtfyInstanceUrl)
            .WithTopic(Topic)
            .WithAccessToken(accessToken);

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Test]
    public void GivenTitle_WhenBuilding_TitleIsQPEncoded()
    {
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic);

        var request = builder.Build();

        StringAssert.IsMatch(@"^=\?[ -~]+\?[ -~]+\?[ -~]+\?=$", request.Title);
    }

    [Test]
    public void GivenAccessToken_WhenBuilding_AuthorizationHeaderIsSet()
    {
        const string accessToken = "abc123";
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic)
            .WithAccessToken(accessToken);

        var request = builder.Build();
        Assert.Multiple(() =>
        {
            Assert.That(request.AuthorizationHeader, Is.Not.Null);
            Assert.That(() => request.AuthorizationHeader, Is.EqualTo($"Bearer {accessToken}"));
        });
    }

    [Test]
    public void GivenInstanceAndTopic_WhenBuilding_EndpointIsSet()
    {
        var builder = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic);

        var request = builder.Build();

        Assert.Multiple(() =>
        {
            Assert.That(request.Endpoint, Is.Not.Null);
            Assert.That(() => request.Endpoint, Is.EqualTo($"{Instance}/{Topic}"));
        });
    }
}