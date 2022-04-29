using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Core.Misc
{
    public class Highlight
    {
        public static void HighlightWord(string text, ScintillaNET.Scintilla TextArea)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            const int NUM = 8;

            // Remove all uses of our indicator
            TextArea.IndicatorCurrent = NUM;
            TextArea.IndicatorClearRange(0, TextArea.TextLength);

            // Update indicator appearance
            TextArea.Indicators[NUM].Style = IndicatorStyle.StraightBox;
            TextArea.Indicators[NUM].Under = true;
            TextArea.Indicators[NUM].ForeColor = Color.LightGray;
            TextArea.Indicators[NUM].OutlineAlpha = 50;
            TextArea.Indicators[NUM].Alpha = 100;

            // Search the document
            TextArea.TargetStart = 0;
            TextArea.TargetEnd = TextArea.TextLength;
            TextArea.SearchFlags = SearchFlags.None;
            while (TextArea.SearchInTarget(text) != -1)
            {
                // Mark the search results with the current indicator
                TextArea.IndicatorFillRange(TextArea.TargetStart, TextArea.TargetEnd - TextArea.TargetStart);

                // Search the remainder of the document
                TextArea.TargetStart = TextArea.TargetEnd;
                TextArea.TargetEnd = TextArea.TextLength;
            }
        }

        private static void HighlightTimer(ScintillaNET.Scintilla TextArea)
        {
            Timer t = new Timer();
            t.Interval = 500;
            t.Tick += new EventHandler((obj, ev) =>
            {
                string sWord = TextArea.GetWordFromPosition(TextArea.CurrentPosition);
                if (!string.IsNullOrEmpty(sWord))
                    HighlightWord(sWord, TextArea);

                t.Stop();
                t.Enabled = false;
                t.Dispose();
            });
            t.Start();
        }

        public static void Words(MouseEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            try
            {
                TextArea.IndicatorClearRange(0, TextArea.TextLength);
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        HighlightTimer(TextArea);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// When the mouse uses the left button and clicks on a selected text, we should be able to find
        /// and highlight different occurrences with the same text
        /// </summary>
        public static void TextArea_MouseDown(object sender, MouseEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            Highlight.Words(e, TextArea);
        }
    }
}
