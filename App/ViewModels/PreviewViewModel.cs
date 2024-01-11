using System.ComponentModel;
using App.Models;

namespace App.ViewModels;

public class PreviewViewModel : ViewModelBase {
    public Configuration Config { get; set; }

    public PreviewViewModel(Configuration configuration) {
        Config = configuration;

        Config.PropertyChanged += Config_OnPropertyChanged;
    }

    private void Config_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        // this.RaisePropertyChanged(e.PropertyName);
    }
}
