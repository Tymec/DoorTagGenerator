using System;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Binding;
using App.Models;
using App.Helpers;
using ReactiveUI;

namespace App.ViewModels;

public partial class PreviewViewModel : ViewModelBase {
    [ObservableProperty]
    private Bitmap? _documentImage;

    public Configuration Config { get; }

    public PreviewViewModel(Configuration config) {
        Config = config;

        // config.WhenAnyPropertyChanged()
        // .Throttle(TimeSpan.FromMilliseconds(500))
        // .ObserveOn(RxApp.MainThreadScheduler)
        // .Subscribe(BuildDocument);

        // Dont block the UI thread
        config.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.TaskpoolScheduler) // NOTE: What does this do?
            .Subscribe(BuildDocument);

        BuildDocument();
    }

    private void BuildDocument(object? _ = null) {
        var doc = DocumentBuilder.Build(Config);

        var images = doc.ToBitmap();
        if (images != null && images.Count > 0) {
            DocumentImage = images[0];
        }
    }
}
