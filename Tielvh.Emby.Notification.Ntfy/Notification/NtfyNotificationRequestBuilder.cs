using System;
using System.Threading;
using Tielvh.Emby.Notification.Ntfy.Configuration;
using Tielvh.Emby.Notification.Ntfy.QuotedPrintable;

namespace Tielvh.Emby.Notification.Ntfy.Notification
{
    public class NtfyNotificationRequestBuilder
    {
        private readonly NtfyNotificationRequest _notificationRequest = new();

        public NtfyNotificationRequestBuilder WithTitle(string title)
        {
            var encoder = new QuotedPrintableEncoder { Charset = Charset.Utf8, Encoding = Encoding.B64 };
            _notificationRequest.Title = encoder.Encode(title);
            return this;
        }

        public NtfyNotificationRequestBuilder WithDescription(string? description)
        {
            _notificationRequest.Description = description;
            return this;
        }

        public NtfyNotificationRequestBuilder WithAccessToken(string? accessToken)
        {
            _notificationRequest.AuthorizationHeader =
                accessToken is not null ? string.Concat("Bearer", ' ', accessToken) : null;
            return this;
        }

        private string? _instance;

        public NtfyNotificationRequestBuilder WithInstance(string instance)
        {
            _ = new Uri(instance);
            _instance = instance;
            return this;
        }

        private string? _topic;

        public NtfyNotificationRequestBuilder WithTopic(string topic)
        {
            _topic = topic;
            return this;
        }

        public NtfyNotificationRequestBuilder WithCancellationToken(CancellationToken cancellationToken)
        {
            _notificationRequest.CancellationToken = cancellationToken;
            return this;
        }

        public NtfyNotificationRequestBuilder WithIcon(Image? icon)
        {
            _notificationRequest.IconUrl = icon?.StaticUrl;
            return this;
        }

        private void Validate()
        {
            if (_instance is null) throw new InvalidOperationException("Instance is not set");
            if (_topic is null) throw new InvalidOperationException("Topic is not set");
            if (_instance == ConfigurationResolver.DefaultNtfyInstanceUrl &&
                _notificationRequest.AuthorizationHeader is not null)
                throw new InvalidOperationException("Access token may not be set when using the default ntfy instance");
            if (_notificationRequest.Title is null) throw new InvalidOperationException("Title is not set");
        }

        public NtfyNotificationRequest Build()
        {
            Validate();
            _notificationRequest.Endpoint = FormattedEndpoint;
            return _notificationRequest;
        }

        private string FormattedEndpoint => string.Concat(_instance!.TrimEnd('/'), '/', _topic);
    }
}