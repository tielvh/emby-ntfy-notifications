using System.Net.Http;
using System.Threading;
using MediaBrowser.Common.Net;

namespace Tielvh.Emby.Notification.Ntfy.Notification
{
    public class NtfyNotificationRequest
    {
        internal NtfyNotificationRequest()
        {
        }

        public string? Title { get; internal set; }
        public string? Description { get; internal set; }
        public string? Url { get; internal set; }
        public string? AuthorizationHeader { get; internal set; }
        public string? Endpoint { get; internal set; }
        public CancellationToken? CancellationToken { get; internal set; }

        private const string IconUrl =
            "https://raw.githubusercontent.com/MediaBrowser/Emby.Resources/16cf411dddf34000a64ee10a41bffd87b45f8d18/images/Logos/logoicon114.png";

        private string TagsHeader => string.Concat("view, View, ", Url);

        public HttpRequestOptions ToHttpRequestOptions()
        {
            var options = new HttpRequestOptions
            {
                Url = Endpoint,
                RequestHttpContent = new StringContent(Description ?? string.Empty),
                CancellationToken = CancellationToken ?? default,
                RequestHeaders = { { "X-Icon", IconUrl }, { "X-Title", Title } }
            };
            if (AuthorizationHeader is not null) options.RequestHeaders.Add("Authorization", AuthorizationHeader);
            if (Url is not null) options.RequestHeaders.Add("X-Tags", TagsHeader);
            return options;
        }
    }
}