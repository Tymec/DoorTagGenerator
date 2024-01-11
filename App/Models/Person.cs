using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public class Person : ObservableObject {
    private string _name = string.Empty;

    public string Name {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    // override ToString() to display the name of the person
    public override string ToString() => Name;
}
