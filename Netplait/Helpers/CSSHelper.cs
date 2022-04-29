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
    public class CSSHelper
    {
        /// <summary>
        /// This method will assign all the configured settings that will be used to generate a new CSS editor
        /// </summary>
        public static void CSSConfiguration(ScintillaNET.Scintilla TextArea)
        {
            TextArea.StyleResetDefault();
            Action Styling = () => Styles(TextArea);
            ThemeConfiguration.Configure(3, TextArea, Styling);

            TextArea.Lexer = Lexer.Css;
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "0");
            TextArea.SetProperty("fold.comment", "1");
            TextArea.SetProperty("fold.preprocessor", "1");

            //Keywords
            string path2keys1 = Application.StartupPath + "\\Syntax\\CSS\\css_syntax.dat";
            TextArea.SetKeywords(0, IndexCore.IndexKeywords(path2keys1.ToString()));
            string path2keys2 = Application.StartupPath + "\\Syntax\\CSS\\css-values_syntax.dat";
            TextArea.SetKeywords(1, IndexCore.IndexKeywords(path2keys2.ToString()));

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
            TextArea.RegisterRgbaImage(1, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\cssTag_icon.png"));
            TextArea.RegisterRgbaImage(2, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\namespace_icon.png"));
            TextArea.RegisterRgbaImage(3, new Bitmap(Application.StartupPath + @"\\Icons\\AutoComp\\property_icon.png"));

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
            TextArea.Styles[Style.Css.Default].ForeColor = CSS.Default.Defaulttext;
            TextArea.Styles[Style.Css.Default].Bold = CSS.Default.DefaultBold;
            TextArea.Styles[Style.Css.Default].Italic = CSS.Default.DefaultItalic;

            TextArea.Styles[Style.Css.Attribute].ForeColor = CSS.Default.Attribute;
            TextArea.Styles[Style.Css.Attribute].Bold = CSS.Default.AttributeBold;
            TextArea.Styles[Style.Css.Attribute].Italic = CSS.Default.AttributeItalic;

            TextArea.Styles[Style.Css.Class].ForeColor = CSS.Default.Class;
            TextArea.Styles[Style.Css.Class].Bold = CSS.Default.ClassBold;
            TextArea.Styles[Style.Css.Class].Italic = CSS.Default.ClassItalic;

            TextArea.Styles[Style.Css.Comment].ForeColor = CSS.Default.Comment;
            TextArea.Styles[Style.Css.Comment].Bold = CSS.Default.CommentBold;
            TextArea.Styles[Style.Css.Comment].Italic = CSS.Default.CommentItalic;

            TextArea.Styles[Style.Css.Directive].ForeColor = CSS.Default.Directive;
            TextArea.Styles[Style.Css.Directive].Bold = CSS.Default.DirectiveBold;
            TextArea.Styles[Style.Css.Directive].Italic = CSS.Default.DirectiveItalic;

            TextArea.Styles[Style.Css.DoubleString].ForeColor = CSS.Default.DoubleString;
            TextArea.Styles[Style.Css.DoubleString].Bold = CSS.Default.DoubleStringBold;
            TextArea.Styles[Style.Css.DoubleString].Italic = CSS.Default.DoubleStringItalic;

            TextArea.Styles[Style.Css.ExtendedIdentifier].ForeColor = CSS.Default.ExtendedIdentifier;
            TextArea.Styles[Style.Css.ExtendedIdentifier].Bold = CSS.Default.ExtendedIdentifierBold;
            TextArea.Styles[Style.Css.ExtendedIdentifier].Italic = CSS.Default.ExtendedIdentifierItalic;

            TextArea.Styles[Style.Css.ExtendedPseudoClass].ForeColor = CSS.Default.ExtendedPseudoClass;
            TextArea.Styles[Style.Css.ExtendedPseudoClass].Bold = CSS.Default.ExtendedPseudoClassBold;
            TextArea.Styles[Style.Css.ExtendedPseudoClass].Italic = CSS.Default.ExtendedPseudoClassItalic;

            TextArea.Styles[Style.Css.ExtendedPseudoElement].ForeColor = CSS.Default.ExtendedPseudoElement;
            TextArea.Styles[Style.Css.ExtendedPseudoElement].Bold = CSS.Default.ExtendedPseudoElementBold;
            TextArea.Styles[Style.Css.ExtendedPseudoElement].Italic = CSS.Default.ExtendedPseudoElementItalic;

            TextArea.Styles[Style.Css.Id].ForeColor = CSS.Default.Id;
            TextArea.Styles[Style.Css.Id].Bold = CSS.Default.IdBold;
            TextArea.Styles[Style.Css.Id].Italic = CSS.Default.IdItalic;

            TextArea.Styles[Style.Css.Identifier].ForeColor = CSS.Default.Identifier;
            TextArea.Styles[Style.Css.Identifier].Bold = CSS.Default.IdentifierBold;
            TextArea.Styles[Style.Css.Identifier].Italic = CSS.Default.IdentifierItalic;

            TextArea.Styles[Style.Css.Identifier2].ForeColor = CSS.Default.Identifier2;
            TextArea.Styles[Style.Css.Identifier2].Bold = CSS.Default.Identifier2Bold;
            TextArea.Styles[Style.Css.Identifier2].Italic = CSS.Default.Identifier2Italic;

            TextArea.Styles[Style.Css.Identifier3].ForeColor = CSS.Default.Identifier3;
            TextArea.Styles[Style.Css.Identifier3].Bold = CSS.Default.Identifier3Bold;
            TextArea.Styles[Style.Css.Identifier3].Italic = CSS.Default.Identifier3Italic;

            TextArea.Styles[Style.Css.Important].ForeColor = CSS.Default.Important;
            TextArea.Styles[Style.Css.Important].Bold = CSS.Default.ImportantBold;
            TextArea.Styles[Style.Css.Important].Italic = CSS.Default.ImportantItalic;

            TextArea.Styles[Style.Css.Media].ForeColor = CSS.Default.Media;
            TextArea.Styles[Style.Css.Media].Bold = CSS.Default.MediaBold;
            TextArea.Styles[Style.Css.Media].Italic = CSS.Default.MediaItalic;

            TextArea.Styles[Style.Css.Operator].ForeColor = CSS.Default.Operator;
            TextArea.Styles[Style.Css.Operator].Bold = CSS.Default.OperatorBold;
            TextArea.Styles[Style.Css.Operator].Italic = CSS.Default.OperatorItalic;

            TextArea.Styles[Style.Css.PseudoClass].ForeColor = CSS.Default.PseudoClass;
            TextArea.Styles[Style.Css.PseudoClass].Bold = CSS.Default.PseudoClassBold;
            TextArea.Styles[Style.Css.PseudoClass].Italic = CSS.Default.PseudoClassItalic;

            TextArea.Styles[Style.Css.PseudoElement].ForeColor = CSS.Default.PseudoElement;
            TextArea.Styles[Style.Css.PseudoElement].Bold = CSS.Default.PseudoElementBold;
            TextArea.Styles[Style.Css.PseudoElement].Italic = CSS.Default.PseudoElementItalic;

            TextArea.Styles[Style.Css.SingleString].ForeColor = CSS.Default.SingleString;
            TextArea.Styles[Style.Css.SingleString].Bold = CSS.Default.SingleStringBold;
            TextArea.Styles[Style.Css.SingleString].Italic = CSS.Default.SingleStringItalic;

            TextArea.Styles[Style.Css.Tag].ForeColor = CSS.Default.Tag;
            TextArea.Styles[Style.Css.Tag].Bold = CSS.Default.TagBold;
            TextArea.Styles[Style.Css.Tag].Italic = CSS.Default.TagItalic;

            TextArea.Styles[Style.Css.UnknownIdentifier].ForeColor = CSS.Default.UnknownIdentifier;
            TextArea.Styles[Style.Css.UnknownIdentifier].Bold = CSS.Default.UnknownIdentifierBold;
            TextArea.Styles[Style.Css.UnknownIdentifier].Italic = CSS.Default.UnknownIdentifierItalic;

            TextArea.Styles[Style.Css.UnknownPseudoClass].ForeColor = CSS.Default.UnknownPseudoClass;
            TextArea.Styles[Style.Css.UnknownPseudoClass].Bold = CSS.Default.UnknownPseudoClassBold;
            TextArea.Styles[Style.Css.UnknownPseudoClass].Italic = CSS.Default.UnknownPseudoClassItalic;

            TextArea.Styles[Style.Css.Value].ForeColor = CSS.Default.Value;
            TextArea.Styles[Style.Css.Value].Bold = CSS.Default.ValueBold;
            TextArea.Styles[Style.Css.Value].Italic = CSS.Default.ValueItalic;

            TextArea.Styles[Style.Css.Variable].ForeColor = CSS.Default.Variable;
            TextArea.Styles[Style.Css.Variable].Bold = CSS.Default.VariableBold;
            TextArea.Styles[Style.Css.Variable].Italic = CSS.Default.VariableItalic;
        }

        /// <summary>
        /// This method will decide if a CSS indentation style is required
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
        /// This method will help the indentation style and intellisense for built-in CSS functions
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

                string CompletionItems = Application.StartupPath + "\\Syntax\\CSS\\Autocomplete\\autoc_items.dat";
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

            //Colors for autocompletion
            if (e.Char == 0x23)
                TextArea.AutoCShow(lenEntered, "Colors...?0");

            //Skip Braces
            Helpers.ZenModel.SkipBraces(TextArea, e);
        }
    }
}
