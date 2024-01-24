using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Themes.Neumorphism.Dialogs;
using Avalonia.Themes.Neumorphism.Dialogs.Enums;

namespace App.Services;

public class DialogService(Window window) {
    public static readonly int DefaultWidth = 480;

    public async Task<DialogResult?> Info(string header, string subtitle) => await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams {
        ContentHeader = header,
        SupportingText = subtitle,
        WindowTitle = "Info dialog",
        DialogHeaderIcon = DialogIconKind.Info,
        DialogIcon = DialogIconKind.Info,
        StartupLocation = WindowStartupLocation.CenterOwner,
        Width = DefaultWidth
    }).ShowDialog(window);

    public async Task<DialogResult?> Error(string message) => await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams {
        ContentHeader = "Error!",
        SupportingText = message,
        WindowTitle = "Error dialog",
        DialogHeaderIcon = DialogIconKind.Error,
        DialogIcon = DialogIconKind.Error,
        StartupLocation = WindowStartupLocation.CenterOwner,
        Width = DefaultWidth
    }).ShowDialog(window);

    public async Task<DialogResult?> Confirm(string action) => await DialogHelper.CreateCommonDialog(new CommonDialogBuilderParams() {
        ContentHeader = "Confirm action",
        SupportingText = "Are you sure to perform this action?",
        WindowTitle = "Confirm dialog",
        StartupLocation = WindowStartupLocation.CenterOwner,
        NegativeResult = new DialogResult("cancel"),
        DialogHeaderIcon = DialogIconKind.Help,
        Width = DefaultWidth,
        LeftDialogButtons = [
            new DialogButton {
                Content = "CANCEL",
                Result = "false"
            }
        ],
        RightDialogButtons = [
            new DialogButton {
                Content = action.ToUpper(),
                Result = "true",
                DialogButtonStyle = new DialogButtonStyle(DialogButtonBackgroundColor.PrimaryColor, DialogButtonForegroundColor.White)
            }
        ]
    }).ShowDialog(window);
}
