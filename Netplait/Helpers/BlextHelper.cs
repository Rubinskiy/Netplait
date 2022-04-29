using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET;
using Netplait.Core.Misc;
using Netplait.Properties;
using Netplait.Core.Indexing;
using Netplait.Core.Indenting;

namespace Netplait.Helpers
{
    public class BlextHelper
    {
        /// <summary>
        /// This method will assign all the configured settings that will be used to generate a new Blank/Text editor
        /// </summary>
        public static void BlextConfiguration(ScintillaNET.Scintilla TextArea)
        {
            TextArea.StyleResetDefault();
            Action Styling = () => Styles(TextArea);
            ThemeConfiguration.Configure(5, TextArea, Styling);

            TextArea.Lexer = Lexer.Null;
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "0");

            //End At Last Line
            if (Properties.Settings.Default.ScrollPastLastLine == true)
                TextArea.EndAtLastLine = false;
            else { TextArea.EndAtLastLine = true; }

            TextArea.ViewWhitespace = Properties.Settings.Default.IndentationGuides;
            TextArea.TabDrawMode = Properties.Settings.Default.TabDrawMode;
            TextArea.CaretStyle = Properties.Settings.Default.CaretStyle;

            TextArea.IndentationGuides = IndentView.None;

            TextArea.CaretLineVisible = true;
            TextArea.CaretWidth = Properties.Settings.Default.CaretWidth;

            TextArea.UseTabs = Properties.Settings.Default.UseTabs;
            TextArea.TabWidth = Properties.Settings.Default.TabWidth;
            TextArea.FontQuality = Properties.Settings.Default.FontQuality;
            TextArea.AdditionalSelectionTyping = true;
            TextArea.MouseSelectionRectangularSwitch = true;
            TextArea.MultipleSelection = true;
            TextArea.AutoCAutoHide = true;

            TextArea.WrapMode = Properties.Settings.Default.WrapMode;
            TextArea.VirtualSpaceOptions = Properties.Settings.Default.VirtualSpaceOptions;
            TextArea.EolMode = Properties.Settings.Default.EolMode;
            TextArea.ViewEol = Properties.Settings.Default.ViewEol;

            TextArea.ClearCmdKey(Keys.Control | Keys.S);
            TextArea.ClearCmdKey(Keys.Control | Keys.Shift | Keys.S);
            TextArea.ClearCmdKey(Keys.Control | Keys.O);
            TextArea.ClearCmdKey(Keys.Control | Keys.Shift | Keys.O);
            TextArea.ClearCmdKey(Keys.Control | Keys.W);
            TextArea.ClearCmdKey(Keys.Control | Keys.F);
            TextArea.ClearCmdKey(Keys.Control | Keys.G);
            TextArea.ClearCmdKey(Keys.Control | Keys.H);
        }

        private static void Styles(ScintillaNET.Scintilla TextArea)
        {
            //Lexer
            TextArea.Styles[Style.Default].ForeColor = Blext.Default.Defaulttext;
            TextArea.Styles[Style.Default].Bold = Blext.Default.DefaulttextBold;
            TextArea.Styles[Style.Default].Italic = Blext.Default.DefaulttextItalic;
        }

        /// <summary>
        /// This method will decide if a Blank/Text indentation style is required
        /// </summary>
        public static void TextArea_InsertCheck(object sender, InsertCheckEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                int startPos = TextArea.Lines[TextArea.LineFromPosition(TextArea.CurrentPosition)].Position;
                int endPos = e.Position;
                string curLineText = TextArea.GetTextRange(startPos, endPos - startPos);
                Match indent = IndentationController.indentLevel.Match(curLineText);
                e.Text = e.Text + indent.Value;
                if (IndentationController.indenter.IsMatch(curLineText))
                {
                    e.Text = e.Text + new string(' ', TextArea.IndentWidth);
                }
            }
        }

        /// <summary>
        /// This method will help the indentation style and intellisense for built-in Blank/Text functions
        /// </summary>
        public static void TextArea_CharAdded(object sender, CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Clear indicators
            TextArea.IndicatorClearRange(0, TextArea.TextLength);

            var currentPos = TextArea.CurrentPosition;

            //Matched braces
            ZenModel.InsertMatchedChars(e, TextArea);

            //Skip Braces
            Helpers.ZenModel.SkipBraces(TextArea, e);
        }
    }
}