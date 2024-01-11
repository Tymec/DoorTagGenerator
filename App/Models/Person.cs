using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public partial class Person : ObservableObject {
    [ObservableProperty]
    private string _name = string.Empty;

    public override string ToString() => Name;
}
