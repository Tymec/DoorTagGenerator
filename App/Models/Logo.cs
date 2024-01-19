using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using QuestPDF.Helpers;
using ReactiveUI;

namespace App.Models;

public partial class Logo : ObservableObject {
    [ObservableProperty]
    private byte[] _data = Placeholders.Image(500, 500);

    [ObservableProperty]
    private float _aspectRatio = 1f;

    [ObservableProperty]
    private float _xOffset = 0;

    [ObservableProperty]
    private float _yOffset = 0;
    public void CopyFrom(Logo other) {
        Data = other.Data;
        XOffset = other.XOffset;
        YOffset = other.YOffset;
        AspectRatio = other.AspectRatio;
    }
}
