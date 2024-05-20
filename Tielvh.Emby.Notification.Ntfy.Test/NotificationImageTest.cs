using MediaBrowser.Model.Entities;
using Tielvh.Emby.Notification.Ntfy.Notification;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class NotificationImageTest
{
    private const string HostUrl = "https://tielvh.uno";
    private const string ItemId = "1234";
    private const ImageType Type = ImageType.Logo;

    [Test]
    public void GivenImage_WhenGettingStaticUrl_UrlIsCorrect()
    {
        var image = new NotificationImage(HostUrl, Type, ItemId);

        var url = image.StaticUrl;

        StringAssert.StartsWith(HostUrl, url);
        StringAssert.Contains(ItemId, url);
        StringAssert.EndsWith(Type.ToString(), url);
    }

    [Test]
    public void GivenInvalidHostUrl_WhenConstructing_UriFormatExceptionIsThrown()
    {
        const string invalidHostUrl = "bogus";

        Assert.Throws<UriFormatException>(() => _ = new NotificationImage(invalidHostUrl, Type, ItemId));
    }
}