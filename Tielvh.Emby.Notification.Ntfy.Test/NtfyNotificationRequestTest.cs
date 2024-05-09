using Tielvh.Emby.Notification.Ntfy.Notification;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class NtfyNotificationRequestTest
{
    private const string Title = "foo";
    private const string Endpoint = "https://tielvh.uno/bar";
    
    [Test]
    public async Task GivenDescriptionNull_WhenConvertingToHttpRequestOptions_ContentIsEmptyString()
    {
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithEndpoint(Endpoint)
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
            .WithEndpoint(Endpoint)
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
            .WithEndpoint(Endpoint)
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
            .WithEndpoint(Endpoint)
            .WithAccessToken(accessToken)
            .Build();

        var options = request.ToHttpRequestOptions();
        
        Assert.That(options.RequestHeaders, Contains.Key("Authorization"));
    }

    [Test]
    public void GivenUrlNull_WhenConvertingToHttpRequestOptions_TagsHeaderIsNotAdded()
    {
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithEndpoint(Endpoint)
            .WithUrl(null)
            .Build();

        var options = request.ToHttpRequestOptions();
        
        Assert.That(options.RequestHeaders, Does.Not.ContainKey("X-Tags"));
    }

    [Test]
    public void GivenUrl_WhenConvertingToHttpRequestOptions_TagsHeaderIsAdded()
    {
        const string url = "https://tielvh.uno";
        var request = new NtfyNotificationRequestBuilder()
            .WithTitle(Title)
            .WithEndpoint(Endpoint)
            .WithUrl(url)
            .Build();

        var options = request.ToHttpRequestOptions();
        
        Assert.That(options.RequestHeaders, Contains.Key("X-Tags"));
        Assert.That(options.RequestHeaders["X-Tags"], Contains.Substring(url));
    }
}