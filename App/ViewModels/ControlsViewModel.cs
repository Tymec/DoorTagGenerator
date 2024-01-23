using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using App.Helpers;
using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;

namespace App.ViewModels;

public partial class ControlsViewModel : ViewModelBase {
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public DoorTag Tag { get; }

    public ControlsViewModel(DoorTag tag) {
        Tag = tag;
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
            DoorTag? tag = await JsonSerializer.DeserializeAsync<DoorTag>(readStream, _jsonOptions, token)
                ?? throw new Exception("Invalid file format.");

            // Copy instead of assigning to keep the same instance.
            Tag.CopyFrom(tag);
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
            await JsonSerializer.SerializeAsync(writeStream, Tag, _jsonOptions, token);
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
                var doc = Tag.ToDocument();

                string tempFile = Path.GetTempFileName() + ".xps";
                doc.GenerateXps(tempFile);

                return tempFile;
            }, token);

            Console.WriteLine($"Printing {path}");

            if (!PrinterHelper.Print(path)) {
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
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }
}
