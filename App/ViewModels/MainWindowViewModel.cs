using App.Models;
using DynamicData.Binding;
using ReactiveUI;

namespace App.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public PreviewViewModel Preview { get; }
    public SettingsViewModel Settings { get; }

    public MainWindowViewModel() {
        Configuration config = new();
        Preview = new PreviewViewModel(config);
        Settings = new SettingsViewModel(config);
    }
}
