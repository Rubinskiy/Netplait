using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Forms.Reload
{
    public class ReloadOnDemand
    {
        /// <summary>
        /// Reload all open instances of Scintilla in the MainForm, after modifying preferences
        /// </summary>        
        public static void ReloadSciOnDemand(TabControl tabControl)
        {
            foreach (TabPage tb in tabControl.TabPages)
            {                
                string tbText = tb.Text.ToString();
                if (tb.Text == "Start Page") { /*does not update*/ }
                else if (FileTypes.FileExtensionHandler.IsPythonFile(tb.Text.ToString()))
                {
                    var sci = (ScintillaNET.Scintilla)tb.Controls[0];
                    Helpers.PythonHelper.PythonConfiguration(sci);
                }
                else if (FileTypes.FileExtensionHandler.IsHTMLFile(tb.Text.ToString()))
                {
                    var sci = (ScintillaNET.Scintilla)tb.Controls[0];
                    Helpers.HTMLHelper.HTMLConfiguration(sci);
                }
                else if (FileTypes.FileExtensionHandler.IsCSSFile(tb.Text.ToString()))
                {
                    var sci = (ScintillaNET.Scintilla)tb.Controls[0];
                    Helpers.CSSHelper.CSSConfiguration(sci);
                }
                else if (FileTypes.FileExtensionHandler.IsJSFile(tb.Text.ToString()))
                {
                    var sci = (ScintillaNET.Scintilla)tb.Controls[0];
                    Helpers.JSHelper.JSConfiguration(sci);
                }
                else
                {
                    var sci = (ScintillaNET.Scintilla)tb.Controls[0];
                    Helpers.BlextHelper.BlextConfiguration(sci);
                }
            }
        }
    }
}
