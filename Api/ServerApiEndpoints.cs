using System.Net.Http;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;

namespace Emby.Notification.Ntfy.Api
{
    [Route("/Notifications/Ntfy/Test", "POST", Summary = "Test Ntfy Notification")]
    public class TestNotificationRequest : IReturnVoid
    {
    }

    [Authenticated]
    public class ServerApiEndpoints : IService
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public ServerApiEndpoints(IHttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public void Post(TestNotificationRequest request)
        {
            var configuration = Plugin.Instance.Configuration;
            var ntfyEndpoint = $"{configuration.Url}/{configuration.Topic}";

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = ntfyEndpoint,
                RequestHeaders =
                {
                    { "Authorization", $"Bearer {configuration.AccessToken}" },
                    { "X-Title", "Emby notification test" },
                    { "X-Icon", "https://emby.media/community/uploads/inline/44692/560bd1408fc27_MB3_512_423.png" }
                },
                RequestHttpContent =
                    new StringContent("If you see this message, it means your Emby ntfy notification setup is working")
            };
            _logger.Debug("Sending ntfy test notification to {0}", configuration.Url);

            var httpResponseTask = _httpClient.Post(httpRequestOptions);
            httpResponseTask.Wait();
            using var httpResponse = httpResponseTask.Result;
            _logger.Debug("Response status code {}", httpResponse.StatusCode);
        }
    }
}