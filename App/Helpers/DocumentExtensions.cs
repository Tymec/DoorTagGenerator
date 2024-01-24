using System;
using System.Collections.Generic;
using System.IO;
using App.Models;
using Avalonia.Media.Imaging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace App.Helpers;

public static class DocumentExtensions {
    public static readonly int DPI = 96;

    public static Document ToDocument(this DoorTag tag) {
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
                                    .AspectRatio(1 / tag.Logo.AspectRatio).AlignCenter()
                                    .TranslateX(tag.Logo.XOffset).TranslateY(tag.Logo.YOffset)
                                    .Image(tag.Logo.Data)
                                    .FitArea()
                                    .WithRasterDpi(DPI);

                                row.Spacing(1, Unit.Centimetre);

                                // Room Number
                                row.RelativeItem(6)
                                    // .Border(1, Unit.Millimetre).BorderColor(Colors.Red.Medium)   // DEBUG
                                    .AlignLeft().AlignMiddle()
                                    .TranslateX(tag.RoomNumber.XOffset).TranslateY(tag.RoomNumber.YOffset)
                                    .Text(tag.RoomNumber.Text)
                                    .SemiBold().FontSize(tag.RoomNumber.Size)
                                    .FontColor(GetColor(tag.RoomNumber.Color));
                            });

                        // Room members
                        foreach (var member in tag.RoomMembers.Members) {
                            col.Item()
                                .PaddingLeft(1, Unit.Centimetre)
                                .TranslateX(tag.RoomMembers.XOffset).TranslateY(tag.RoomMembers.YOffset)
                                .Row(row => {
                                    // row.Spacing(5);
                                    row.RelativeItem(1)
                                        .Text(member.Name)
                                        .FontSize(tag.RoomMembers.Size)
                                        .FontColor(GetColor(tag.RoomMembers.Color));
                                });
                        }
                    });
            });
        });
    }

    public static List<Bitmap> ToBitmap(this Document doc) {
        ImageGenerationSettings opts = new() {
            RasterDpi = DPI
        };
        IEnumerable<byte[]> images = doc.GenerateImages(opts);

        List<Bitmap> bitmaps = [];
        foreach (byte[] image in images) {
            using MemoryStream stream = new(image);
            bitmaps.Add(new Bitmap(stream));
        }

        return bitmaps;
    }

    private static string GetColor(string color, string defaultColor = Colors.Black) {
        if (string.IsNullOrEmpty(color)) {
            return defaultColor;
        }

        // The following formats are supported: #RGB, #ARGB, #RRGGBB, #AARRGGBB, The hash sign is optional.
        if (color.StartsWith('#')) {
            color = color[1..];
        }

        if (color.Length != 3 && color.Length != 4 && color.Length != 6 && color.Length != 8) {
            return defaultColor;
        }

        // make sure each character is a valid hex digit
        if (!int.TryParse(color, System.Globalization.NumberStyles.HexNumber, null, out _)) {
            return defaultColor;
        }

        return color;
    }
}
