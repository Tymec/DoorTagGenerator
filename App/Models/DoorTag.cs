using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public partial class DoorTag : ObservableObject {
    [ObservableProperty]
    private Logo _logo = new();

    [ObservableProperty]
    private RoomNumber _roomNumber = new();

    [ObservableProperty]
    private RoomMembers _roomMembers = new();

    public void CopyFrom(DoorTag other) {
        Logo.CopyFrom(other.Logo);
        RoomNumber.CopyFrom(other.RoomNumber);
        RoomMembers.CopyFrom(other.RoomMembers);
    }

    public static DoorTag Default() {
        return new DoorTag() {
            Logo = { Data = Utils.LoadFromResource("Logo.png") },
            RoomNumber = { Text = "1083" },
            RoomMembers = {
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
            }
        };
    }
}
