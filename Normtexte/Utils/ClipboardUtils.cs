
using System;
using System.Windows;

namespace Normtexte.Utils
{
    class ClipboardUtils
    {
        internal static string[] GetExcelData()
        {
            IDataObject dataObj = Clipboard.GetDataObject();
            string clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue) as string;
            if (clipboardRawData == null)
            {
               return null;

            }
            string[] tokens = System.Text.RegularExpressions.Regex.Split(
                clipboardRawData.Substring(1, clipboardRawData.Length - 2), @";");
            return tokens;
        }

        internal static void SetExcelData(params object[] data)
        {
            Clipboard.SetText("\"" + String.Join("\";\"", data) + "\"", TextDataFormat.CommaSeparatedValue);
        }
    }
}
