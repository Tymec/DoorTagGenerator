using System.Drawing.Printing;
using System.Threading.Tasks;

namespace App.Services;

public interface IPrintService {
    bool PrintImage(byte[] data);
    bool PrintXps(string path);
}
