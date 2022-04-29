using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Netplait.Core.Misc
{
    public class UpdateSelection
    {
        /// <summary>
        /// Identification of braces
        /// </summary>
        private static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '<':
                case '>':
                    return true;
            }
            return false;

        }

        static int lastCaretPos = 0;
        public static void TextArea_UpdateUI(object sender, UpdateUIEventArgs e, ScintillaNET.Scintilla TextArea, ToolStripStatusLabel lbl, bool IsHTML)
        {
            if ((e.Change & UpdateChange.Selection) > 0)
            {
                //Clear indicators
                TextArea.IndicatorClearRange(0, TextArea.TextLength);

                var currentPos = TextArea.CurrentPosition;
                var anchorPos = TextArea.AnchorPosition;
                var Line = TextArea.LineFromPosition(currentPos) + 1;
                lbl.Text = "Ln: " + Line + " Ch: " + currentPos + " Sel: " + Math.Abs(anchorPos - currentPos);
            }

            var caretPos = TextArea.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(TextArea.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(TextArea.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = TextArea.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                    {
                        TextArea.BraceBadLight(bracePos1);
                        TextArea.HighlightGuide = 0;
                    }
                    else
                    {
                        TextArea.BraceHighlight(bracePos1, bracePos2);
                        TextArea.HighlightGuide = TextArea.GetColumn(bracePos1);
                    }
                }
                else
                {
                    // Turn off brace matching
                    TextArea.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                    TextArea.HighlightGuide = 0;
                }
            }
            if(IsHTML == true)
            {
                //Jenja templates are signified by '{{' in HTML files
                var startPos = TextArea.Lines[TextArea.FirstVisibleLine].Position;
                var endPos = TextArea.Lines[TextArea.FirstVisibleLine + TextArea.LinesOnScreen].EndPosition;
                var code = TextArea.GetTextRange(startPos, endPos - startPos);
                foreach (Match mtch in Regex.Matches(code, @"\{\{.*?\}\}", RegexOptions.Multiline))
                {
                    int targetStartStyle = TextArea.GetStyleAt(mtch.Index + startPos);
                    int targetEndStyle = TextArea.GetStyleAt(mtch.Index + startPos + mtch.Length - 1);
                    TextArea.StartStyling(mtch.Index + startPos);
                    TextArea.SetStyling(mtch.Length, Style.Python.DefName);
                }
            }
        }
    }
}
