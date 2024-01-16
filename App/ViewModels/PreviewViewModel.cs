using System;
using System.Reactive.Linq;
using App.Helpers;
using App.Models;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace App.ViewModels;

public partial class PreviewViewModel : ViewModelBase {
    [ObservableProperty]
    private Bitmap? _documentImage;

    public Configuration Config { get; }

    public PreviewViewModel(Configuration config) {
        Config = config;

        this.WhenAnyValue(x => x.Config.Logo)
            .Where(x => x != null)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(BuildDocument);

        this.WhenAnyValue(x => x.Config.RoomNumber)
            .Where(x => !string.IsNullOrEmpty(x))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(BuildDocument);

        this.WhenAnyValue(x => x.Config.RoomMembers)
            .Where(x => x.Count > 0)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(BuildDocument);
    }

    private void BuildDocument(object? _) {
        var doc = DocumentBuilder.Build(Config);
        var images = doc.ToImage();
        if (images != null && images.Count > 0) {
            DocumentImage = images[0];
        }
    }
}
