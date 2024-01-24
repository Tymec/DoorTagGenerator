using System;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Binding;
using App.Models;
using App.Helpers;
using ReactiveUI;

namespace App.ViewModels;

public partial class PreviewViewModel : ViewModelBase {
    [ObservableProperty]
    private Bitmap? _documentImage;

    public DoorTag Tag { get; }

    public PreviewViewModel(DoorTag tag) {
        Tag = tag;

        // Watch tag properties for changes
        Tag.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        // Watch properties of tag properties for changes
        Tag.Logo.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        Tag.RoomNumber.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        Tag.RoomMembers.WhenAnyPropertyChanged()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        // Watch collection
        Tag.RoomMembers.Members.ToObservableChangeSet()
            .Throttle(TimeSpan.FromMilliseconds(200))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => BuildDocument());

        BuildDocument();
    }

    private void BuildDocument() {
        Messages?.Clear();
        try {
            var doc = Tag.ToDocument();

            var images = doc.ToBitmap();
            if (images != null && images.Count > 0) {
                DocumentImage = images[0];
            }
        } catch (Exception e) {
            Messages?.Add(e.Message);
        }
    }
}
