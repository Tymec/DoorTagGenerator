using CommunityToolkit.Mvvm.ComponentModel;
using QuestPDF.Helpers;

namespace App.Models;

public partial class Logo : ObservableObject {
    [ObservableProperty]
    private byte[] _data = Placeholders.Image(500, 500);

    // TODO: Aspect ratio
    // TODO: Size

    [ObservableProperty]
    private float _xOffset = 0;

    [ObservableProperty]
    private float _yOffset = 0;

    public void CopyFrom(Logo other) {
        Data = other.Data;
        XOffset = other.XOffset;
        YOffset = other.YOffset;
    }
}
