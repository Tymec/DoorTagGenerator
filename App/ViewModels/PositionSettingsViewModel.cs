using App.Models;

namespace App.ViewModels;

public partial class PositionSettingsViewModel : ViewModelBase {
    public DoorTag Tag { get; }

    public PositionSettingsViewModel(DoorTag tag) {
        Tag = tag;
    }
}
