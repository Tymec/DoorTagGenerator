using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using System.Windows;
using System.Printing;

namespace App.Services;

public class WindowsPrinterService : IPrintService {
    public bool PrintImage(byte[] data) {
        return false;
    }

    public bool PrintXps(string path) {
        PrintTicket ticket = new() {
            PageOrientation = PageOrientation.Landscape,
            PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4),
        };

        PrintDialog dialog = new() {
            PageRangeSelection = PageRangeSelection.AllPages,
            UserPageRangeEnabled = true,
            PrintTicket = ticket,
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
}
