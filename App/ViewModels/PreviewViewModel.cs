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

        config.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        config.RoomMembers.ToObservableChangeSet()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        BuildDocument();
    }

    private void BuildDocument() {
        var doc = DocumentBuilder.Build(Config);

        var images = doc.ToBitmap();
        if (images != null && images.Count > 0) {
            DocumentImage = images[0];
        }
    }
}
