using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace App.Models;

public partial class Configuration : ObservableObject {
    [ObservableProperty]
    private string _logoUrl;

    private string _roomNumber = "0";
    public string RoomNumber {
        get => _roomNumber;
        set {
            if (string.IsNullOrEmpty(value)) {
                SetProperty(ref _roomNumber, "0");
                return;
            }

            if (!int.TryParse(value, out _)) {
                return;
            }

            SetProperty(ref _roomNumber, value);
        }
    }

    [ObservableProperty]
    private ObservableCollection<Person> _roomMembers;

    public Configuration() {
        LogoUrl = " ";
        RoomNumber = "0";
        RoomMembers =
        [
            new Person { Name = "John" },
            new Person { Name = "Veronica" },
            new Person { Name = "Alice" },
            new Person { Name = "Bob" },
            new Person { Name = "Eve" },
            new Person { Name = "Mallory" },
            new Person { Name = "Trent" },
            new Person { Name = "Walter" },
            new Person { Name = "Carol" },
        ];
    }

    public void CopyFrom(Configuration other) {
        LogoUrl = other.LogoUrl;
        RoomNumber = other.RoomNumber;
        RoomMembers = other.RoomMembers;
    }
}
