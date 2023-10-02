using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Emby.Notifications;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller;
using MediaBrowser.Model.Logging;

namespace Emby.Notification.Ntfy
{
    public class NtfyNotifier : IUserNotifier
    {
        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;
        private readonly IServerApplicationHost _appHost;

        public NtfyNotifier(ILogger logger, IHttpClient httpClient, IServerApplicationHost appHost)
        {
            _logger = logger;
            _httpClient = httpClient;
            _appHost = appHost;
        }

        private Plugin Plugin => _appHost.Plugins.OfType<Plugin>().First();

        public string Key => "ntfynotifications";

        public string Name => Plugin.StaticName;
        public string SetupModuleUrl => Plugin.NotificationSetupModuleUrl;

        public async Task SendNotification(InternalNotificationRequest request, CancellationToken cancellationToken)
        {
            var options = request.Configuration.Options;
            var url = options["Url"];
            var topic = options["Topic"];
            var accessToken = options["Token"];
            var ntfyEndpoint = $"{url}/{topic}";

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = ntfyEndpoint,
                RequestHeaders =
                {
                    { "Authorization", $"Bearer {accessToken}" },
                    { "X-Title", request.Title },
                    {
                        "X-Icon",
                        "https://raw.githubusercontent.com/MediaBrowser/Emby.Resources/16cf411dddf34000a64ee10a41bffd87b45f8d18/images/Logos/logoicon114.png"
                    }
                },
                RequestHttpContent = new StringContent(request.Description ?? string.Empty),
                CancellationToken = cancellationToken
            };
            _logger.Debug("Ntfy notification to {0} - {1} - {2}", url, request.Title,
                request.Description);

            using var httpResponse = await _httpClient.Post(httpRequestOptions);
            _logger.Debug("Response status code {0}", httpResponse.StatusCode.ToString());
        }
    }
}