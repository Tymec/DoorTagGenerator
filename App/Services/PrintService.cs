using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace App.Services;


public class PrintService {
    // https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Diagnostics/Diagnostics/VisualExtensions.cs#L56
    public static Bitmap Rasterize(Control ctrl, double dpi = 96) {
        Vector dpiVector = new(dpi, dpi);
        // using var bitmap = new RenderTargetBitmap(pixelSize, dpiVector);
        // bitmap.Render(vis);
        // bitmap.Save(destination);
        throw new NotImplementedException(); // TODO: implement
    }
}
