using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public partial class RoomNumber : ObservableObject {
    private string _text = "";
    public string Text {
        get => _text;
        set {
            if (string.IsNullOrEmpty(value)) {
                // SetProperty(ref _text, "0");
                SetProperty(ref _text, "");
                return;
            }

            if (!long.TryParse(value, out _)) {
                return;
            }

            SetProperty(ref _text, value);
        }
    }

    [ObservableProperty]
    private int _size = 52;

    [ObservableProperty]
    private string _color = "#000000";

    [ObservableProperty]
    private float _xOffset = 0;

    [ObservableProperty]
    private float _yOffset = 0;

    public override string ToString() => Text;

    public void CopyFrom(RoomNumber other) {
        Text = other.Text;
        Size = other.Size;
        Color = other.Color;
        XOffset = other.XOffset;
        YOffset = other.YOffset;
    }
}
