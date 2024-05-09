using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emby.Notifications;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller;
using MediaBrowser.Model.Logging;
using Tielvh.Emby.Notification.Ntfy.Configuration;
using Tielvh.Emby.Notification.Ntfy.Notification;

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

            var ntfyRequest = new NtfyNotificationRequestBuilder()
                .WithTitle(request.Title)
                .WithDescription(request.Description)
                .WithInstance(configResolver.Url)
                .WithTopic(configResolver.Topic)
                .WithAccessToken(configResolver.AccessToken)
                .WithCancellationToken(cancellationToken)
                .Build();

            _logger.Info("Ntfy notification to {url} - {title} - {description}", configResolver.Url, request.Title,
                request.Description);
            using var httpResponse = await _httpClient.Post(ntfyRequest.ToHttpRequestOptions());
            _logger.Info("Response status code {statusCode}", httpResponse.StatusCode.ToString());
        }
    }
}