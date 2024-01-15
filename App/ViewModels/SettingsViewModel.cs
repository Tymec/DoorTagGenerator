using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using App.Models;
using App.Services;

namespace App.ViewModels;


public partial class SettingsViewModel : ViewModelBase {
    public Configuration Config { get; set; }

    public SettingsViewModel(Configuration config) {
        Config = config;
    }

    [RelayCommand]
    private async Task LoadFile(CancellationToken token) {
        try {
            var file = await FileService.DoOpenFilePickerAsync();
            if (file is null) return;

            // TODO: Implement deserializing config
        } catch (Exception e) {
        }
    }

    [RelayCommand]
    private async Task SaveFile(CancellationToken token) {
        var file = await FileService.DoSaveFilePickerAsync();
        if (file is null) return;

        // TODO: Serialize config
    }

    [RelayCommand]
    private async Task Print(CancellationToken token) {
    }
}
