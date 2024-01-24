using Avalonia.Controls;
using Avalonia.Styling;

namespace App.Views;

public partial class ControlsView : UserControl {
    public ControlsView() {
        InitializeComponent();

        ThemeToggle.IsChecked =
            App.Current?.GetValue(ThemeVariantScope.ActualThemeVariantProperty) == ThemeVariant.Dark;
    }
}
