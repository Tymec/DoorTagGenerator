using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace App.Models;

public class Configuration : ObservableObject {
    private string _logoUrl;
    private int _roomNumber;
    private ObservableCollection<Person> _roomMembers;

    public string LogoUrl {
        get => _logoUrl;
        set => SetProperty(ref _logoUrl, value);
    }

    public int RoomNumber {
        get => _roomNumber;
        set => SetProperty(ref _roomNumber, value);
    }

    public ObservableCollection<Person> RoomMembers {
        get => _roomMembers;
        set => SetProperty(ref _roomMembers, value);
    }

    public Configuration() {
        _logoUrl = "https://example.com/logo.png";
        _roomNumber = 0;
        _roomMembers =
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
}
