using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using App.Models;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Avalonia.Platform.Storage;
using System.Drawing;

namespace App.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public Configuration Config { get; private set; }

    public SettingsViewModel(Configuration config) {
        Config = config;
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

        // TODO: Generate PDF using QuestPDF

        try {
            var filesService = (
                App.Current?.Services?.GetService<FileService>()
            ) ?? throw new NullReferenceException("Missing FileService instance.");

            var printService = (
                App.Current?.Services?.GetService<PrintService>()
            ) ?? throw new NullReferenceException("Missing PrintService instance.");

            var file = await filesService.SaveFile(
                title: "Save Image File",
                suggestedName: "preview",
                defaultExtension: ".png",
                FilePickerFileTypes.ImageAll
            );
            if (file is null) return;

            await using var writeStream = await file.OpenWriteAsync();
            printService.Rasterize("Preview", writeStream);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }
}
