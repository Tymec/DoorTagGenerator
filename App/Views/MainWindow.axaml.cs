using Avalonia.Controls;
using Avalonia.Styling;

namespace App.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();

        ThemeToggle.IsChecked =
            App.Current?.GetValue(ThemeVariantScope.ActualThemeVariantProperty) == ThemeVariant.Dark;
    }
}
