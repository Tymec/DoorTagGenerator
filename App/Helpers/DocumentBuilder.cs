using System.Collections.Generic;
using System.IO;
using App.Models;
using Avalonia.Media.Imaging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace App.Helpers;

public static class DocumentBuilder {
    public static Document Build(Configuration config) {
        return Document.Create(container => {
            container.Page(page => {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text(config.RoomNumber)
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x => {
                        x.Spacing(20);

                        x.Item().Text(string.Join(", ", config.RoomMembers));

                        if (config.Logo != null)
                            x.Item().Image(config.Logo);
                        else
                            x.Item().Text("No image");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x => {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        });
    }

    public static List<Bitmap> ToImage(this Document doc, int dpi = 96) {
        ImageGenerationSettings opts = new() {
            RasterDpi = dpi
        };
        IEnumerable<byte[]> images = doc.GenerateImages(opts);

        List<Bitmap> bitmaps = [];
        foreach (byte[] image in images) {
            using MemoryStream stream = new(image);
            bitmaps.Add(new Bitmap(stream));
        }

        return bitmaps;
    }
}
