using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Drawing;
using System.Windows.Forms;

namespace Netplait.Core.Misc
{
    public class HexColorDialog
    {
        /// <summary>
        /// When the user types '#' a custom autocomplete pops-up showing a color list
        /// </summary>
        public static void TextArea_AutoCSelection(object sender, AutoCSelectionEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Hex color tool
            int caretPos = TextArea.SelectionStart;
            if (e.Text == "Colors...")
            {
                ColorDialog cdl = new ColorDialog();
                cdl.AllowFullOpen = true;
                cdl.FullOpen = true;
                if (cdl.ShowDialog() == DialogResult.OK)
                {
                    string color = (cdl.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                    TextArea.InsertText(caretPos, color);
                    TextArea.GotoPosition(caretPos + color.Length);
                }
            }
        }
    }
}
