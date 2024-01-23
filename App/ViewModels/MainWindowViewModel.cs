using System;
using System.Threading;
using System.Threading.Tasks;
using App.Models;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    public PreviewViewModel Preview { get; }
    public ValueSettingsViewModel ValueSettings { get; }
    public StyleSettingsViewModel StyleSettings { get; }
    public PositionSettingsViewModel PositionSettings { get; }
    public ControlsViewModel Controls { get; }

    public MainWindowViewModel() {
        DoorTag tag = new();

        Preview = new(tag);

        ValueSettings = new(tag);
        StyleSettings = new(tag);
        PositionSettings = new(tag);

        Controls = new(tag);
    }

    [RelayCommand]
    private Task SwitchTheme(bool dark, CancellationToken token) {
        ErrorMessages?.Clear();

        var theme = dark ? ThemeVariant.Dark : ThemeVariant.Light;
        try {
            App.Current?.SetValue(ThemeVariantScope.ActualThemeVariantProperty, theme);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }

        return Task.CompletedTask;
    }
}
