using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using Netplait.Core.Misc;

namespace Netplait.Classes
{
    public class Assignation
    {
        /// <summary>
        /// This method will assign the configured styles from the Helpers -> Python class to the Python editor
        /// </summary>
        public static void AssignPythonStyles(ScintillaNET.Scintilla TextArea, ToolStripStatusLabel status_lbl)
        {
            Helpers.PythonHelper.PythonConfiguration(TextArea);
            TextArea.CharAdded += ((sender, e) => Helpers.PythonHelper.TextArea_CharAdded(sender, e, TextArea));
            TextArea.InsertCheck += ((sender, e) => Helpers.PythonHelper.TextArea_InsertCheck(sender, e, TextArea));
            TextArea.MarginClick += ((sender, e) => Helpers.Margins.TextArea_MarginClick(sender, e, TextArea));
            TextArea.UpdateUI += ((sender, e) => UpdateSelection.TextArea_UpdateUI(sender, e, TextArea, status_lbl, false));
            TextArea.MouseDown += ((sender, e) => Highlight.TextArea_MouseDown(sender, e, TextArea));
            TextArea.DwellStart += ((sender, e) => Helpers.PythonHelper.TextArea_DwellStart(sender, e, TextArea));
            TextArea.DwellEnd += ((sender, e) => Helpers.PythonHelper.TextArea_DwellEnd(sender, e, TextArea));
        }

        /// <summary>
        /// This method will assign the configured styles from the Helpers -> HTML class to the HTML editor
        /// </summary>
        public static void AssignHTMLStyles(ScintillaNET.Scintilla TextArea, ToolStripStatusLabel status_lbl)
        {
            Helpers.HTMLHelper.HTMLConfiguration(TextArea);
            TextArea.CharAdded += ((sender, e) => Helpers.HTMLHelper.TextArea_CharAdded(sender, e, TextArea));
            TextArea.InsertCheck += ((sender, e) => Helpers.HTMLHelper.TextArea_InsertCheck(sender, e, TextArea));
            TextArea.UpdateUI += ((sender, e) => UpdateSelection.TextArea_UpdateUI(sender, e, TextArea, status_lbl, true));
            TextArea.AutoCSelection += ((sender, e) => HexColorDialog.TextArea_AutoCSelection(sender, e, TextArea));
            TextArea.AutoCCompleted += ((sender, e) => Helpers.HTMLHelper.TextArea_AutoCCompleted(sender, e, TextArea));
            TextArea.MarginClick += ((sender, e) => Helpers.Margins.TextArea_MarginClick(sender, e, TextArea));
            TextArea.MouseDown += ((sender, e) => Highlight.TextArea_MouseDown(sender, e, TextArea));
        }

        /// <summary>
        /// This method will assign the configured styles from the Helpers -> CSS class to the CSS editor
        /// </summary>
        public static void AssignCSSStyles(ScintillaNET.Scintilla TextArea, ToolStripStatusLabel status_lbl)
        {
            Helpers.CSSHelper.CSSConfiguration(TextArea);
            TextArea.CharAdded += ((sender, e) => Helpers.CSSHelper.TextArea_CharAdded(sender, e, TextArea));
            TextArea.InsertCheck += ((sender, e) => Helpers.CSSHelper.TextArea_InsertCheck(sender, e, TextArea));
            TextArea.AutoCSelection += ((sender, e) => HexColorDialog.TextArea_AutoCSelection(sender, e, TextArea));
            TextArea.MarginClick += ((sender, e) => Helpers.Margins.TextArea_MarginClick(sender, e, TextArea));
            TextArea.UpdateUI += ((sender, e) => UpdateSelection.TextArea_UpdateUI(sender, e, TextArea, status_lbl, false));
            TextArea.MouseDown += ((sender, e) => Highlight.TextArea_MouseDown(sender, e, TextArea));
        }

        /// <summary>
        /// This method will assign the configured styles from the Helpers -> JS class to the JS editor
        /// </summary>
        public static void AssignJSStyles(ScintillaNET.Scintilla TextArea, ToolStripStatusLabel status_lbl)
        {
            Helpers.JSHelper.JSConfiguration(TextArea);
            TextArea.CharAdded += ((sender, e) => Helpers.JSHelper.TextArea_CharAdded(sender, e, TextArea));
            TextArea.InsertCheck += ((sender, e) => Helpers.JSHelper.TextArea_InsertCheck(sender, e, TextArea));
            TextArea.AutoCSelection += ((sender, e) => HexColorDialog.TextArea_AutoCSelection(sender, e, TextArea));
            TextArea.MarginClick += ((sender, e) => Helpers.Margins.TextArea_MarginClick(sender, e, TextArea));
            TextArea.UpdateUI += ((sender, e) => UpdateSelection.TextArea_UpdateUI(sender, e, TextArea, status_lbl, false));
            TextArea.MouseDown += ((sender, e) => Highlight.TextArea_MouseDown(sender, e, TextArea));
        }

        /// <summary>
        /// This method will assign the configured styles from the Helpers -> Blext class to the Blext editor
        /// </summary>
        public static void AssignBlextStyles(ScintillaNET.Scintilla TextArea, ToolStripStatusLabel status_lbl)
        {
            Helpers.BlextHelper.BlextConfiguration(TextArea);
            TextArea.CharAdded += ((sender, e) => Helpers.BlextHelper.TextArea_CharAdded(sender, e, TextArea));
            TextArea.InsertCheck += ((sender, e) => Helpers.BlextHelper.TextArea_InsertCheck(sender, e, TextArea));
            TextArea.MarginClick += ((sender, e) => Helpers.Margins.TextArea_MarginClick(sender, e, TextArea));
            TextArea.UpdateUI += ((sender, e) => UpdateSelection.TextArea_UpdateUI(sender, e, TextArea, status_lbl, false));
            TextArea.MouseDown += ((sender, e) => Highlight.TextArea_MouseDown(sender, e, TextArea));
        }
    }
}
