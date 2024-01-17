using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using System.Windows;

namespace App.Services;

public class WindowsPrinterService : IPrintService {
    public bool PrintImage(byte[] data) {
        return false;
    }

    public bool PrintXps(string path) {
        PrintDialog dialog = new() {
            PageRangeSelection = PageRangeSelection.AllPages,
            UserPageRangeEnabled = true,
        };

        bool? print = dialog.ShowDialog();
        if (print == null || print == false) {
            return false;
        }

        try {
            XpsDocument doc = new(path, FileAccess.Read);
            FixedDocumentSequence fixedDocSeq = doc.GetFixedDocumentSequence();
            DocumentPaginator docPaginator = fixedDocSeq.DocumentPaginator;
            dialog.PrintDocument(docPaginator, $"Printing {Path.GetFileName(path)}");

            return true;
        } catch (Exception e) {
            MessageBox.Show(e.Message);

            return false;
        }
    }

    // private static FixedDocumentSequence CreateFixedDocumentSequence(byte[] data) {
    //     XpsDocument? doc = null;

    //     using MemoryStream stream = new(data);
    //     using Package package = Package.Open(stream);

    //     Uri uri = new("memorystream://document.xps");
    //     try {
    //         PackageStore.AddPackage(uri, package);
    //         doc = new(package, CompressionOption.Maximum, uri.AbsoluteUri);
    //         return doc.GetFixedDocumentSequence();
    //     } finally {
    //         doc?.Close();
    //         PackageStore.RemovePackage(uri);
    //     }
    // }
}
