using MediaBrowser.Model.Plugins;

namespace Emby.Notification.Ntfy.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string Url { get; set; }
        public string Topic { get; set; }
        public string AccessToken { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Topic) && !string.IsNullOrEmpty(AccessToken);
        }
    }
}