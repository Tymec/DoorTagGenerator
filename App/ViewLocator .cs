using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace App;

public class ViewLocator : IDataTemplate {
    public static bool SupportsRecycling => false;

    public Control Build(object? data) {
        if (data is null) {
            return new TextBlock { Text = "Null" };
        }

        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null) {
            return (Control)Activator.CreateInstance(type)!;
        } else {
            return new TextBlock { Text = "Not Found: " + name };
        }
    }

    public bool Match(object? data) {
        return data is ViewModels.SettingsViewModel;
    }
}
