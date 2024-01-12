using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace App.ViewModels;

public class PreviewViewModel : ViewModelBase {
    private static readonly HttpClient _httpClient = new();

    private Bitmap? _logoImage = ImageHelper.LoadFromResource("Placeholder.png");

    public Configuration Config { get; }

    public Bitmap? LogoImage {
        get => _logoImage;
        private set => this.RaiseAndSetIfChanged(ref _logoImage, value);
    }

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
