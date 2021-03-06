﻿using FluentTerminal.App.Services;
using FluentTerminal.App.Services.Utilities;
using FluentTerminal.Models;
using FluentTerminal.Models.Enums;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FluentTerminal.App.ViewModels.Settings
{
    public class MousePageViewModel : ObservableObject
    {
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IDefaultValueProvider _defaultValueProvider;
        private readonly ApplicationSettings _applicationSettings;

        public MousePageViewModel(ISettingsService settingsService, IDialogService dialogService, IDefaultValueProvider defaultValueProvider)
        {
            _settingsService = settingsService;
            _dialogService = dialogService;
            _defaultValueProvider = defaultValueProvider;
            _applicationSettings = _settingsService.GetApplicationSettings();

            RestoreDefaultsCommand = new AsyncRelayCommand(RestoreDefaultsAsync);
        }

        public bool CopyOnSelect
        {
            get => _applicationSettings.CopyOnSelect;
            set
            {
                if (_applicationSettings.CopyOnSelect != value)
                {
                    _applicationSettings.CopyOnSelect = value;
                    _settingsService.SaveApplicationSettings(_applicationSettings);
                    OnPropertyChanged();
                }
            }
        }

        public MouseAction MouseRightClickAction
        {
            get => _applicationSettings.MouseRightClickAction;
            set
            {
                if (_applicationSettings.MouseRightClickAction != value)
                {
                    _applicationSettings.MouseRightClickAction = value;
                    _settingsService.SaveApplicationSettings(_applicationSettings);
                    OnPropertyChanged();
                }
            }
        }

        public bool MouseRightClickNoneIsSelected
        {
            get => MouseRightClickAction == MouseAction.None;
            set { if (value) MouseRightClickAction = MouseAction.None; }
        }

        public bool MouseRightClickContextMenuIsSelected
        {
            get => MouseRightClickAction == MouseAction.ContextMenu;
            set { if (value) MouseRightClickAction = MouseAction.ContextMenu; }
        }

        public bool MouseRightClickPasteIsSelected
        {
            get => MouseRightClickAction == MouseAction.Paste;
            set { if (value) MouseRightClickAction = MouseAction.Paste; }
        }

        public bool MouseRightClickCopySelectionOrPasteIsSelected
        {
            get => MouseRightClickAction == MouseAction.CopySelectionOrPaste;
            set { if (value) MouseRightClickAction = MouseAction.CopySelectionOrPaste; }
        }

        public MouseAction MouseMiddleClickAction
        {
            get => _applicationSettings.MouseMiddleClickAction;
            set
            {
                if (_applicationSettings.MouseMiddleClickAction != value)
                {
                    _applicationSettings.MouseMiddleClickAction = value;
                    _settingsService.SaveApplicationSettings(_applicationSettings);
                    OnPropertyChanged();
                }
            }
        }

        public bool MouseMiddleClickNoneIsSelected
        {
            get => MouseMiddleClickAction == MouseAction.None;
            set { if (value) MouseMiddleClickAction = MouseAction.None; }
        }

        public bool MouseMiddleClickContextMenuIsSelected
        {
            get => MouseMiddleClickAction == MouseAction.ContextMenu;
            set { if (value) MouseMiddleClickAction = MouseAction.ContextMenu; }
        }

        public bool MouseMiddleClickPasteIsSelected
        {
            get => MouseMiddleClickAction == MouseAction.Paste;
            set { if (value) MouseMiddleClickAction = MouseAction.Paste; }
        }

        public bool MouseMiddleClickCopySelectionOrPasteIsSelected
        {
            get => MouseMiddleClickAction == MouseAction.CopySelectionOrPaste;
            set { if (value) MouseMiddleClickAction = MouseAction.CopySelectionOrPaste; }
        }

        public ICommand RestoreDefaultsCommand { get; }

        // Requires UI thread
        private async Task RestoreDefaultsAsync()
        {
            // ConfigureAwait(true) because we're setting some view-model properties afterwards.
            var result = await _dialogService.ShowMessageDialogAsync(I18N.Translate("PleaseConfirm"),
                    I18N.Translate("ConfirmRestoreMouseSettings"), DialogButton.OK, DialogButton.Cancel)
                .ConfigureAwait(true);

            if (result == DialogButton.OK)
            {
                var defaults = _defaultValueProvider.GetDefaultApplicationSettings();
                CopyOnSelect = defaults.CopyOnSelect;
                MouseMiddleClickAction = defaults.MouseMiddleClickAction;
                MouseRightClickAction = defaults.MouseRightClickAction;
            }
        }
    }
}
