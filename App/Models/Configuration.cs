using CommunityToolkit.Mvvm.ComponentModel;
using QuestPDF.Helpers;
using System.Collections.ObjectModel;

namespace App.Models;

public partial class Configuration : ObservableObject {
    [ObservableProperty]
    private byte[] _logo = Placeholders.Image(500, 500);

    private string _roomNumber = "0";
    public string RoomNumber {
        get => _roomNumber;
        set {
            if (string.IsNullOrEmpty(value)) {
                SetProperty(ref _roomNumber, "0");
                return;
            }

            if (!long.TryParse(value, out _)) {
                return;
            }

            SetProperty(ref _roomNumber, value);
        }
    }

    [ObservableProperty]
    private ObservableCollection<Person> _roomMembers = [
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
    ];

    public void CopyFrom(Configuration other) {
        Logo = other.Logo;
        RoomNumber = other.RoomNumber;
        RoomMembers = other.RoomMembers;
    }
}
