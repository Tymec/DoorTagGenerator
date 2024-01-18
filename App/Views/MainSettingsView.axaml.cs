using Avalonia.Controls;

namespace App.Views;

public partial class MainSettingsView : UserControl {
    public MainSettingsView() {
        InitializeComponent();

        RoomMemberInput.KeyDown += (sender, e) => {
            if (e.Key == Avalonia.Input.Key.Enter && RoomMemberButton?.Command != null && RoomMemberButton.Command.CanExecute(RoomMemberInput.Text)) {
                RoomMemberButton?.Command?.Execute(RoomMemberInput.Text);
            }
        };

        // RoomNumberInput.TextChanged += TextBox_TextChanged;
    }

    // private void TextBox_TextChanged(object? sender, RoutedEventArgs e) {
    //     if (sender is not TextBox textBox) {
    //         return;
    //     }

    //     if (textBox.Text == "0") {
    //         textBox.CaretIndex = textBox.Text.Length;
    //     }
    // }
}
