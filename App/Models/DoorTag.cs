using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public partial class DoorTag : ObservableObject {
    [ObservableProperty]
    private Logo _logo = new() { Data = Utils.LoadFromResource("Logo.png") };

    [ObservableProperty]
    private RoomNumber _roomNumber = new() { Text = "1083" };

    [ObservableProperty]
    private RoomMembers _roomMembers = new() {
        Members = [
            new() { Name = "John Doe" },
            new() { Name = "Marcus Aurelius" },
            new() { Name = "Veronica Mars" },
            new() { Name = "Thomas Anderson" },
            new() { Name = "Bruce Wayne" },
            new() { Name = "Tony Stark" },
            new() { Name = "Peter Parker" },
            new() { Name = "Clark Kent" },
            new() { Name = "Diana Prince" },
            new() { Name = "Natasha Romanoff" },
        ]
    };

    public void CopyFrom(DoorTag other) {
        Logo.CopyFrom(other.Logo);
        RoomNumber.CopyFrom(other.RoomNumber);
        RoomMembers.CopyFrom(other.RoomMembers);
    }
}
