using App.Models;

namespace App.ViewModels;

public partial class StyleSettingsViewModel : ViewModelBase {
    public Configuration Config { get; private set; }

    public StyleSettingsViewModel(Configuration config) {
        Config = config;
    }
}
