using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using App.Models;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System.Reactive.Linq;
using ReactiveUI;
using App.Helpers;
using QuestPDF.Fluent;
using System.Linq;

namespace App.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    [ObservableProperty]
    private string _logoUrl = string.Empty;

    [ObservableProperty]
    private string _roomMember = string.Empty;

    public Configuration Config { get; private set; }

    public SettingsViewModel(Configuration config) {
        Config = config;

        this.WhenAnyValue(x => x.LogoUrl)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Where(x => !string.IsNullOrEmpty(x) && Uri.TryCreate(x, UriKind.Absolute, out var _))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(LoadLogoUrl);
    }

    private async void LoadLogoUrl(string url) {
        Console.WriteLine($"Loading logo from {url}");

        ErrorMessages?.Clear();

        try {
            var image = await ImageHelper.LoadFromUrl(new Uri(url)) ?? throw new Exception("Invalid image format.");
            Config.Logo = image;
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(CanAddRoomMember))]
    private async Task AddRoomMember(string name, CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            await Task.Run(() => {
                Config.RoomMembers.Add(new Person() { Name = name });
                RoomMember = string.Empty;
            }, token);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    private bool CanAddRoomMember(string name) {
        return !string.IsNullOrEmpty(name) && !Config.RoomMembers.Any(x => x.Name == name);
    }

    [RelayCommand(CanExecute = nameof(CanRemoveRoomMember))]
    private async Task RemoveRoomMember(string name, CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            await Task.Run(() => {
                var member = Config.RoomMembers.FirstOrDefault(x => x.Name == name);
                if (member is not null) {
                    Config.RoomMembers.Remove(member);
                }
            }, token);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    private bool CanRemoveRoomMember(string name) {
        return !string.IsNullOrEmpty(name) && Config.RoomMembers.Any(x => x.Name == name);
    }

    [RelayCommand]
    private async Task LoadLogoFile(CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            var filesService = (
                App.Current?.Services?.GetService<FileService>()
            ) ?? throw new NullReferenceException("Missing FileService instance.");

            var file = await filesService.OpenFile(
                title: "Select logo image",
                FilePickerFileTypes.ImageAll
            );
            if (file is null) return;

            await using var fileStream = await file.OpenReadAsync();
            using var memoryStream = new MemoryStream();

            fileStream.CopyTo(memoryStream);
            Config.Logo = memoryStream.ToArray();

            LogoUrl = string.Empty;
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task LoadFile(CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            var filesService = (
                App.Current?.Services?.GetService<FileService>()
            ) ?? throw new NullReferenceException("Missing FileService instance.");

            var file = await filesService.OpenFile(
                title: "Open Configuration File",
                FileService.Json
            );
            if (file is null) return;

            await using var readStream = await file.OpenReadAsync();
            Configuration? cfg = await JsonSerializer.DeserializeAsync<Configuration>(readStream, _jsonOptions, token)
                ?? throw new Exception("Invalid file format.");

            // Copy instead of assigning to keep the same instance.
            Config.CopyFrom(cfg);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task SaveFile(CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            var filesService = (
                App.Current?.Services?.GetService<FileService>()
            ) ?? throw new NullReferenceException("Missing FileService instance.");

            var file = await filesService.SaveFile(
                title: "Save Configuration File",
                suggestedName: "config",
                defaultExtension: ".json",
                FileService.Json
            );
            if (file is null) return;

            await using var writeStream = await file.OpenWriteAsync();
            await JsonSerializer.SerializeAsync(writeStream, Config, _jsonOptions, token);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task Print(CancellationToken token) {
        ErrorMessages?.Clear();

        try {
#if WINDOWS
            string path = await Task.Run(() => {
                var doc = DocumentBuilder.Build(Config);

                string tempFile = Path.GetTempFileName() + ".xps";
                doc.GenerateXps(tempFile);

                return tempFile;
            }, token);

            if (!PrinterHelper.Print(path)) {
                throw new Exception("Printing failed.");
            }

            File.Delete(path);
#else
            var filesService = (
                App.Current?.Services?.GetService<FileService>()
            ) ?? throw new NullReferenceException("Missing FileService instance.");

            var file = await filesService.SaveFile(
                title: "Save PDF File",
                suggestedName: "document",
                defaultExtension: ".pdf",
            FilePickerFileTypes.Pdf
            );
            if (file is null) return;

            await using var writeStream = await file.OpenWriteAsync();
            var doc = DocumentBuilder.Build(Config);
            doc.GeneratePdf(writeStream);
#endif
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }
}
