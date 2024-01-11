using System.ComponentModel;
using App.Models;

namespace App.ViewModels;


public class SettingsViewModel : ViewModelBase {
    public Configuration Config { get; set; }

    public SettingsViewModel(Configuration config) {
        Config = config;
    }
}
