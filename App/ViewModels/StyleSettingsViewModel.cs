using App.Models;

namespace App.ViewModels;

public partial class StyleSettingsViewModel : ViewModelBase {
    public DoorTag Tag { get; }

    public StyleSettingsViewModel(DoorTag tag) {
        Tag = tag;
    }
}
