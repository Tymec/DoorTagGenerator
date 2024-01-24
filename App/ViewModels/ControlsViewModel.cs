using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;
using App.Services;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;

namespace App.ViewModels;

public partial class ControlsViewModel(DoorTag tag) : ViewModelBase {
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public DoorTag Tag { get; } = tag;

    [RelayCommand]
    private async Task LoadFile(CancellationToken token) {
        Messages?.Clear();

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
            DoorTag? tag = await JsonSerializer.DeserializeAsync<DoorTag>(readStream, _jsonOptions, token)
                ?? throw new Exception("Invalid file format.");

            // Copy instead of assigning to keep the same instance.
            Tag.CopyFrom(tag);

            Messages?.Add("Loaded successfully.");
        } catch (Exception e) {
            Messages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task SaveFile(CancellationToken token) {
        Messages?.Clear();

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
            await JsonSerializer.SerializeAsync(writeStream, Tag, _jsonOptions, token);

            Messages?.Add("Saved successfully.");
        } catch (Exception e) {
            Messages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task Print(CancellationToken token) {
        Messages?.Clear();

        try {
#if WINDOWS
            string path = await Task.Run(() => {
                var doc = Tag.ToDocument();

                string tempFile = Path.GetTempFileName() + ".xps";
                doc.GenerateXps(tempFile);

                return tempFile;
            }, token);

            Console.WriteLine($"Printing {path}");

            bool? result = PrinterHelper.Print(path);
            if (result == null) {
                return;
            } else if (result == false) {
                throw new Exception("Printing failed.");
            }
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
            var doc = Tag.ToDocument() ?? throw new Exception("Failed to build document.");
            doc.GeneratePdf(writeStream);
#endif
            Messages?.Add("Printed successfully.");
        } catch (Exception e) {
            // IDEA: Show error dialog
            Messages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task ResetToDefaults(CancellationToken token) {
        Messages?.Clear();

        try {
            var dialogService = (
                App.Current?.Services?.GetService<DialogService>()
            ) ?? throw new NullReferenceException("Missing DialogService instance.");

            var result = await dialogService.Confirm("reset");
            if (result?.GetResult == "true") {
                Tag.CopyFrom(DoorTag.Default());
                Messages?.Add("Reset to default values.");
            }
        } catch (Exception e) {
            Messages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private Task SwitchTheme(bool dark, CancellationToken token) {
        Messages?.Clear();

        var theme = dark ? ThemeVariant.Dark : ThemeVariant.Light;
        try {
            App.Current?.SetValue(ThemeVariantScope.ActualThemeVariantProperty, theme);
        } catch (Exception e) {
            Messages?.Add(e.Message);
        }

        return Task.CompletedTask;
    }
}
