using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Drawing;
using System.ComponentModel;

namespace Netplait.Helpers
{
    public class ThemeConfiguration
    {
        /// <summary>
        /// Configure the graphical styles assigned to the editor
        /// </summary>
        /// <param name="lang">1: Python, 2: HTML, 3: CSS, 4: JS, 5: Blext</param>
        public static void Configure(int lang, ScintillaNET.Scintilla TextArea, Action Styles)
        {
            Color back_color_dark = Properties.Py.Default.BackColorDark;
            Color fore_color_dark = Properties.Py.Default.ForeColorDark;
            Color back_color_white = Properties.Py.Default.BackColorWhite;
            Color fore_color_white = Properties.Py.Default.ForeColorWhite;

            TextArea.Styles[Style.Default].Font = Properties.Settings.Default.GlobalFont.ToString();
            TextArea.Styles[Style.Default].Size = Properties.Settings.Default.GlobalFontSize;

            //Theme
            if (Properties.Settings.Default.DarkTheme == true)
            {
                //TextArea
                TextArea.Styles[Style.Default].BackColor = back_color_dark;
                TextArea.Styles[Style.Default].ForeColor = fore_color_dark;
                TextArea.Styles[Style.Default].BackColor = back_color_dark;
                TextArea.Styles[Style.Default].ForeColor = fore_color_dark;
                TextArea.StyleClearAll();

                //Styles
                Styles();

                //Margin
                Margins.InitNumberMargin(TextArea, back_color_dark, fore_color_dark);
                Margins.InitBookmarkMargin(TextArea, back_color_dark);

                //Caret
                if (lang == 1)
                    TextArea.CaretForeColor = Properties.Py.Default.Caret;
                else if (lang == 2)
                    TextArea.CaretForeColor = Properties.HTML.Default.Caret;
                else if (lang == 3)
                    TextArea.CaretForeColor = Properties.CSS.Default.Caret;
                else if (lang == 4)
                    TextArea.CaretForeColor = Properties.JS.Default.Caret;
                else if (lang == 5)
                    TextArea.CaretForeColor = Properties.Blext.Default.Caret;
                TextArea.AdditionalCaretForeColor = Color.FromArgb(240, 240, 240);

                //Selection
                TextArea.SetSelectionBackColor(true, Color.FromArgb(100, 100, 100));

                //Folding margin colors
                Margins.InitCodeFolding(TextArea, back_color_dark, fore_color_dark);

                //Set Line highlight Color
                if (Properties.Settings.Default.HighlightLine == true)
                    TextArea.CaretLineBackColor = Properties.Settings.Default.CaretLineBackColor;
                else { TextArea.CaretLineBackColor = back_color_dark; }

                //Calltip
                Calltip.InitCallTip(TextArea, fore_color_dark, fore_color_white, true);

                //Brace matching
                TextArea.Styles[Style.BraceLight].BackColor = Properties.Settings.Default.BraceDarkBack;
                TextArea.Styles[Style.BraceLight].ForeColor = Properties.Settings.Default.BraceDarkFore;
                TextArea.Styles[Style.BraceBad].BackColor = Properties.Settings.Default.BraceDarkBack;
                TextArea.Styles[Style.BraceBad].ForeColor = Color.FromArgb(255, 38, 74);
            }
            else
            {
                //TextArea
                TextArea.Styles[Style.Default].BackColor = back_color_white;
                TextArea.Styles[Style.Default].ForeColor = fore_color_white;
                TextArea.Styles[Style.Default].BackColor = back_color_white;
                TextArea.Styles[Style.Default].ForeColor = fore_color_white;
                TextArea.StyleClearAll();

                //Styles
                Styles();

                //Margin
                Margins.InitNumberMargin(TextArea, back_color_white, fore_color_white);
                Margins.InitBookmarkMargin(TextArea, back_color_white);

                //Caret
                TextArea.CaretForeColor = Color.Black;
                TextArea.AdditionalCaretForeColor = Color.FromArgb(220, 220, 220);

                //Selection
                TextArea.SetSelectionBackColor(true, Color.FromArgb(200, 200, 200));

                //Folding margin colors
                Margins.InitCodeFolding(TextArea, back_color_white, fore_color_white);

                //Set Line highlight Color
                if (Properties.Settings.Default.HighlightLine == true)
                    TextArea.CaretLineBackColor = Properties.Settings.Default.CaretLineBackColor;
                else { TextArea.CaretLineBackColor = back_color_white; }

                //Calltip
                Calltip.InitCallTip(TextArea, fore_color_dark, fore_color_white, false);

                //Brace matching
                TextArea.Styles[Style.BraceLight].BackColor = Properties.Settings.Default.BraceLightBack;
                TextArea.Styles[Style.BraceLight].ForeColor = Properties.Settings.Default.BraceLightFore;
                TextArea.Styles[Style.BraceBad].BackColor = Properties.Settings.Default.BraceLightBack;
                TextArea.Styles[Style.BraceBad].ForeColor = Color.Red;
            }
        }
    }
}
