using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels;

public partial class ViewModelBase : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<string>? _errorMessages;

    protected ViewModelBase() {
        ErrorMessages = [];
    }
}
