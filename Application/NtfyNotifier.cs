using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Emby.Notifications;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller;
using MediaBrowser.Model.Logging;
using Tielvh.Emby.Notification.Ntfy.Configuration;
using Tielvh.Emby.Notification.Ntfy.QuotedPrintable;

namespace Tielvh.Emby.Notification.Ntfy.Application
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
            var configResolver = new ConfigurationResolver(request.Configuration.Options);
            
            var quotedPrintableEncoder = new QuotedPrintableEncoder();
            var encodedTitle = quotedPrintableEncoder.Encode(request.Title);

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = configResolver.NtfyEndpoint,
                RequestHeaders =
                {
                    { "Authorization", $"Bearer {configResolver.AccessToken}" },
                    { "X-Title", encodedTitle },
                    {
                        "X-Icon",
                        "https://raw.githubusercontent.com/MediaBrowser/Emby.Resources/16cf411dddf34000a64ee10a41bffd87b45f8d18/images/Logos/logoicon114.png"
                    }
                },
                RequestHttpContent = new StringContent(request.Description ?? string.Empty),
                CancellationToken = cancellationToken
            };
            _logger.Info("Ntfy notification to {configResolver.Url} - {request.Title} - {request.Description}", configResolver.Url, request.Title,
                request.Description);

            using var httpResponse = await _httpClient.Post(httpRequestOptions);
            _logger.Info("Response status code {httpResponse.StatusCode.ToString()}", httpResponse.StatusCode.ToString());
        }
    }
}