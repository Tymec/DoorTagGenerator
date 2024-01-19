using App.Handlers;
using Avalonia.Controls;
using Avalonia.Input;

namespace App.Views;

public partial class PositionSettingsView : UserControl {
    public PositionSettingsView() {
        InitializeComponent();

        foreach (var input in new[] {
            LogoXOffset, LogoYOffset,
            RoomNumberXOffset, RoomNumberYOffset,
            RoomMembersXOffset, RoomMembersYOffset
        }) {
            input.KeyDown += NumericUpDownHandler.OnKeyDown;
            input.PointerWheelChanged += NumericUpDownHandler.OnPointerWheelChanged;
        }
    }
}
