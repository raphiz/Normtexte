using System;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace NormtexteAddIn
{
    public partial class ThisAddIn
    {

        Office.CommandBarButton addNormTextButton;
        Office.CommandBarButton insertNormTextButton;
        Excel.Range currentSelection;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // initialize & register context-menu entries
            var menuItemType = Office.MsoControlType.msoControlButton;
            var controls = Application.CommandBars["Cell"].Controls;

            // evtl. use missing instead of 5 to add it at the end or after the insert item
            insertNormTextButton = (Office.CommandBarButton)controls.Add(menuItemType, missing, missing, 5, true);
            insertNormTextButton.Style = Office.MsoButtonStyle.msoButtonCaption;
            insertNormTextButton.Caption = "Normtext einfügen...";
            insertNormTextButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(OnInsertNormTextButton);

            addNormTextButton = (Office.CommandBarButton)controls.Add(menuItemType, missing, missing, 6, true);
            addNormTextButton.Style = Office.MsoButtonStyle.msoButtonCaption;
            addNormTextButton.Caption = "Normtext hinzufügen...";
            addNormTextButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(OnAddNormTextButton);

            Application.SheetActivate += new Excel.AppEvents_SheetActivateEventHandler(o => Console.WriteLine(o));
            Application.SheetBeforeRightClick += new Excel.AppEvents_SheetBeforeRightClickEventHandler(SaveRange);


        }
        void SaveRange(object Sh, Excel.Range Target, ref bool Cancel)
        {
            currentSelection = Target;
        }

        void OnAddNormTextButton(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            System.Windows.Forms.MessageBox.Show("Add Norm Text!");
        }
        void OnInsertNormTextButton(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            System.Windows.Forms.MessageBox.Show("Insert Norm Text!");
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
