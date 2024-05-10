using System;
using MediaBrowser.Model.Entities;

namespace Tielvh.Emby.Notification.Ntfy.Notification
{
    public class Image
    {
        private readonly string _itemId;
        private readonly string _hostUrl;
        private readonly ImageType _type;

        public Image(string hostUrl, ImageType type, string itemId)
        {
            _ = new Uri(hostUrl);
            _hostUrl = hostUrl;
            _type = type;
            _itemId = itemId;
        }

        public string StaticUrl => $"{_hostUrl.TrimEnd('/')}/emby/items/{_itemId}/images/{_type.ToString()}";
    }
}