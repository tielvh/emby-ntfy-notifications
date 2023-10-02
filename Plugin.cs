using System;
using System.Collections.Generic;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;

namespace Emby.Notification.Ntfy
{
    public class Plugin : BasePlugin, IHasWebPages
    {
        public const string StaticName = "Ntfy";
        
        public override string Name => $"{StaticName} Notifications";

        public override Guid Id => new("0ecaedaf-b310-4fc5-95be-367216652f70");

        private const string EditorJsName = "ntfynotificationeditorjs";

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "ntfynotificationtemplate",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.entryeditor.template.html",
                    IsMainConfigPage = false
                },
                new PluginPageInfo
                {
                    Name = EditorJsName,
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.entryeditor.js"
                }
            };
        }
        
        public string NotificationSetupModuleUrl => GetPluginPageUrl(EditorJsName);
    }
}