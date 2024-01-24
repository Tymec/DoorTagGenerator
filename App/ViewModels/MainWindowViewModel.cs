using App.Models;

namespace App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    public PreviewViewModel Preview { get; }
    public ValueSettingsViewModel ValueSettings { get; }
    public StyleSettingsViewModel StyleSettings { get; }
    public PositionSettingsViewModel PositionSettings { get; }
    public ControlsViewModel Controls { get; }

    public MainWindowViewModel() {
        var tag = DoorTag.Default();

        Preview = new(tag);
        ValueSettings = new(tag);
        StyleSettings = new(tag);
        PositionSettings = new(tag);
        Controls = new(tag);
    }
}
