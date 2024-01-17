using System;
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
                page.Size(PageSizes.Letter.Landscape());
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(row => {
                        row.Item()
                            .BorderBottom(2)
                            .BorderColor(Colors.Red.Medium)
                            .Row(col => {
                                col.RelativeItem().Image(config.Logo).FitArea();

                                col.RelativeItem().Text(config.RoomNumber).SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);
                            });

                        row.Item()
                            .Text(string.Join(", ", config.RoomMembers))
                            .FontSize(18)
                            .FontColor(Colors.Blue.Medium);

                    });
            });
        });
    }

    public static List<Bitmap> ToBitmap(this Document doc, int dpi = 96) {
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
