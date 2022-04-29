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
    public class JSHelper
    {
        /// <summary>
        /// This method will assign all the configured settings that will be used to generate a new Javascript editor
        /// </summary>
        public static void JSConfiguration(ScintillaNET.Scintilla TextArea)
        {
            TextArea.StyleResetDefault();
            Action Styling = () => Styles(TextArea);
            ThemeConfiguration.Configure(4, TextArea, Styling);

            TextArea.Lexer = Lexer.Cpp;
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "0");
            TextArea.SetProperty("fold.comment", "1");
            TextArea.SetProperty("fold.preprocessor", "1");
            TextArea.SetProperty("fold.at.else", "1");

            //Keywords
            string path2syntax = Application.StartupPath + "\\Syntax\\JS\\js_syntax.dat";
            TextArea.SetKeywords(0, IndexCore.IndexKeywords(path2syntax.ToString()));
            string path2keys1 = Application.StartupPath + "\\Syntax\\JS\\js_keywords.dat";
            TextArea.SetKeywords(1, IndexCore.IndexKeywords(path2keys1.ToString()));

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

            TextArea.RegisterRgbaImage(0, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\asterisk_icon.png"));
            TextArea.RegisterRgbaImage(1, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\keywords_icon.png"));
            TextArea.RegisterRgbaImage(2, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\property_icon.png"));

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
            TextArea.Styles[Style.Cpp.Default].ForeColor = JS.Default.Defaulttext;
            TextArea.Styles[Style.Cpp.Default].Bold = JS.Default.DefaulttextBold;
            TextArea.Styles[Style.Cpp.Default].Italic = JS.Default.DefaulttextItalic;

            TextArea.Styles[Style.Cpp.Comment].ForeColor = JS.Default.Comment;
            TextArea.Styles[Style.Cpp.Comment].Bold = JS.Default.CommentBold;
            TextArea.Styles[Style.Cpp.Comment].Italic = JS.Default.CommentItalic;

            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = JS.Default.CommentLine;
            TextArea.Styles[Style.Cpp.CommentLine].Bold = JS.Default.CommentLineBold;
            TextArea.Styles[Style.Cpp.CommentLine].Italic = JS.Default.CommentLineItalic;

            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = JS.Default.CommentDoc;
            TextArea.Styles[Style.Cpp.CommentDoc].Bold = JS.Default.CommentDocBold;
            TextArea.Styles[Style.Cpp.CommentDoc].Italic = JS.Default.CommentDocItalic;

            TextArea.Styles[Style.Cpp.Number].ForeColor = JS.Default.Number;
            TextArea.Styles[Style.Cpp.Number].Bold = JS.Default.NumberBold;
            TextArea.Styles[Style.Cpp.Number].Italic = JS.Default.NumberItalic;

            TextArea.Styles[Style.Cpp.StringEol].ForeColor = JS.Default.StringEol;
            TextArea.Styles[Style.Cpp.StringEol].Bold = JS.Default.StringEolBold;
            TextArea.Styles[Style.Cpp.StringEol].Italic = JS.Default.StringEolItalic;

            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = JS.Default.CommentLineDoc;
            TextArea.Styles[Style.Cpp.CommentLineDoc].Bold = JS.Default.CommentLineDocBold;
            TextArea.Styles[Style.Cpp.CommentLineDoc].Italic = JS.Default.CommentLineDocItalic;

            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = JS.Default.CommentDocKeyword;
            TextArea.Styles[Style.Cpp.CommentDocKeyword].Bold = JS.Default.CommentDocKeywordBold;
            TextArea.Styles[Style.Cpp.CommentDocKeyword].Italic = JS.Default.CommentDocKeywordItalic;

            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = JS.Default.CommentDocKeywordError;
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].Bold = JS.Default.CommentDocKeywordErrorBold;
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].Italic = JS.Default.CommentDocKeywordErrorItalic;

            TextArea.Styles[Style.Cpp.Word].ForeColor = JS.Default.Word;
            TextArea.Styles[Style.Cpp.Word].Bold = JS.Default.WordBold;
            TextArea.Styles[Style.Cpp.Word].Italic = JS.Default.WordItalic;

            TextArea.Styles[Style.Cpp.Word2].ForeColor = JS.Default.Word2;
            TextArea.Styles[Style.Cpp.Word2].Bold = JS.Default.Word2Bold;
            TextArea.Styles[Style.Cpp.Word2].Italic = JS.Default.Word2Italic;

            TextArea.Styles[Style.Cpp.String].ForeColor = JS.Default.String;
            TextArea.Styles[Style.Cpp.String].Bold = JS.Default.StringBold;
            TextArea.Styles[Style.Cpp.String].Italic = JS.Default.StringItalic;

            TextArea.Styles[Style.Cpp.Character].ForeColor = JS.Default.Character;
            TextArea.Styles[Style.Cpp.Character].Bold = JS.Default.CharacterBold;
            TextArea.Styles[Style.Cpp.Character].Italic = JS.Default.CharacterItalic;

            TextArea.Styles[Style.Cpp.Uuid].ForeColor = JS.Default.UUID;
            TextArea.Styles[Style.Cpp.Uuid].Bold = JS.Default.UUIDBold;
            TextArea.Styles[Style.Cpp.Uuid].Italic = JS.Default.UUIDItalic;

            TextArea.Styles[Style.Cpp.Operator].ForeColor = JS.Default.Operator;
            TextArea.Styles[Style.Cpp.Operator].Bold = JS.Default.OperatorBold;
            TextArea.Styles[Style.Cpp.Operator].Italic = JS.Default.Operatoritalic;

            TextArea.Styles[Style.Cpp.Identifier].ForeColor = JS.Default.Identifier;
            TextArea.Styles[Style.Cpp.Identifier].Bold = JS.Default.IdentifierBold;
            TextArea.Styles[Style.Cpp.Identifier].Italic = JS.Default.IdentifierItalic;

            TextArea.Styles[Style.Cpp.Verbatim].ForeColor = JS.Default.Verbatim;
            TextArea.Styles[Style.Cpp.Verbatim].Bold = JS.Default.VerbatimBold;
            TextArea.Styles[Style.Cpp.Verbatim].Italic = JS.Default.VerbatimItalic;

            TextArea.Styles[Style.Cpp.Regex].ForeColor = JS.Default.Regex;
            TextArea.Styles[Style.Cpp.Regex].Bold = JS.Default.RegexBold;
            TextArea.Styles[Style.Cpp.Regex].Italic = JS.Default.RegexItalic;
        }

        /// <summary>
        /// This method will decide if a Javascript indentation style is required
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
        /// This method will help the indentation style and intellisense for built-in Javascript functions
        /// </summary>
        public static void TextArea_CharAdded(object sender, CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Clear indicators
            TextArea.IndicatorClearRange(0, TextArea.TextLength);

            var currentPos = TextArea.CurrentPosition;
            var wordStartPos = TextArea.WordStartPosition(currentPos, true);
            var lenEntered = currentPos - wordStartPos;

            //Indentation
            if ((e.Char == 125))
            {
                int curLine = TextArea.LineFromPosition(TextArea.CurrentPosition);
                if (IndentationController.outdentWords.Contains(TextArea.Lines[curLine].Text.Trim()))
                {
                    TextArea.Lines[curLine].Indentation -= TextArea.IndentWidth;
                }
            }

            //Matched braces
            ZenModel.InsertMatchedChars(e, TextArea);

            //Autocomplete
            if (Properties.Settings.Default.AutoComplete == true)
            {
                int caretPos = TextArea.SelectionStart;
                string word = TextArea.GetWordFromPosition(caretPos - 1);

                string CompletionItems = Application.StartupPath + "\\Syntax\\JS\\Autocomplete\\autoc_items.dat";
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
            var caretPosition = TextArea.CurrentPosition;
            if (TextArea.GetCharAt(caretPosition) == '"' || TextArea.GetCharAt(caretPosition) == '\'')
            {
                TextArea.AutoCCancel();
            }

            //Skip Braces
            Helpers.ZenModel.SkipBraces(TextArea, e);
        }
    }
}
