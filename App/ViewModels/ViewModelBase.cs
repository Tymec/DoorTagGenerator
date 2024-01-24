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
    private ObservableCollection<string> _messages = [];

    protected ViewModelBase() {
        Messages.ToObservableChangeSet()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => ShowToast());
    }

    private void ShowToast() {
        if (Messages.Count > 0) {
            SnackbarHost.Post(Messages[0]);
            Console.WriteLine($"Toast: {Messages[0]}");
        }
    }
}
