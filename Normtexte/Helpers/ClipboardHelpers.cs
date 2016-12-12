using System.Windows;

namespace NormtexteUI.Helpers
{
    internal class ClipboardHelpers
    {
        internal static string[] GetExcelData()
        {
            var dataObj = Clipboard.GetDataObject();
            var clipboardRawData = dataObj?.GetData(DataFormats.CommaSeparatedValue) as string;
            if (clipboardRawData == null)
            {
               return null;
            }
            var tokens = System.Text.RegularExpressions.Regex.Split(
                clipboardRawData.Substring(1, clipboardRawData.Length - 2), @";");
            return tokens;
        }

        internal static void SetExcelData(params object[] data)
        {
            Clipboard.SetText("\"" + string.Join("\";\"", data) + "\"", TextDataFormat.CommaSeparatedValue);
        }
    }
}
