using System;
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
using System.Linq;

namespace App.ViewModels;

public partial class ValueSettingsViewModel : ViewModelBase {

    [ObservableProperty]
    private string _logoUrl = string.Empty;

    [ObservableProperty]
    private string _roomMember = string.Empty;

    public DoorTag Tag { get; }

    public ValueSettingsViewModel(DoorTag tag) {
        Tag = tag;

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
            var image = await Utils.LoadFromUrl(new Uri(url)) ?? throw new Exception("Invalid image format.");
            Tag.Logo.Data = image;
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(CanAddRoomMember))]
    private async Task AddRoomMember(string name, CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            await Task.Run(() => {
                Tag.RoomMembers.Members.Add(new() { Name = name });
                RoomMember = string.Empty;
            }, token);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    private bool CanAddRoomMember(string name) => !string.IsNullOrEmpty(name) && !Tag.RoomMembers.Members.Any(x => x.Name == name);

    [RelayCommand(CanExecute = nameof(CanRemoveRoomMember))]
    private async Task RemoveRoomMember(string name, CancellationToken token) {
        ErrorMessages?.Clear();

        try {
            await Task.Run(() => {
                var member = Tag.RoomMembers.Members.FirstOrDefault(x => x.Name == name);
                if (member is not null) {
                    Tag.RoomMembers.Members.Remove(member);
                }
            }, token);
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }

    private bool CanRemoveRoomMember(string name) => !string.IsNullOrEmpty(name) && Tag.RoomMembers.Members.Any(x => x.Name == name);

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
            Tag.Logo.Data = memoryStream.ToArray();

            LogoUrl = string.Empty;
        } catch (Exception e) {
            ErrorMessages?.Add(e.Message);
        }
    }
}
