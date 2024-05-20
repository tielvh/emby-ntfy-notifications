using Tielvh.Emby.Notification.Ntfy.Notification;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class NtfyNotificationRequestTest
{
    private const string Title = "foo";
    private const string Instance = "https://tielvh.uno";
    private const string Topic = "bar";

    [Test]
    public async Task GivenDescriptionNull_WhenConvertingToHttpRequestOptions_ContentIsEmptyString()
    {
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic)
            .WithDescription(null)
            .Build();

        var options = request.ToHttpRequestOptions();

        Assert.That(await options.RequestHttpContent.ReadAsStringAsync(), Is.EqualTo(string.Empty));
    }

    [Test]
    public async Task GivenDescription_WhenConvertingToHttpRequestOptions_DescriptionIsAdded()
    {
        const string description = "Lorem ipsum";
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic)
            .WithDescription(description)
            .Build();

        var options = request.ToHttpRequestOptions();

        Assert.That(await options.RequestHttpContent.ReadAsStringAsync(), Is.EqualTo(description));
    }

    [Test]
    public void GivenAuthorizationHeaderNull_WhenConvertingToHttpRequestOptions_HeaderIsNotAdded()
    {
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic)
            .WithAccessToken(null)
            .Build();

        var options = request.ToHttpRequestOptions();

        Assert.That(options.RequestHeaders, Does.Not.ContainKey("Authorization"));
    }

    [Test]
    public void GivenAuthorizationHeader_WhenConvertingToHttpRequestOptions_HeaderIsAdded()
    {
        const string accessToken = "very secret access token :)";
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithInstance(Instance)
            .WithTopic(Topic)
            .WithAccessToken(accessToken)
            .Build();

        var options = request.ToHttpRequestOptions();

        Assert.That(options.RequestHeaders, Contains.Key("Authorization"));
    }
}