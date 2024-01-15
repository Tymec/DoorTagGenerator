using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;

namespace App.Services;

public class PrintService(Window target) {
    private readonly Window _target = target;

    // https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Diagnostics/Diagnostics/VisualExtensions.cs#L56
    public void Rasterize(string viewName, Stream stream, double dpi = 96) {
        var source = _target.FindControl<UserControl>(viewName)
            ?? throw new InvalidOperationException($"Could not find view '{viewName}'.");

        var rect = new Rect(source.Bounds.Size);
        var pixelSize = new PixelSize((int)rect.Width, (int)rect.Height);
        var dpiVector = new Vector(dpi, dpi);

        var root = source.GetVisualRoot() as Control ?? source;

        // Offset the visual to the top left corner.

        var bitmap = new RenderTargetBitmap(
            pixelSize,
            dpiVector
        );
        bitmap.Render(root);
        bitmap.Save(stream);
    }
}
