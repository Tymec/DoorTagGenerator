using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace App.Services;

public class FileService(Window target) {
    public static readonly FilePickerFileType Json = new("JSON") {
        Patterns = ["*.json"],
        AppleUniformTypeIdentifiers = ["public.json"],
        MimeTypes = ["application/json"]
    };

    public async Task<IStorageFile?> OpenFile(string title = "Open File", params FilePickerFileType[] types) {
        var files = await target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions() {
            Title = title,
            FileTypeFilter = types,
            AllowMultiple = false
        });

        return files.Count >= 1 ? files[0] : null;
    }

    public async Task<IStorageFile?> SaveFile(string title = "Save File", string suggestedName = "output", string defaultExtension = ".txt", params FilePickerFileType[] types) {
        return await target.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions() {
            Title = title,
            SuggestedFileName = suggestedName,
            DefaultExtension = defaultExtension,
            FileTypeChoices = types,
            ShowOverwritePrompt = true
        });
    }
}
