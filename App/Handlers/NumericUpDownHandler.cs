using Avalonia.Controls;
using Avalonia.Input;

namespace App.Handlers;

public static class NumericUpDownHandler {
    public static void OnKeyDown(object? sender, KeyEventArgs e) {
        if (sender is not NumericUpDown input) {
            return;
        }

        var increment = input.Increment;
        if (e.Key == Key.Up) {
            input.Value += increment;
        } else if (e.Key == Key.Down) {
            input.Value -= increment;
        }

        if (input.Value < input.Minimum) input.Value = input.Minimum;
        if (input.Value > input.Maximum) input.Value = input.Maximum;
    }

    public static void OnPointerWheelChanged(object? sender, PointerWheelEventArgs e) {
        if (sender is not NumericUpDown input) {
            return;
        }

        var increment = input.Increment;
        input.Value += increment * (int)e.Delta.Y;

        if (input.Value < input.Minimum) input.Value = input.Minimum;
        if (input.Value > input.Maximum) input.Value = input.Maximum;
    }
}
