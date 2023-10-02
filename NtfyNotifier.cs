using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Notifications;
using MediaBrowser.Model.Logging;

namespace Emby.Notification.Ntfy
{
    public class NtfyNotifier : INotificationService
    {
        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;

        public NtfyNotifier(ILogger logger, IHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task SendNotification(UserNotification request, CancellationToken cancellationToken)
        {
            var configuration = Plugin.Instance.Configuration;
            var ntfyEndpoint = $"{configuration.Url}/{configuration.Topic}";

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = ntfyEndpoint,
                RequestHeaders =
                {
                    { "Authorization", $"Bearer {configuration.AccessToken}" },
                    { "X-Title", request.Name },
                    { "X-Icon", "https://emby.media/favicon-32x32.png" }
                },
                RequestHttpContent = new StringContent(request.Description ?? string.Empty),
                CancellationToken = cancellationToken
            };
            _logger.Debug("Ntfy notification to {0} - {1} - {2}", configuration.Url, request.Name, request.Description);

            using var httpResponse = await _httpClient.Post(httpRequestOptions);
            _logger.Debug("Response status code {0}", httpResponse.StatusCode.ToString());
        }

        public bool IsEnabledForUser(User user)
        {
            var configuration = Plugin.Instance.Configuration;
            return configuration != null && configuration.Validate();
        }

        public string Name => Plugin.Instance.Name;
    }
}