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
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Content()
                    .Column(col => {
                        col.Item()
                            .PaddingBottom(1, Unit.Centimetre)
                            .Row(row => {
                                // Logo
                                row.RelativeItem(4)
                                    // .Border(1, Unit.Millimetre).BorderColor(Colors.Red.Medium)   // DEBUG
                                    // .Width(8, Unit.Centimetre)
                                    // .Height(2, Unit.Centimetre)
                                    // .MaxHeight(2, Unit.Centimetre)
                                    // .MaxWidth(4, Unit.Centimetre)
                                    .AspectRatio(1)
                                    .AlignCenter()
                                    .Image(config.Logo)
                                    .FitArea()
                                    .WithRasterDpi(96);

                                row.Spacing(1, Unit.Centimetre);

                                // Room Number
                                row.RelativeItem(6)
                                    // .Border(1, Unit.Millimetre).BorderColor(Colors.Red.Medium)   // DEBUG
                                    .AlignLeft().AlignMiddle()
                                    .Text(config.RoomNumber).SemiBold().FontSize(52).FontColor(Colors.Black);
                            });

                        // Room members
                        foreach (var person in config.RoomMembers) {
                            col.Item()
                                .PaddingLeft(1, Unit.Centimetre)
                                .Shrink()
                                .ScaleToFit()
                                .Row(row => {
                                    // row.Spacing(5);

                                    row.RelativeItem(1)
                                        .Text(person.Name);
                                });
                        }
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
