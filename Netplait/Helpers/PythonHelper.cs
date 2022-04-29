using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET;
using Netplait.Core.Misc;
using Netplait.Core.Indexing;

namespace Netplait.Helpers
{
    public class PythonHelper
    {
        /// <summary>
        /// This method will assign all the configured settings that will be used to generate a new Python editor
        /// </summary>
        public static void PythonConfiguration(ScintillaNET.Scintilla TextArea)
        {
            TextArea.StyleResetDefault();
            Action Styling = () => Styles(TextArea);
            ThemeConfiguration.Configure(1, TextArea, Styling);

            TextArea.Lexer = Lexer.Python;
            TextArea.SetProperty("tab.timmy.whinge.level", "1");
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "0");
            TextArea.SetProperty("fold.comment", "1");
            TextArea.SetProperty("fold.preprocessor", "1");
            TextArea.SetProperty("fold.at.else", "1");

            //Keywords
            string path2keys = Application.StartupPath + "\\Syntax\\Python\\py_syntax.dat";
            TextArea.SetKeywords(0, IndexCore.IndexKeywords(path2keys.ToString()));
            string path2func = Application.StartupPath + "\\Syntax\\Python\\py_functions.dat";
            TextArea.SetKeywords(1, IndexCore.IndexKeywords(path2func.ToString()));

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
            
