using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using App.Views;
using App.Services;
using App.ViewModels;
using QuestPDF.Infrastructure;

namespace App;

public partial class App : Application {
    public static new App? Current => Application.Current as App;
    public IServiceProvider? Services { get; private set; }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted() {
        QuestPDF.Settings.License = LicenseType.Community;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = new MainWindowViewModel()
            };

            var services = new ServiceCollection();
            services.AddSingleton(x => new FileService(desktop.MainWindow));
#if _WINDOWS
            services.AddSingleton<IPrintService, WindowsPrinterService>();
// #else
//             services.AddSingleton<IPrintService, LinuxPrinterService>();
#endif
            Services = services.BuildServiceProvider();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
