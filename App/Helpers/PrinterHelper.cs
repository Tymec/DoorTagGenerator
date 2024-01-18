using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using System.Windows;
using System.Printing;
// TODO: Might have to conditionally import Windows-specific namespaces

namespace App.Helpers;

public static class PrinterHelper {
    public static bool Print(string xpsPath) {
        if (!OperatingSystem.IsWindows()) {
            throw new PlatformNotSupportedException("Printing is only supported on Windows.");
        }

        // Landscape letter
        PrintTicket ticket = new() {
            PageOrientation = PageOrientation.Landscape,
            PageMediaSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter),
            OutputQuality = OutputQuality.Automatic,
            PageResolution = new PageResolution(PageQualitativeResolution.High),
            TrueTypeFontMode = TrueTypeFontMode.RenderAsBitmap,
        };

        PrintDialog dialog = new() {
            PageRangeSelection = PageRangeSelection.AllPages,
            UserPageRangeEnabled = true,
            // PrintTicket = ticket,
        };

        bool? print = dialog.ShowDialog();
        if (print == null || print == false) {
            return false;
        }

        XpsDocument xpsDocument = new(xpsPath, FileAccess.Read);
        FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
        DocumentPaginator paginator = fixedDocSeq.DocumentPaginator;

        try {
            dialog.PrintDocument(paginator, "Printing document");
            return true;
        } catch (Exception e) {
            MessageBox.Show(e.Message);
            return false;
        }
    }
}