            TextArea.RegisterRgbaImage(0, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\keywords_icon.png"));
            TextArea.RegisterRgbaImage(1, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\method_icon.png"));
            TextArea.RegisterRgbaImage(2, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\namespace_icon.png"));

            TextArea.ClearCmdKey(Keys.Control | Keys.S);
            TextArea.ClearCmdKey(Keys.Control | Keys.Shift | Keys.S);
            TextArea.ClearCmdKey(Keys.Control | Keys.O);
            TextArea.ClearCmdKey(Keys.Control | Keys.Shift | Keys.O);
            TextArea.ClearCmdKey(Keys.Control | Keys.W);
            TextArea.ClearCmdKey(Keys.Control | Keys.F);
            TextArea.ClearCmdKey(Keys.Control | Keys.G);
            TextArea.ClearCmdKey(Keys.Control | Keys.H);

            TextArea.MouseDwellTime = 800;
        }

        private static void Styles(ScintillaNET.Scintilla TextArea)
        {
            //Lexer
            TextArea.Styles[Style.Python.Default].ForeColor = Properties.Py.Default.Defaulttext;
            TextArea.Styles[Style.Python.Default].Bold = Properties.Py.Default.DefaultBold;
            TextArea.Styles[Style.Python.Default].Italic = Properties.Py.Default.DefaultItalic;

            TextArea.Styles[Style.Python.CommentLine].ForeColor = Properties.Py.Default.LineComments;
            TextArea.Styles[Style.Python.CommentLine].Bold = Properties.Py.Default.LineCommentsBold;
            TextArea.Styles[Style.Python.CommentLine].Italic = Properties.Py.Default.LineCommentsItalic;

            TextArea.Styles[Style.Python.Number].ForeColor = Properties.Py.Default.Numbers;
            TextArea.Styles[Style.Python.Number].Bold = Properties.Py.Default.NumberBold;
            TextArea.Styles[Style.Python.Number].Italic = Properties.Py.Default.NumberItalic;

            TextArea.Styles[Style.Python.String].ForeColor = Properties.Py.Default.Strings;
            TextArea.Styles[Style.Python.String].Bold = Properties.Py.Default.StringBold;
            TextArea.Styles[Style.Python.String].Italic = Properties.Py.Default.StringItalic;

            TextArea.Styles[Style.Python.Character].ForeColor = Properties.Py.Default.Characters;
            TextArea.Styles[Style.Python.Character].Bold = Properties.Py.Default.CharacterBold;
            TextArea.Styles[Style.Python.Character].Italic = Properties.Py.Default.CharacterItalic;

            TextArea.Styles[Style.Python.Word].ForeColor = Properties.Py.Default.Keywords;
            TextArea.Styles[Style.Python.Word].Bold = Properties.Py.Default.KeywordsBold;
            TextArea.Styles[Style.Python.Word].Italic = Properties.Py.Default.KeywordsItalic;

            TextArea.Styles[Style.Python.Triple].ForeColor = Properties.Py.Default.TripleComments;
            TextArea.Styles[Style.Python.Triple].Bold = Properties.Py.Default.TripleCommentsBold;
            TextArea.Styles[Style.Python.Triple].Italic = Properties.Py.Default.TripleCommentsItalic;

            TextArea.Styles[Style.Python.TripleDouble].ForeColor = Properties.Py.Default.TripleDouble;
            TextArea.Styles[Style.Python.TripleDouble].Bold = Properties.Py.Default.TripleDoubleBold;
            TextArea.Styles[Style.Python.TripleDouble].Italic = Properties.Py.Default.TripleDoubleItalic;

            TextArea.Styles[Style.Python.ClassName].ForeColor = Properties.Py.Default.Classnames;
            TextArea.Styles[Style.Python.ClassName].Bold = Properties.Py.Default.ClassnamesBold;
            TextArea.Styles[Style.Python.ClassName].Italic = Properties.Py.Default.ClassnamesItalic;

            TextArea.Styles[Style.Python.DefName].ForeColor = Properties.Py.Default.Definitionnames;
            TextArea.Styles[Style.Python.DefName].Bold = Properties.Py.Default.DefnameBold;
            TextArea.Styles[Style.Python.DefName].Italic = Properties.Py.Default.DefnameItalic;

            TextArea.Styles[Style.Python.CommentBlock].ForeColor = Properties.Py.Default.Block;
            TextArea.Styles[Style.Python.CommentBlock].Bold = Properties.Py.Default.BlockBold;
            TextArea.Styles[Style.Python.CommentBlock].Italic = Properties.Py.Default.BlockItalic;

            TextArea.Styles[Style.Python.StringEol].ForeColor = Properties.Py.Default.EOL;
            TextArea.Styles[Style.Python.StringEol].Bold = Properties.Py.Default.EOLBold;
            TextArea.Styles[Style.Python.StringEol].Italic = Properties.Py.Default.EOLItalic;
            TextArea.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
            TextArea.Styles[Style.Python.StringEol].FillLine = true;

            TextArea.Styles[Style.Python.Word2].ForeColor = Properties.Py.Default.Word2;
            TextArea.Styles[Style.Python.Word2].Bold = Properties.Py.Default.Word2Bold;
            TextArea.Styles[Style.Python.Word2].Italic = Properties.Py.Default.Word2Italic;

            TextArea.Styles[Style.Python.Decorator].ForeColor = Properties.Py.Default.Decorators;
            TextArea.Styles[Style.Python.Decorator].Bold = Properties.Py.Default.DecoratorsBold;
            TextArea.Styles[Style.Python.Decorator].Italic = Properties.Py.Default.DecoratorsItalic;
        }

        public static void TextArea_DwellStart(object sender, DwellEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            var currentPos = e.Position;
            var wordStartPos = TextArea.WordStartPosition(currentPos, true);
            var wordEndPos = TextArea.WordEndPosition(currentPos, true);
            var word = TextArea.GetTextRange(wordStartPos, wordEndPos - wordStartPos);

            foreach (var method in Methods.WordList)
            {
                if (method.ToLower() == word.ToLower())
                {
                    string param = Methods.Hover(method, TextArea);
                    TextArea.CallTipShow(wordStartPos, param);
                }
            }
        }

        public static void TextArea_DwellEnd(object sender, DwellEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            TextArea.CallTipCancel();
        }

        /// <summary>
        /// This method will decide if a Python indentation style is required
        /// </summary>
        public static Regex indentLevel = new Regex("^[\\s]*");
        public static Regex indenter = new Regex(@":$", RegexOptions.Multiline);
        public static void TextArea_InsertCheck(object sender, InsertCheckEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                int startPos = TextArea.Lines[TextArea.LineFromPosition(TextArea.CurrentPosition)].Position;
                int endPos = e.Position;
                string curLineText = TextArea.GetTextRange(startPos, endPos - startPos);
                Match indent = indentLevel.Match(curLineText);
                e.Text = e.Text + indent.Value;
                if (indenter.IsMatch(curLineText))
                {
                    e.Text += '\t'; //e.Text + new string(' ', TextArea.IndentWidth);
                }
            }
        }

        /// <summary>
        /// This method will help the indentation style and intellisense for built-in Python functions
        /// </summary>
        public static void TextArea_CharAdded(object sender, CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Clear indicators
            TextArea.IndicatorClearRange(0, TextArea.TextLength);

            //Matched braces
            ZenModel.InsertMatchedChars(e, TextArea);

            //Intellisense
            try
            {
                Methods.InitCalltips(sender, e, TextArea);
            }
            catch(Exception ex) { }

            //Autocomplete
            if (Properties.Settings.Default.AutoComplete == true)
            {
                var currentPos = TextArea.CurrentPosition;
                var wordStartPos = TextArea.WordStartPosition(currentPos, true);
                var lenEntered = currentPos - wordStartPos;
                int caretPos = TextArea.SelectionStart;
                string word = TextArea.GetWordFromPosition(caretPos - 1);

                string CompletionItems = Application.StartupPath + "\\Syntax\\Python\\Autocomplete\\autoc_items.dat";
                List<string> CompletionList = IndexCore.IndexAutocomplete(CompletionItems);
                TextArea.AutoCIgnoreCase = true;
                TextArea.AutoCOrder = Order.PerformSort;

                if (lenEntered > 0)
                {
                    if (!TextArea.AutoCActive)
                        TextArea.AutoCShow(lenEntered, string.Join(" ", CompletionList.ToArray()));
                }
            }
            else { }

            //Prevent from showing Autocomplete at end of tag
            Helpers.ZenModel.SkipQuotes(TextArea);

            //Skip Braces
            Helpers.ZenModel.SkipBraces(TextArea, e);
        }
    }
}
