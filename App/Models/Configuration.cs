using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace App.Models;

public partial class Configuration : ObservableObject {
    [ObservableProperty]
    private string _logoUrl;

    [ObservableProperty]
    private int _roomNumber;

    [ObservableProperty]
    private ObservableCollection<Person> _roomMembers;

    public Configuration() {
        LogoUrl = "https://example.com/logo.png";
        RoomNumber = 0;
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
}
