
using App.Models;

namespace App.ViewModels;
public partial class SettingsViewModel : ViewModelBase {
    public MainSettingsViewModel Main { get; }
    public StyleSettingsViewModel Style { get; }

    public SettingsViewModel(Configuration config) {
        Main = new(config);
        Style = new(config);
    }
}
