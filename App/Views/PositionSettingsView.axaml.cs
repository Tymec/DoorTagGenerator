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

    private void HandleNumericArrows(object? sender, KeyEventArgs e) {
        if (sender is not NumericUpDown input) {
            return;
        }

        var increment = input.Increment;
        if (e.Key == Key.Up) {
            input.Value += increment;
        } else if (e.Key == Key.Down) {
            input.Value -= increment;
        }
    }

    private void HandleNumericScroll(object? sender, PointerWheelEventArgs e) {
        if (sender is not NumericUpDown input) {
            return;
        }

        var increment = input.Increment;
        input.Value += increment * (int)e.Delta.Y;
    }
}
