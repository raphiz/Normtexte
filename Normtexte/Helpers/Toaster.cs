using System.Windows;
using netoaster;

namespace NormtexteUI.Helpers
{
    public class Toaster
    {
        private static readonly ToasterPosition Position = ToasterPosition.PrimaryScreenBottomRight;

        internal static void Warning(string title, string message = "", Window owner = null)
        {
            WarningToaster.Toast(owner, title: title,
                                        message: message,
                                        position: Position);
        }
        internal static void Success(string title, string message = "", Window owner = null)
        {
            SuccessToaster.Toast(owner, title: title,
                                        message: message,
                                        position: Position);
        }
    }
}
