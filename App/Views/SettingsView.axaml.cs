using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using App.ViewModels;

namespace App.Views;

public partial class SettingsView : UserControl {
    public SettingsView() {
        InitializeComponent();

        LogoUrlInput?.AddHandler(TextInputEvent, logoUrl_OnTextChanged, RoutingStrategies.Tunnel);
    }

    private void logoUrl_OnTextChanged(object? sender, TextInputEventArgs e) {
        if (DataContext is SettingsViewModel vm) {
            vm.Config.LogoUrl = LogoUrlInput.Text!;
        }
    }
}
