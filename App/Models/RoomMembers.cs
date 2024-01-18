using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.Models;

public partial class RoomMembers : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<RoomMember> _members = [];

    [ObservableProperty]
    private int _size = 16;

    [ObservableProperty]
    private string _color = "#000000";

    [ObservableProperty]
    private float _xOffset = 0;

    [ObservableProperty]
    private float _yOffset = 0;

    public void CopyFrom(RoomMembers other) {
        Members.Clear();
        foreach (var member in other.Members) {
            Members.Add(new() { Name = member.Name });
        }

        Size = other.Size;
        Color = other.Color;
        XOffset = other.XOffset;
        YOffset = other.YOffset;
    }
}
