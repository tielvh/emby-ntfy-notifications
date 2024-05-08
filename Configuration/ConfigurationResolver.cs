using System;
using System.Collections.Generic;

namespace Tielvh.Emby.Notification.Ntfy.Configuration
{
    public class ConfigurationResolver
    {
        private readonly string _url;
        private const string DefaultNtfyInstanceUrl = "https://ntfy.sh";
        public string Url => _url == String.Empty ? DefaultNtfyInstanceUrl : _url;
        public string NtfyEndpoint => string.Concat(Url.TrimEnd('/'), Topic);
        
        private readonly string _accessToken;
        public string? AccessToken => _accessToken == string.Empty ? null : _accessToken;
        
        private string Topic { get; }

        public ConfigurationResolver(IDictionary<string, string> options)
        {
            _url = options[ConfigurationKeys.Url];
            _accessToken = options[ConfigurationKeys.AccessToken];
            Topic = options[ConfigurationKeys.Topic];
        }
        
        private static class ConfigurationKeys
        {
            public const string Url = "Url";
            public const string AccessToken = "Token";
            public const string Topic = "Topic";
        }
    }
}