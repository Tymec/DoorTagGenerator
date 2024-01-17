using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Themes.Neumorphism.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Binding;
using ReactiveUI;

namespace App.ViewModels;

public partial class ViewModelBase : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<string> _errorMessages = [];

    protected ViewModelBase() {
        ErrorMessages.ToObservableChangeSet()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => ShowToast());
    }

    private void ShowToast() {
        if (ErrorMessages.Count > 0) {
            SnackbarHost.Post(ErrorMessages[0]);
        }
    }
}
