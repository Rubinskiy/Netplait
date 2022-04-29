using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using System.Text.RegularExpressions;
using System.Drawing;
using Netplait.Core.Misc;
using Netplait.Core.Indexing;
using Netplait.Core.Indenting;

namespace Netplait.Helpers
{
    public class HTMLHelper
    {
        /// <summary>
        /// This method will assign all the configured settings that will be used to generate a new HTML editor
        /// </summary>
        public static void HTMLConfiguration(ScintillaNET.Scintilla TextArea)
        {
            TextArea.StyleResetDefault();
            Action Styling = () => Styles(TextArea);
            ThemeConfiguration.Configure(2, TextArea, Styling);

            TextArea.Lexer = Lexer.Html;
            TextArea.SetProperty("fold.html", "1");
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "0");
            TextArea.SetProperty("fold.comment", "1");
            TextArea.SetProperty("fold.preprocessor", "1");
            TextArea.SetProperty("fold.at.else", "1");

            //Keywords
            string path2keys = Application.StartupPath + "\\Syntax\\HTML\\html_syntax.dat";
            TextArea.SetKeywords(0, IndexCore.IndexKeywords(path2keys.ToString()));   

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

            TextArea.RegisterRgbaImage(0, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\color_icon.png"));
            TextArea.RegisterRgbaImage(1, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\keywords_icon.png"));
            TextArea.RegisterRgbaImage(2, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\property_icon.png"));
            TextArea.RegisterRgbaImage(3, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\event_icon.png"));

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
            TextArea.Styles[Style.Html.Tag].ForeColor = Properties.HTML.Default.Tag;
            TextArea.Styles[Style.Html.Tag].Bold = Properties.HTML.Default.TagBold;
            TextArea.Styles[Style.Html.Tag].Italic = Properties.HTML.Default.TagItalic;

            TextArea.Styles[Style.Html.TagUnknown].ForeColor = Properties.HTML.Default.TagUnknown;
            TextArea.Styles[Style.Html.TagUnknown].Bold = Properties.HTML.Default.TagUnknownBold;
            TextArea.Styles[Style.Html.TagUnknown].Italic = Properties.HTML.Default.TagUnknownItalic;

            TextArea.Styles[Style.Html.Attribute].ForeColor = Properties.HTML.Default.Attribute;
            TextArea.Styles[Style.Html.Attribute].Bold = Properties.HTML.Default.AttributeBold;
            TextArea.Styles[Style.Html.Attribute].Italic = Properties.HTML.Default.AttributeItalic;

            TextArea.Styles[Style.Html.AttributeUnknown].ForeColor = Properties.HTML.Default.AttributeUnknown;
            TextArea.Styles[Style.Html.AttributeUnknown].Bold = Properties.HTML.Default.AttributeUnknownBold;
            TextArea.Styles[Style.Html.AttributeUnknown].Italic = Properties.HTML.Default.AttributeUnknownItalic;

            TextArea.Styles[Style.Html.Number].ForeColor = Properties.HTML.Default.Number;
            TextArea.Styles[Style.Html.Number].Bold = Properties.HTML.Default.NumberBold;
            TextArea.Styles[Style.Html.Number].Italic = Properties.HTML.Default.NumberItalic;

            TextArea.Styles[Style.Html.DoubleString].ForeColor = Properties.HTML.Default.DoubleString;
            TextArea.Styles[Style.Html.DoubleString].Bold = Properties.HTML.Default.DoubleStringBold;
            TextArea.Styles[Style.Html.DoubleString].Italic = Properties.HTML.Default.DoubleStringItalic;

            TextArea.Styles[Style.Html.SingleString].ForeColor = Properties.HTML.Default.SingleString;
            TextArea.Styles[Style.Html.SingleString].Bold = Properties.HTML.Default.SingleStringBold;
            TextArea.Styles[Style.Html.SingleString].Italic = Properties.HTML.Default.SingleStringItalic;

            TextArea.Styles[Style.Html.Other].ForeColor = Properties.HTML.Default.Other;
            TextArea.Styles[Style.Html.Other].Bold = Properties.HTML.Default.OtherBold;
            TextArea.Styles[Style.Html.Other].Italic = Properties.HTML.Default.OtherItalic;

            TextArea.Styles[Style.Html.Comment].ForeColor = Properties.HTML.Default.Comment;
            TextArea.Styles[Style.Html.Comment].Bold = Properties.HTML.Default.CommentBold;
            TextArea.Styles[Style.Html.Comment].Italic = Properties.HTML.Default.CommentItalic;

            TextArea.Styles[Style.Html.Entity].ForeColor = Properties.HTML.Default.Entity;
            TextArea.Styles[Style.Html.Entity].Bold = Properties.HTML.Default.EntityBold;
            TextArea.Styles[Style.Html.Entity].Italic = Properties.HTML.Default.EntityItalic;

            TextArea.Styles[Style.Html.Script].ForeColor = Properties.HTML.Default.Script;
            TextArea.Styles[Style.Html.Script].Bold = Properties.HTML.Default.ScriptBold;
            TextArea.Styles[Style.Html.Script].Italic = Properties.HTML.Default.ScriptItalic;

            TextArea.Styles[Style.Html.CData].ForeColor = Properties.HTML.Default.CData;
            TextArea.Styles[Style.Html.CData].Bold = Properties.HTML.Default.CDataBold;
            TextArea.Styles[Style.Html.CData].Italic = Properties.HTML.Default.CDataItalic;

            TextArea.Styles[Style.Html.Question].ForeColor = Properties.HTML.Default.Question;
            TextArea.Styles[Style.Html.Question].Bold = Properties.HTML.Default.QuestionBold;
            TextArea.Styles[Style.Html.Question].Italic = Properties.HTML.Default.QuestionItalic;

            TextArea.Styles[Style.Html.Value].ForeColor = Properties.HTML.Default.Value;
            TextArea.Styles[Style.Html.Value].Bold = Properties.HTML.Default.ValueBold;
            TextArea.Styles[Style.Html.Value].Italic = Properties.HTML.Default.ValueItalic;

            TextArea.Styles[Style.Html.XcComment].ForeColor = Properties.HTML.Default.XcComment;
            TextArea.Styles[Style.Html.XcComment].Bold = Properties.HTML.Default.XcCommentBold;
            TextArea.Styles[Style.Html.XcComment].Italic = Properties.HTML.Default.XcCommentItalic;

            TextArea.Styles[Style.Html.TagEnd].ForeColor = Properties.HTML.Default.TagEnd;
            TextArea.Styles[Style.Html.TagEnd].Bold = Properties.HTML.Default.TagEndBold;
            TextArea.Styles[Style.Html.TagEnd].Italic = Properties.HTML.Default.TagEndItalic;

            TextArea.Styles[Style.Html.XmlStart].ForeColor = Properties.HTML.Default.XmlStart;
            TextArea.Styles[Style.Html.XmlStart].Bold = Properties.HTML.Default.XmlStartBold;
            TextArea.Styles[Style.Html.XmlStart].Italic = Properties.HTML.Default.XmlStartItalic;

            TextArea.Styles[Style.Html.XmlEnd].ForeColor = Properties.HTML.Default.XmlEnd;
            TextArea.Styles[Style.Html.XmlEnd].Bold = Properties.HTML.Default.XmlEndBold;
            TextArea.Styles[Style.Html.XmlEnd].Italic = Properties.HTML.Default.XmlEndItalic;
        }

        /// <summary>
        /// Does the completed tag supposed to be closed '<tag></tag>' or self-enclosed '<tag />' ?
        /// </summary>
        public static void TextArea_AutoCCompleted(object sender, AutoCSelectionEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Autoinsert e.g. 'lang' => 'lang=""'
            int caretPos = TextArea.SelectionStart;
            string word = TextArea.GetWordFromPosition(caretPos - 1);

            string Tags = Application.StartupPath + "\\Syntax\\HTML\\insert_tags.dat";
            List<string> InsertList = IndexCore.IndexAutocomplete(Tags);

            foreach (var key in InsertList)
            {                
                if (word.EndsWith(key.ToString()))
                {
                    string str = "=\"\"";
                    TextArea.InsertText(caretPos, str);
                    TextArea.GotoPosition(caretPos + str.Length - 1);
                }
                else { }
            }

            string Tags1 = Application.StartupPath + "\\Syntax\\HTML\\self_insert_tags.dat";
            List<string> InsertList1 = IndexCore.IndexAutocomplete(Tags1);
            foreach (var key in InsertList1)
            {
                if (word.EndsWith(key.ToString()))
                {
                    string str = " />";
                    TextArea.InsertText(caretPos, str);
                }
                else { }
            }
        }        

        /// <summary>
        /// This method will decide if an HTML indentation style is required
        /// </summary>
        public static void TextArea_InsertCheck(object sender, InsertCheckEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                int startPos = TextArea.Lines[TextArea.LineFromPosition(TextArea.CurrentPosition)].Position;
                int endPos = e.Position;
                string curLineText = TextArea.GetTextRange(startPos, endPos - startPos);
                // Text until the caret.
                Match indent = IndentationController.indentLevel.Match(curLineText);
                e.Text = e.Text + indent.Value;
                if (IndentationController.indenter.IsMatch(curLineText))
                {
                    e.Text = e.Text + new string(' ', TextArea.IndentWidth);
                }

                var curLine = TextArea.LineFromPosition(e.Position);
                var caretPos = TextArea.CurrentPosition;
                var curLineNew = TextArea.Lines[curLine].Text;
            }
        }

        /// <summary>
        /// This method will help the indentation style for the HTML editor
        /// </summary>
        public static void TextArea_CharAdded(object sender, CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            //Clear indicators
            TextArea.IndicatorClearRange(0, TextArea.TextLength);

            var currentPos = TextArea.CurrentPosition;
            var wordStartPos = TextArea.WordStartPosition(currentPos, true);
            var lenEntered = currentPos - wordStartPos;

            //Autocomplete
            if (Properties.Settings.Default.AutoComplete == true)
            {                
                int caretPos = TextArea.SelectionStart;
                string word = TextArea.GetWordFromPosition(caretPos - 1);
                int x = TextArea.PointXFromPosition(TextArea.CurrentPosition);

                string CompletionItems = Application.StartupPath + "\\Syntax\\HTML\\Autocomplete\\autoc_items.dat";
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

            //Indentation
            ZenModel.InsertMatchedChars(e, TextArea);
            if ((e.Char == 125))
            {
                int curLine = TextArea.LineFromPosition(TextArea.CurrentPosition);
                if (IndentationController.outdentWords.Contains(TextArea.Lines[curLine].Text.Trim()))
                {
                    TextArea.Lines[curLine].Indentation -= TextArea.IndentWidth;
                }
            }

            //Prevent from showing Autocomplete at end of tag
            var caretPosition = TextArea.CurrentPosition;
            if (TextArea.GetCharAt(caretPosition) == '"' || TextArea.GetCharAt(caretPosition) == '\'')
            {
                TextArea.AutoCCancel();
            }

            //Add closing tags
            Helpers.ZenModel.InsertClosingTag(TextArea, e);

            //InitZenCoding
            Helpers.ZenModel.InitZenCoding(TextArea, e);

            //Colors for autocompletion
            if (e.Char == 0x23)
                TextArea.AutoCShow(lenEntered, "Colors...?0");

            //Skip Braces
            Helpers.ZenModel.SkipBraces(TextArea, e);
        }
    }
}