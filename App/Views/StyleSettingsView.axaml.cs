using App.Handlers;
using Avalonia.Controls;

namespace App.Views;

public partial class StyleSettingsView : UserControl {
    public StyleSettingsView() {
        InitializeComponent();

        foreach (var input in new[] {
            LogoWidthInput, LogoHeightInput,
            RoomMembersSizeInput
        }) {
            input.KeyDown += NumericUpDownHandler.OnKeyDown;
            input.PointerWheelChanged += NumericUpDownHandler.OnPointerWheelChanged;
        }
    }
}
