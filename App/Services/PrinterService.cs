using System;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Xps.Packaging;

namespace App.Services;

public class WindowsPrinterService : IPrintService {
    public bool PrintImage(byte[] data) {
        return false;
    }

    public bool PrintXps(byte[] data) {
        PrintDialog dialog = new();

        try {
            XpsDocument document = new(data);
            FixedDocumentSequence sequence = document.GetFixedDocumentSequence();
            DocumentPaginator paginator = sequence.DocumentPaginator;

            dialog.PrintDocument(paginator, "Print Document");
        } catch (Exception e) {
            MessageBox.Show(e.Message);
            return false;
        }
    }
}
