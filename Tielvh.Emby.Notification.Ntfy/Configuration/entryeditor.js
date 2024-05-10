define(['pluginManager', 'emby-input'], function (pluginManager) {
    'use strict';

    function EntryEditor() {
    }

    EntryEditor.setObjectValues = function (context, entry) {
        entry.FriendlyName = context.querySelector('.txtFriendlyName').value;
        entry.Options.Token = context.querySelector('.txtToken').value;
        entry.Options.Url = context.querySelector('.txtUrl').value;
        entry.Options.Topic = context.querySelector('.txtTopic').value;
        entry.Options.HostUrl = context.querySelector('.txtHostUrl').value;
    };

    EntryEditor.setFormValues = function (context, entry) {
        context.querySelector('.txtFriendlyName').value = entry.FriendlyName || '';
        context.querySelector('.txtToken').value = entry.Options.Token || '';
        context.querySelector('.txtUrl').value = entry.Options.Url || '';
        context.querySelector('.txtTopic').value = entry.Options.Topic || '';
        context.querySelector('.txtHostUrl').value = entry.Options.HostUrl || '';
    };

    EntryEditor.loadTemplate = function (context) {
        return require(['text!' + pluginManager.getConfigurationResourceUrl('ntfynotificationtemplate')]).then(responses => context.innerHTML = responses[0]);
    };

    EntryEditor.destroy = function () {
    };

    return EntryEditor;
});