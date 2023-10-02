using System;
using System.Collections.Generic;
using Emby.Notification.Ntfy.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Emby.Notification.Ntfy
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths,
            xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "Ntfy Notifications";

        public override Guid Id => new("0ecaedaf-b310-4fc5-95be-367216652f70");

        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            yield return new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = GetType().Namespace + ".Configuration.config.html",
                EnableInMainMenu = true,
                MenuSection = "server",
                MenuIcon = "chat"
            };
        }
    }
}