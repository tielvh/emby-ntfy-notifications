using Tielvh.Emby.Notification.Ntfy.Notification;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class NtfyNotificationRequestBuilderTest
{
    [Test]
    public void GivenInvalidEndpointFormat_WhenAddingEndpoint_UriFormatExceptionIsThrown()
    {
        const string invalidEndpoint = "bogus";
        var builder = new NtfyNotificationRequestBuilder();

        Assert.Throws<UriFormatException>(() => builder.WithEndpoint(invalidEndpoint));
    }

    [Test]
    public void GivenNoEndpointScheme_WhenAddingEndpoint_UriFormatExceptionIsThrown()
    {
        const string endpointWithInvalidScheme = "tielvh.uno";
        var builder = new NtfyNotificationRequestBuilder();

        Assert.Throws<UriFormatException>(() => builder.WithEndpoint(endpointWithInvalidScheme));
    }
}