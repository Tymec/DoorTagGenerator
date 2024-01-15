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
    private Bitmap? _logoImage = ImageHelper.LoadFromResource("Placeholder.png");

    public Configuration Config { get; }

    public PreviewViewModel(Configuration config) {
        Config = config;

        this.WhenAnyValue(x => x.Config.LogoUrl)
            .Where(x => !string.IsNullOrEmpty(x))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(LoadLogo);
    }

    public async void LoadLogo(string url) {
        var image = await ImageHelper.LoadFromUrl(url);
        if (image is not null) {
            LogoImage = image;
        }
    }
}
