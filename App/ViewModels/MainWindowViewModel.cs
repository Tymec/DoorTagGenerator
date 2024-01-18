using App.Models;

namespace App.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public PreviewViewModel Preview { get; }
    public SettingsViewModel Settings { get; }

    public MainWindowViewModel() {
        Configuration config = new();
        Preview = new(config);
        Settings = new(config);
    }
}
