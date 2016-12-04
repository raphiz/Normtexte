using netoaster;
using System.Windows;

namespace Normtexte.Utils
{
    class Toaster
    {
        private static ToasterPosition position = ToasterPosition.PrimaryScreenBottomRight;
        internal static void Warning(string title, string message = "", Window owner = null)
        {
            WarningToaster.Toast(owner, title: title,
                                        message: message,
                                        position: position);
        }
        internal static void Success(string title, string message = "", Window owner = null)
        {
            SuccessToaster.Toast(owner, title: title,
                                        message: message,
                                        position: position);
        }
    }
}
