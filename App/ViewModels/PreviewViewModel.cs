using System;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Binding;
using App.Models;
using App.Helpers;

namespace App.ViewModels;

public partial class PreviewViewModel : ViewModelBase {
    [ObservableProperty]
    private Bitmap? _documentImage;

    public Configuration Config { get; }

    public PreviewViewModel(Configuration config) {
        Config = config;
        config.WhenAnyPropertyChanged().Subscribe(BuildDocument);
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
