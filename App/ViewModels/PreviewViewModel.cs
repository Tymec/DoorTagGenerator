using System.ComponentModel;
using App.Models;

namespace App.ViewModels;

public class PreviewViewModel : ViewModelBase {
    public Configuration Config { get; set; }

    public PreviewViewModel(Configuration config) {
        Config = config;
    }
}
