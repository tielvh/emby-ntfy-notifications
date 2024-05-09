using System;
using System.Threading;
using Tielvh.Emby.Notification.Ntfy.QuotedPrintable;

namespace Tielvh.Emby.Notification.Ntfy.Notification
{
    public class NtfyNotificationRequestBuilder
    {
        private const string AuthorizationType = "Bearer";
        private readonly NtfyNotificationRequest _notificationRequest = new();

        public NtfyNotificationRequestBuilder WithTitle(string title)
        {
            var encoder = new QuotedPrintableEncoder { Charset = Charset.Utf8, Encoding = Encoding.B64 };
            _notificationRequest.Title = encoder.Encode(title);
            return this;
        }

        public NtfyNotificationRequestBuilder WithDescription(string description)
        {
            _notificationRequest.Description = description;
            return this;
        }

        public NtfyNotificationRequestBuilder WithUrl(string url)
        {
            _notificationRequest.Url = url;
            return this;
        }

        public NtfyNotificationRequestBuilder WithAccessToken(string? accessToken)
        {
            if (accessToken is not null)
                _notificationRequest.AuthorizationHeader = string.Concat(AuthorizationType, ' ', accessToken);
            return this;
        }

        public NtfyNotificationRequestBuilder WithEndpoint(string endpoint)
        {
            _ = new Uri(endpoint);
            _notificationRequest.Endpoint = endpoint;
            return this;
        }

        public NtfyNotificationRequestBuilder WithCancellationToken(CancellationToken cancellationToken)
        {
            _notificationRequest.CancellationToken = cancellationToken;
            return this;
        }

        private void Validate()
        {
            if (_notificationRequest.Title is null) throw new InvalidOperationException("Title is not set");
            if (_notificationRequest.Endpoint is null) throw new InvalidOperationException("Endpoint is not set");
        }

        public NtfyNotificationRequest Build()
        {
            Validate();
            return _notificationRequest;
        }
    }
}