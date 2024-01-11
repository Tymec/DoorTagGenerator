using System.ComponentModel;
using App.Models;

namespace App.ViewModels;


public class SettingsViewModel : ViewModelBase {
    public Configuration Config { get; set; }

    public SettingsViewModel(Configuration configuration) {
        Config = configuration;

        Config.PropertyChanged += Config_OnPropertyChanged;
    }

    private void Config_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        // this.RaisePropertyChanged(e.PropertyName);
    }
}
