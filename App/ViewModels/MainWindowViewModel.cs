using App.Models;

namespace App.ViewModels;

public class MainWindowViewModel : ViewModelBase {
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
}
