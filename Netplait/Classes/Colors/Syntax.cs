using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Netplait.Properties;

namespace Netplait.Classes.Colors
{
    public partial class Syntax : Form
    {
        public Syntax()
        {
            InitializeComponent();

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            tabControl2.Appearance = TabAppearance.FlatButtons;
            tabControl2.ItemSize = new Size(0, 1);
            tabControl2.SizeMode = TabSizeMode.Fixed;

            tabControl4.Appearance = TabAppearance.FlatButtons;
            tabControl4.ItemSize = new Size(0, 1);
            tabControl4.SizeMode = TabSizeMode.Fixed;

            tabControl5.Appearance = TabAppearance.FlatButtons;
            tabControl5.ItemSize = new Size(0, 1);
            tabControl5.SizeMode = TabSizeMode.Fixed;

            tabControl6.Appearance = TabAppearance.FlatButtons;
            tabControl6.ItemSize = new Size(0, 1);
            tabControl6.SizeMode = TabSizeMode.Fixed;

            //comboBox7
            comboBox7.Items.Add("Light");
            comboBox7.Items.Add("Dark Nimbus (Experimental)");
            if (Settings.Default.DarkTheme == false) { comboBox7.SelectedIndex = 0; }
            else if (Settings.Default.DarkTheme == true) { comboBox7.SelectedIndex = 1; }

            #region Panel_clicked
            foreach (TabPage tp in tabControl1.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (TabPage tp in tabControl2.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (TabPage tp in tabControl3.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (TabPage tp in tabControl4.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (TabPage tp in tabControl5.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (TabPage tp in tabControl6.TabPages)
            {
                foreach (Control control in tp.Controls)
                {
                    if (control is Panel)
                    {
                        Panel p = (Panel)control;
                        p.Click += new EventHandler(Panel_click);
                    }
                }
            }

            foreach (Control control in lightGroup.Controls)
            {
                if (control is Panel)
                {
                    Panel p = (Panel)control;
                    p.Click += new EventHandler(Panel_click);
                }
            }

            foreach (Control control in darkGroup.Controls)
            {
                if (control is Panel)
                {
                    Panel p = (Panel)control;
                    p.Click += new EventHandler(Panel_click);
                }
            }
            #endregion

            #region Python
            //Load python
            pythonList.SelectedIndex = 0;

            //Backcolors
            pythonDarkback.BackColor = Py.Default.BackColorDark;
            pythonDarkfore.BackColor = Py.Default.ForeColorDark;
            pythonLightback.BackColor = Py.Default.BackColorWhite;
            pythonLightfore.BackColor = Py.Default.ForeColorWhite;

            //Caret
            caretBack.BackColor = Py.Default.Caret;

            //Others
            defaultBack.BackColor = Py.Default.Defaulttext;
            defaultBold.Checked = Py.Default.DefaultBold;
            defaultItalic.Checked = Py.Default.DefaultItalic;

            numberBack.BackColor = Py.Default.Numbers;
            numberBold.Checked = Py.Default.NumberBold;
            numberItalic.Checked = Py.Default.NumberItalic;

            stringBack.BackColor = Py.Default.Strings;
            stringBold.Checked = Py.Default.StringBold;
            stringItalic.Checked = Py.Default.StringItalic;

            characterBack.BackColor = Py.Default.Characters;
            characterBold.Checked = Py.Default.CharacterBold;
            characterItalic.Checked = Py.Default.CharacterItalic;

            wordBack.BackColor = Py.Default.Keywords;
            wordBold.Checked = Py.Default.KeywordsBold;
            wordItalic.Checked = Py.Default.KeywordsItalic;

            classBack.BackColor = Py.Default.Classnames;
            classBold.Checked = Py.Default.ClassnamesBold;
            classItalic.Checked = Py.Default.ClassnamesItalic;

            defBack.BackColor = Py.Default.Definitionnames;
            defBold.Checked = Py.Default.DefnameBold;
            defItalic.Checked = Py.Default.DefnameItalic;

            word2Back.BackColor = Py.Default.Word2;
            word2Bold.Checked = Py.Default.Word2Bold;
            word2Italic.Checked = Py.Default.Word2Italic;

            decoratorBack.BackColor = Py.Default.Decorators;
            decoratorBold.Checked = Py.Default.DecoratorsBold;
            decoratorItalic.Checked = Py.Default.DecoratorsItalic;

            lineBack.BackColor = Py.Default.LineComments;
            lineBold.Checked = Py.Default.LineCommentsBold;
            lineItalic.Checked = Py.Default.LineCommentsItalic;

            tripleBack.BackColor = Py.Default.TripleComments;
            tripleBold.Checked = Py.Default.TripleCommentsBold;
            tripleItalic.Checked = Py.Default.TripleCommentsItalic;

            tripledoubleBack.BackColor = Py.Default.TripleDouble;
            tripledoubleBold.Checked = Py.Default.TripleDoubleBold;
            tripledoubleItalic.Checked = Py.Default.TripleDoubleItalic;

            blockBack.BackColor = Py.Default.Block;
            blockBold.Checked = Py.Default.BlockBold;
            blockItalic.Checked = Py.Default.BlockItalic;

            eolBack.BackColor = Py.Default.EOL;
            eolBold.Checked = Py.Default.EOLBold;
            eolItalic.Checked = Py.Default.EOLItalic;
            #endregion

            #region HTML
            //Load HTML
            HTMLlist.SelectedIndex = 0;

            //Caret
            caretBackHTML.BackColor = HTML.Default.Caret;

            //Others
            tagBack.BackColor = HTML.Default.Tag;
            tagBold.Checked = HTML.Default.TagBold;
            tagItalic.Checked = HTML.Default.TagItalic;

            tagUnknownBack.BackColor = HTML.Default.TagUnknown;
            tagUnknownBold.Checked = HTML.Default.TagUnknownBold;
            tagUnknownItalic.Checked = HTML.Default.TagUnknownItalic;

            attributeBack.BackColor = HTML.Default.Attribute;
            attributeBold.Checked = HTML.Default.AttributeBold;
            attributeItalic.Checked = HTML.Default.AttributeItalic;

            attributeUnknownBack.BackColor = HTML.Default.AttributeUnknown;
            attributeUnknownBold.Checked = HTML.Default.AttributeUnknownBold;
            attributeUnknownItalic.Checked = HTML.Default.AttributeUnknownItalic;

            numberBackHTML.BackColor = HTML.Default.Number;
            numberBold.Checked = HTML.Default.NumberBold;
            numberItalic.Checked = HTML.Default.NumberItalic;

            doubleStringBack.BackColor = HTML.Default.DoubleString;
            doubleStringBold.Checked = HTML.Default.DoubleStringBold;
            doubleStringItalic.Checked = HTML.Default.DoubleStringItalic;

            singleStringBack.BackColor = HTML.Default.SingleString;
            singleStringBold.Checked = HTML.Default.SingleStringBold;
            singleStringItalic.Checked = HTML.Default.SingleStringItalic;

            otherBack.BackColor = HTML.Default.Other;
            otherBold.Checked = HTML.Default.OtherBold;
            otherItalic.Checked = HTML.Default.OtherItalic;

            commentBack.BackColor = HTML.Default.Comment;
            commentBold.Checked = HTML.Default.CommentBold;
            commentItalic.Checked = HTML.Default.CommentItalic;

            entityBack.BackColor = HTML.Default.Entity;
            entityBold.Checked = HTML.Default.EntityBold;
            entityItalic.Checked = HTML.Default.EntityItalic;

            scriptBack.BackColor = HTML.Default.Script;
            scriptBold.Checked = HTML.Default.ScriptBold;
            scriptItalic.Checked = HTML.Default.ScriptItalic;

            cdataBack.BackColor = HTML.Default.CData;
            cdataBold.Checked = HTML.Default.CDataBold;
            cdataItalic.Checked = HTML.Default.CDataItalic;

            questionBack.BackColor = HTML.Default.Question;
            questionBold.Checked = HTML.Default.QuestionBold;
            questionItalic.Checked = HTML.Default.QuestionItalic;

            valueBack.BackColor = HTML.Default.Value;
            valueBold.Checked = HTML.Default.ValueBold;
            valueItalic.Checked = HTML.Default.ValueItalic;

            xccommentBack.BackColor = HTML.Default.XcComment;
            xccommentBold.Checked = HTML.Default.XcCommentBold;
            xccommentItalic.Checked = HTML.Default.XcCommentItalic;

            tagEndBack.BackColor = HTML.Default.TagEnd;
            tagEndBold.Checked = HTML.Default.TagEndBold;
            tagEndItalic.Checked = HTML.Default.TagEndItalic;

            xmlStartBack.BackColor = HTML.Default.XmlStart;
            xmlStartBold.Checked = HTML.Default.XmlStartBold;
            xmlStartItalic.Checked = HTML.Default.XmlStartItalic;

            xmlEndBack.BackColor = HTML.Default.XmlEnd;
            xmlEndBold.Checked = HTML.Default.XmlEndBold;
            xmlEndItalic.Checked = HTML.Default.XmlEndItalic;
            #endregion

            #region CSS
            //Load CSS
            CSSlist.SelectedIndex = 0;

            //Caret
            caretBackCSS.BackColor = CSS.Default.Caret;

            //Others
            defaultBackCSS.BackColor = CSS.Default.Defaulttext;
            defaultBoldCSS.Checked = CSS.Default.DefaultBold;
            defaultItalicCSS.Checked = CSS.Default.DefaultItalic;

            attributeBackCSS.BackColor = CSS.Default.Attribute;
            attributeBoldCSS.Checked = CSS.Default.AttributeBold;
            attributeItalicCSS.Checked = CSS.Default.AttributeItalic;

            classBackCSS.BackColor = CSS.Default.Class;
            classBoldCSS.Checked = CSS.Default.ClassBold;
            classItalicCSS.Checked = CSS.Default.ClassItalic;

            commentBackCSS.BackColor = CSS.Default.Comment;
            commentBoldCSS.Checked = CSS.Default.CommentBold;
            commentItalicCSS.Checked = CSS.Default.CommentItalic;

            directiveBack.BackColor = CSS.Default.Directive;
            directiveBold.Checked = CSS.Default.DirectiveBold;
            directiveItalic.Checked = CSS.Default.DirectiveItalic;

            doublestringBackCSS.BackColor = CSS.Default.DoubleString;
            doublestringBoldCSS.Checked = CSS.Default.DoubleStringBold;
            doublestringItalicCSS.Checked = CSS.Default.DoubleStringItalic;

            extendedidentifierBack.BackColor = CSS.Default.ExtendedIdentifier;
            extendedidentifierBold.Checked = CSS.Default.ExtendedIdentifierBold;
            extendedidentifierItalic.Checked = CSS.Default.ExtendedIdentifierItalic;

            extendedpseudoclassBack.BackColor = CSS.Default.ExtendedPseudoClass;
            extendedpseudoclassBold.Checked = CSS.Default.ExtendedPseudoClassBold;
            extendedpseudoclassItalic.Checked = CSS.Default.ExtendedPseudoClassItalic;

            extendedpseudocelementBack.BackColor = CSS.Default.ExtendedPseudoElement;
            extendedpseudocelementBold.Checked = CSS.Default.ExtendedPseudoElementBold;
            extendedpseudocelementItalic.Checked = CSS.Default.ExtendedPseudoElementItalic;

            idBack.BackColor = CSS.Default.Id;
            idBold.Checked = CSS.Default.IdBold;
            idItalic.Checked = CSS.Default.IdItalic;

            identifierBack.BackColor = CSS.Default.Identifier;
            identifierBold.Checked = CSS.Default.IdentifierBold;
            identifierItalic.Checked = CSS.Default.IdentifierItalic;

            identifier2Back.BackColor = CSS.Default.Identifier2;
            identifier2Bold.Checked = CSS.Default.Identifier2Bold;
            identifier2Italic.Checked = CSS.Default.Identifier2Italic;

            identifier3Back.BackColor = CSS.Default.Identifier3;
            identifier3Bold.Checked = CSS.Default.Identifier3Bold;
            identifier3Italic.Checked = CSS.Default.Identifier3Italic;

            importantBack.BackColor = CSS.Default.Important;
            importantBold.Checked = CSS.Default.ImportantBold;
            importantItalic.Checked = CSS.Default.ImportantItalic;

            mediaBack.BackColor = CSS.Default.Media;
            mediaBold.Checked = CSS.Default.MediaBold;
            mediaItalic.Checked = CSS.Default.MediaItalic;

            operatorBack.BackColor = CSS.Default.Operator;
            operatorBold.Checked = CSS.Default.OperatorBold;
            operatorItalic.Checked = CSS.Default.OperatorItalic;

            pseudoclassBack.BackColor = CSS.Default.PseudoClass;
            pseudoclassBold.Checked = CSS.Default.PseudoClassBold;
            pseudoclassItalic.Checked = CSS.Default.PseudoClassItalic;

            pseudoelementBack.BackColor = CSS.Default.PseudoElement;
            pseudoelementBold.Checked = CSS.Default.PseudoElementBold;
            pseudoelementItalic.Checked = CSS.Default.PseudoElementItalic;

            singlestringBackCSS.BackColor = CSS.Default.SingleString;
            singleStringBold.Checked = CSS.Default.SingleStringBold;
            singleStringItalic.Checked = CSS.Default.SingleStringItalic;

            tagBackCSS.BackColor = CSS.Default.Tag;
            tagBoldCSS.Checked = CSS.Default.TagBold;
            tagItalicCSS.Checked = CSS.Default.TagItalic;

            unknownidentifierBack.BackColor = CSS.Default.UnknownIdentifier;
            unknownidentifierBold.Checked = CSS.Default.UnknownIdentifierBold;
            unknownidentifierItalic.Checked = CSS.Default.UnknownIdentifierItalic;

            unknownpseudoclassBack.BackColor = CSS.Default.UnknownPseudoClass;
            unknownpseudoclassBold.Checked = CSS.Default.UnknownPseudoClassBold;
            unknownpseudoclassItalic.Checked = CSS.Default.UnknownPseudoClassItalic;

            valueBackCSS.BackColor = CSS.Default.Value;
            valueBoldCSS.Checked = CSS.Default.ValueBold;
            valueItalicCSS.Checked = CSS.Default.ValueItalic;

            variableBack.BackColor = CSS.Default.Variable;
            variableBold.Checked = CSS.Default.VariableBold;
            variableItalic.Checked = CSS.Default.VariableItalic;
            #endregion

            #region JS
            //Load JS
            JSlist.SelectedIndex = 0;

            //Caret
            caretBackJS.BackColor = JS.Default.Caret;

            //Others
            defaultBackJS.BackColor = JS.Default.Defaulttext;
            defaultBoldJS.Checked = JS.Default.DefaulttextBold;
            defaultItalicJS.Checked = JS.Default.DefaulttextItalic;

            commentBackJS.BackColor = JS.Default.Comment;
            commentBoldJS.Checked = JS.Default.CommentBold;
            commentItalicJS.Checked = JS.Default.CommentItalic;

            commentLineBack.BackColor = JS.Default.CommentLine;
            commentLineBold.Checked = JS.Default.CommentLineBold;
            commentLineItalic.Checked = JS.Default.CommentLineItalic;

            commentDocBack.BackColor = JS.Default.CommentDoc;
            commentDocBold.Checked = JS.Default.CommentDocBold;
            commentDocItalic.Checked = JS.Default.CommentDocItalic;

            numberBackJS.BackColor = JS.Default.Number;
            numberBoldJS.Checked = JS.Default.NumberBold;
            numberItalicJS.Checked = JS.Default.NumberItalic;

            eolBackJS.BackColor = JS.Default.StringEol;
            eolBoldJS.Checked = JS.Default.StringEolBold;
            eolItalicJS.Checked = JS.Default.StringEolItalic;

            commentLineDocBack.BackColor = JS.Default.CommentLineDoc;
            commentLineDocBold.Checked = JS.Default.CommentLineDocBold;
            commentLineDocItalic.Checked = JS.Default.CommentLineDocItalic;

            commentDocKeywordBack.BackColor = JS.Default.CommentDocKeyword;
            commentDocKeywordBold.Checked = JS.Default.CommentDocKeywordBold;
            commentDocKeywordItalic.Checked = JS.Default.CommentDocKeywordItalic;

            commentDocKeywordErrorBack.BackColor = JS.Default.CommentDocKeywordError;
            commentDocKeywordErrorBold.Checked = JS.Default.CommentDocKeywordErrorBold;
            commentDocKeywordErrorItalic.Checked = JS.Default.CommentDocKeywordErrorItalic;

            wordBackJS.BackColor = JS.Default.Word;
            wordBoldJS.Checked = JS.Default.WordBold;
            wordItalicJS.Checked = JS.Default.WordItalic;

            word2BackJS.BackColor = JS.Default.Word2;
            word2BoldJS.Checked = JS.Default.Word2Bold;
            word2ItalicJS.Checked = JS.Default.Word2Italic;

            stringBackJS.BackColor = JS.Default.String;
            stringBoldJS.Checked = JS.Default.StringBold;
            stringItalicJS.Checked = JS.Default.StringItalic;

            characterBackJS.BackColor = JS.Default.Character;
            characterBoldJS.Checked = JS.Default.CharacterBold;
            characterItalicJS.Checked = JS.Default.CharacterItalic;

            UUIDBack.BackColor = JS.Default.UUID;
            UUIDBold.Checked = JS.Default.UUIDBold;
            UUIDItalic.Checked = JS.Default.UUIDItalic;

            operatorBackJS.BackColor = JS.Default.Operator;
            operatorBoldJS.Checked = JS.Default.OperatorBold;
            operatorItalicJS.Checked = JS.Default.Operatoritalic;

            identifierBackJS.BackColor = JS.Default.Identifier;
            identifierBoldJS.Checked = JS.Default.IdentifierBold;
            identifierItalicJS.Checked = JS.Default.IdentifierItalic;

            verbatimBack.BackColor = JS.Default.Verbatim;
            verbatimBold.Checked = JS.Default.VerbatimBold;
            verbatimItalic.Checked = JS.Default.VerbatimItalic;

            regexBack.BackColor = JS.Default.Regex;
            regexBold.Checked = JS.Default.RegexBold;
            regexItalic.Checked = JS.Default.RegexItalic;
            #endregion

            #region Blext
            //Load Blext
            Blextlist.SelectedIndex = 0;

            //Caret
            caretBackBlext.BackColor = Blext.Default.Caret;

            //Others
            defaultBackBlext.BackColor = Blext.Default.Defaulttext;
            defaultBoldBlext.Checked = Blext.Default.DefaulttextBold;
            defaultItalicBlext.Checked = Blext.Default.DefaulttextItalic;
            #endregion
        }

        void Panel_click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            Color c = Color.Empty;

            c = cdl_Color(p);
            if (c != Color.Empty)
            {
                p.BackColor = IntToColor(cdl.Color.ToArgb());
            }
            else { }
        }

        Color cdl_Color(Panel panel)
        {
            if (cdl.ShowDialog() == DialogResult.OK)
            {
                return IntToColor(cdl.Color.ToArgb());
            }
            return Color.Empty;
        }

        static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private TabPage Findtb(string option, TabControl tabControl)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                string tabName = tab.Text;
                if (string.Equals(option, tabName))
                    return (tab);
            }
            return (null);
        }

        private void listIndexChanged(ListBox lst, TabControl tbControl)
        {
            TabPage tab = Findtb(lst.SelectedItem.ToString(), tbControl);
            if (tab != null)
                tbControl.SelectedTab = tab;
            else
                tbControl.SelectedIndex = -1;
        }

        private void SavePython()
        {
            //Backcolors
            Py.Default.BackColorDark = pythonDarkback.BackColor;
            Py.Default.ForeColorDark = pythonDarkfore.BackColor;
            Py.Default.BackColorWhite = pythonLightback.BackColor;
            Py.Default.ForeColorWhite = pythonLightfore.BackColor;

            //Caret
            Py.Default.Caret = caretBack.BackColor;

            //Others
            Py.Default.Defaulttext = defaultBack.BackColor;
            Py.Default.DefaultBold = defaultBold.Checked;
            Py.Default.DefaultItalic = defaultItalic.Checked;

            Py.Default.Numbers = numberBack.BackColor;
            Py.Default.NumberBold = numberBold.Checked;
            Py.Default.NumberItalic = numberItalic.Checked;

            Py.Default.Strings = stringBack.BackColor;
            Py.Default.StringBold = stringBold.Checked;
            Py.Default.StringItalic = stringItalic.Checked;

            Py.Default.Characters = characterBack.BackColor;
            Py.Default.CharacterBold = characterBold.Checked;
            Py.Default.CharacterItalic = characterItalic.Checked;

            Py.Default.Keywords = wordBack.BackColor;
            Py.Default.KeywordsBold = wordBold.Checked;
            Py.Default.KeywordsItalic = wordItalic.Checked;

            Py.Default.Classnames = classBack.BackColor;
            Py.Default.ClassnamesBold = classBold.Checked;
            Py.Default.ClassnamesItalic = classItalic.Checked;

            Py.Default.Definitionnames = defBack.BackColor;
            Py.Default.DefnameBold = defBold.Checked;
            Py.Default.DefnameItalic = defItalic.Checked;

            Py.Default.Word2 = word2Back.BackColor;
            Py.Default.Word2Bold = word2Bold.Checked;
            Py.Default.Word2Italic = word2Italic.Checked;

            Py.Default.Decorators = decoratorBack.BackColor;
            Py.Default.DecoratorsBold = decoratorBold.Checked;
            Py.Default.DecoratorsItalic = decoratorItalic.Checked;

            Py.Default.LineComments = lineBack.BackColor;
            Py.Default.LineCommentsBold = lineBold.Checked;
            Py.Default.LineCommentsItalic = lineItalic.Checked;

            Py.Default.TripleComments = tripleBack.BackColor;
            Py.Default.TripleCommentsBold = tripleBold.Checked;
            Py.Default.TripleCommentsItalic = tripleItalic.Checked;

            Py.Default.TripleDouble = tripledoubleBack.BackColor;
            Py.Default.TripleDoubleBold = tripledoubleBold.Checked;
            Py.Default.TripleDoubleItalic = tripledoubleItalic.Checked;

            Py.Default.Block = blockBack.BackColor;
            Py.Default.BlockBold = blockBold.Checked;
            Py.Default.BlockItalic = blockItalic.Checked;

            Py.Default.EOL = eolBack.BackColor;
            Py.Default.EOLBold = eolBold.Checked;
            Py.Default.EOLItalic = eolItalic.Checked;

            //Save and Close
            Py.Default.Save();
            ConfigurationManager.RefreshSection("AppSettings");
        }

        private void SaveHTML()
        {
            //Caret
            HTML.Default.Caret = caretBackHTML.BackColor;

            //Tag
            HTML.Default.Tag = tagBack.BackColor;
            HTML.Default.TagBold = tagBold.Checked;
            HTML.Default.TagItalic = tagItalic.Checked;

            //TagUnknown
            HTML.Default.TagUnknown = tagUnknownBack.BackColor;
            HTML.Default.TagUnknownBold = tagUnknownBold.Checked;
            HTML.Default.TagUnknownItalic = tagUnknownItalic.Checked;

            //Attribute
            HTML.Default.Attribute = attributeBack.BackColor;
            HTML.Default.AttributeBold = attributeBold.Checked;
            HTML.Default.AttributeItalic = attributeItalic.Checked;

            //AttributeUnknown
            HTML.Default.AttributeUnknown = attributeUnknownBack.BackColor;
            HTML.Default.AttributeUnknownBold = attributeUnknownBold.Checked;
            HTML.Default.AttributeUnknownItalic = attributeUnknownItalic.Checked;

            //Number
            HTML.Default.Number = numberBackHTML.BackColor;
            HTML.Default.NumberBold = numberBold.Checked;
            HTML.Default.NumberItalic = numberItalic.Checked;

            //DoubleString
            HTML.Default.DoubleString = doubleStringBack.BackColor;
            HTML.Default.DoubleStringBold = doubleStringBold.Checked;
            HTML.Default.DoubleStringItalic = doubleStringItalic.Checked;

            //SingleString
            HTML.Default.SingleString = singleStringBack.BackColor;
            HTML.Default.SingleStringBold = singleStringBold.Checked;
            HTML.Default.SingleStringItalic = singleStringItalic.Checked;

            //Other
            HTML.Default.Other = otherBack.BackColor;
            HTML.Default.OtherBold = otherBold.Checked;
            HTML.Default.OtherItalic = otherItalic.Checked;

            //Comment
            HTML.Default.Comment = commentBack.BackColor;
            HTML.Default.CommentBold = commentBold.Checked;
            HTML.Default.CommentItalic = commentItalic.Checked;

            //Entity
            HTML.Default.Entity = entityBack.BackColor;
            HTML.Default.EntityBold = entityBold.Checked;
            HTML.Default.EntityItalic = entityItalic.Checked;

            //Script
            HTML.Default.Script = scriptBack.BackColor;
            HTML.Default.ScriptBold = scriptBold.Checked;
            HTML.Default.ScriptItalic = scriptItalic.Checked;

            //CData
            HTML.Default.CData = cdataBack.BackColor;
            HTML.Default.CDataBold = cdataBold.Checked;
            HTML.Default.CDataItalic = cdataItalic.Checked;

            //Question
            HTML.Default.Question = questionBack.BackColor;
            HTML.Default.QuestionBold = questionBold.Checked;
            HTML.Default.QuestionItalic = questionItalic.Checked;

            //Value
            HTML.Default.Value = valueBack.BackColor;
            HTML.Default.ValueBold = valueBold.Checked;
            HTML.Default.ValueItalic = valueItalic.Checked;

            //XcComment
            HTML.Default.XcComment = xccommentBack.BackColor;
            HTML.Default.XcCommentBold = xccommentBold.Checked;
            HTML.Default.XcCommentItalic = xccommentItalic.Checked;

            //TagEnd
            HTML.Default.TagEnd = tagEndBack.BackColor;
            HTML.Default.TagEndBold = tagEndBold.Checked;
            HTML.Default.TagEndItalic = tagEndItalic.Checked;

            //XmlStart
            HTML.Default.XmlStart = xmlStartBack.BackColor;
            HTML.Default.XmlStartBold = xmlStartBold.Checked;
            HTML.Default.XmlStartItalic = xmlStartItalic.Checked;

            //XmlEnd
            HTML.Default.XmlEnd = xmlEndBack.BackColor;
            HTML.Default.XmlEndBold = xmlEndBold.Checked;
            HTML.Default.XmlEndItalic = xmlEndItalic.Checked;

            //Save and Close
            HTML.Default.Save();
            ConfigurationManager.RefreshSection("AppSettings");
        }

        private void SaveCSS()
        {
            //Caret
            CSS.Default.Caret = caretBackCSS.BackColor;

            //Default
            CSS.Default.Defaulttext = defaultBackCSS.BackColor;
            CSS.Default.DefaultBold = defaultBoldCSS.Checked;
            CSS.Default.DefaultItalic = defaultItalicCSS.Checked;

            //Attribute
            CSS.Default.Attribute = attributeBackCSS.BackColor;
            CSS.Default.AttributeBold = attributeBoldCSS.Checked;
            CSS.Default.AttributeItalic = attributeItalicCSS.Checked;

            //Class
            CSS.Default.Class = classBackCSS.BackColor;
            CSS.Default.ClassBold = classBoldCSS.Checked;
            CSS.Default.ClassItalic = classItalicCSS.Checked;

            //Comment
            CSS.Default.Comment = commentBackCSS.BackColor;
            CSS.Default.CommentBold = commentBoldCSS.Checked;
            CSS.Default.CommentItalic = commentItalicCSS.Checked;

            //Directive
            CSS.Default.Directive = directiveBack.BackColor;
            CSS.Default.DirectiveBold = directiveBold.Checked;
            CSS.Default.DirectiveItalic = directiveItalic.Checked;

            //DoubleString
            CSS.Default.DoubleString = doublestringBackCSS.BackColor;
            CSS.Default.DoubleStringBold = doublestringBoldCSS.Checked;
            CSS.Default.DoubleStringItalic = doublestringItalicCSS.Checked;

            //ExtendedIdentifier
            CSS.Default.ExtendedIdentifier = extendedidentifierBack.BackColor;
            CSS.Default.ExtendedIdentifierBold = extendedidentifierBold.Checked;
            CSS.Default.ExtendedIdentifierItalic = extendedidentifierItalic.Checked;

            //ExtendedPseudoClass
            CSS.Default.ExtendedPseudoClass = extendedpseudoclassBack.BackColor;
            CSS.Default.ExtendedPseudoClassBold = extendedpseudoclassBold.Checked;
            CSS.Default.ExtendedPseudoClassItalic = extendedpseudoclassItalic.Checked;

            //ExtendedPseudoElement
            CSS.Default.ExtendedPseudoElement = extendedpseudocelementBack.BackColor;
            CSS.Default.ExtendedPseudoElementBold = extendedpseudocelementBold.Checked;
            CSS.Default.ExtendedPseudoElementItalic = extendedpseudocelementItalic.Checked;

            //Id
            CSS.Default.Id = idBack.BackColor;
            CSS.Default.IdBold = idBold.Checked;
            CSS.Default.IdItalic = idItalic.Checked;

            //Identifier
            CSS.Default.Identifier = identifierBack.BackColor;
            CSS.Default.IdentifierBold = identifierBold.Checked;
            CSS.Default.IdentifierItalic = identifierItalic.Checked;

            //Identifier2
            CSS.Default.Identifier2 = identifier2Back.BackColor;
            CSS.Default.Identifier2Bold = identifier2Bold.Checked;
            CSS.Default.Identifier2Italic = identifier2Italic.Checked;

            //Identifier3
            CSS.Default.Identifier3 = identifier3Back.BackColor;
            CSS.Default.Identifier3Bold = identifier3Bold.Checked;
            CSS.Default.Identifier3Italic = identifier3Italic.Checked;

            //Important
            CSS.Default.Important = importantBack.BackColor;
            CSS.Default.ImportantBold = importantBold.Checked;
            CSS.Default.ImportantItalic = importantItalic.Checked;

            //Media
            CSS.Default.Media = mediaBack.BackColor;
            CSS.Default.MediaBold = mediaBold.Checked;
            CSS.Default.MediaItalic = mediaItalic.Checked;

            //Operator
            CSS.Default.Operator = operatorBack.BackColor;
            CSS.Default.OperatorBold = operatorBold.Checked;
            CSS.Default.OperatorItalic = operatorItalic.Checked;

            //PseudoClass
            CSS.Default.PseudoClass = pseudoclassBack.BackColor;
            CSS.Default.PseudoClassBold = pseudoclassBold.Checked;
            CSS.Default.PseudoClassItalic = pseudoclassItalic.Checked;

            //PseudoElement
            CSS.Default.PseudoElement = pseudoelementBack.BackColor;
            CSS.Default.PseudoElementBold = pseudoelementBold.Checked;
            CSS.Default.PseudoElementItalic = pseudoelementItalic.Checked;

            //SingleString
            CSS.Default.SingleString = singlestringBackCSS.BackColor;
            CSS.Default.SingleStringBold = singleStringBold.Checked;
            CSS.Default.SingleStringItalic = singleStringItalic.Checked;

            //Tag
            CSS.Default.Tag = tagBackCSS.BackColor;
            CSS.Default.TagBold = tagBoldCSS.Checked;
            CSS.Default.TagItalic = tagItalicCSS.Checked;

            //UnknownIdentifier
            CSS.Default.UnknownIdentifier = unknownidentifierBack.BackColor;
            CSS.Default.UnknownIdentifierBold = unknownidentifierBold.Checked;
            CSS.Default.UnknownIdentifierItalic = unknownidentifierItalic.Checked;

            //UnknownPseudoClass
            CSS.Default.UnknownPseudoClass = unknownpseudoclassBack.BackColor;
            CSS.Default.UnknownPseudoClassBold = unknownpseudoclassBold.Checked;
            CSS.Default.UnknownPseudoClassItalic = unknownpseudoclassItalic.Checked;

            //Value
            CSS.Default.Value = valueBackCSS.BackColor;
            CSS.Default.ValueBold = valueBoldCSS.Checked;
            CSS.Default.ValueItalic = valueItalicCSS.Checked;

            //Variable
            CSS.Default.Variable = variableBack.BackColor;
            CSS.Default.VariableBold = variableBold.Checked;
            CSS.Default.VariableItalic = variableItalic.Checked;

            //Save and Close
            CSS.Default.Save();
            ConfigurationManager.RefreshSection("AppSettings");
        }

        private void SaveJS()
        {
            //Caret
            JS.Default.Caret = caretBackJS.BackColor;

            //Default
            JS.Default.Defaulttext = defaultBackJS.BackColor;
            JS.Default.DefaulttextBold = defaultBoldJS.Checked;
            JS.Default.DefaulttextItalic = defaultItalicJS.Checked;

            //Comment
            JS.Default.Comment = commentBackJS.BackColor;
            JS.Default.CommentBold = commentBoldJS.Checked;
            JS.Default.CommentItalic = commentItalicJS.Checked;

            //CommentLine
            JS.Default.CommentLine = commentLineBack.BackColor;
            JS.Default.CommentLineBold = commentLineBold.Checked;
            JS.Default.CommentLineItalic = commentLineItalic.Checked;

            //CommentDoc
            JS.Default.CommentDoc = commentDocBack.BackColor;
            JS.Default.CommentDocBold = commentDocBold.Checked;
            JS.Default.CommentDocItalic = commentDocItalic.Checked;

            //Number
            JS.Default.Number = numberBackJS.BackColor;
            JS.Default.NumberBold = numberBoldJS.Checked;
            JS.Default.NumberItalic = numberItalicJS.Checked;

            //StringEOL
            JS.Default.StringEol = eolBackJS.BackColor;
            JS.Default.StringEolBold = eolBoldJS.Checked;
            JS.Default.StringEolItalic = eolItalicJS.Checked;

            //CommentLineDoc
            JS.Default.CommentLineDoc = commentLineDocBack.BackColor;
            JS.Default.CommentLineDocBold = commentLineDocBold.Checked;
            JS.Default.CommentLineDocItalic = commentLineDocItalic.Checked;

            //CommentDocKeyword
            JS.Default.CommentDocKeyword = commentDocKeywordBack.BackColor;
            JS.Default.CommentDocKeywordBold = commentDocKeywordBold.Checked;
            JS.Default.CommentDocKeywordItalic = commentDocKeywordItalic.Checked;

            //CommentDocKeywordError
            JS.Default.CommentDocKeywordError = commentDocKeywordErrorBack.BackColor;
            JS.Default.CommentDocKeywordErrorBold = commentDocKeywordErrorBold.Checked;
            JS.Default.CommentDocKeywordErrorItalic = commentDocKeywordErrorItalic.Checked;

            //Word
            JS.Default.Word = wordBackJS.BackColor;
            JS.Default.WordBold = wordBoldJS.Checked;
            JS.Default.WordItalic = wordItalicJS.Checked;

            //Word2
            JS.Default.Word2 = word2BackJS.BackColor;
            JS.Default.Word2Bold = word2BoldJS.Checked;
            JS.Default.Word2Italic = word2ItalicJS.Checked;

            //String
            JS.Default.String = stringBackJS.BackColor;
            JS.Default.StringBold = stringBoldJS.Checked;
            JS.Default.StringItalic = stringItalicJS.Checked;

            //Character
            JS.Default.Character = characterBackJS.BackColor;
            JS.Default.CharacterBold = characterBoldJS.Checked;
            JS.Default.CharacterItalic = characterItalicJS.Checked;

            //UUID
            JS.Default.UUID = UUIDBack.BackColor;
            JS.Default.UUIDBold = UUIDBold.Checked;
            JS.Default.UUIDItalic = UUIDItalic.Checked;

            //Operator
            JS.Default.Operator = operatorBackJS.BackColor;
            JS.Default.OperatorBold = operatorBoldJS.Checked;
            JS.Default.Operatoritalic = operatorItalicJS.Checked;

            //Identifier
            JS.Default.Identifier = identifierBackJS.BackColor;
            JS.Default.IdentifierBold = identifierBoldJS.Checked;
            JS.Default.IdentifierItalic = identifierItalicJS.Checked;

            //Verbatim
            JS.Default.Verbatim = verbatimBack.BackColor;
            JS.Default.VerbatimBold = verbatimBold.Checked;
            JS.Default.VerbatimItalic = verbatimItalic.Checked;

            //Regex
            JS.Default.Regex = regexBack.BackColor;
            JS.Default.RegexBold = regexBold.Checked;
            JS.Default.RegexItalic = regexItalic.Checked;

            //Save and Close
            JS.Default.Save();
            ConfigurationManager.RefreshSection("AppSettings");
        }

        private void SaveBlext()
        {
            //Caret
            Blext.Default.Caret = caretBackBlext.BackColor;

            //Default
            Blext.Default.Defaulttext = defaultBackBlext.BackColor;
            Blext.Default.DefaulttextBold = defaultBoldBlext.Checked;
            Blext.Default.DefaulttextItalic = defaultItalicBlext.Checked;

            //Save and Close
            Blext.Default.Save();
            ConfigurationManager.RefreshSection("AppSettings");
        }

        private void SaveSettings()
        {
            try
            {
                //Save settings
                if (comboBox7.SelectedIndex == 0) { Settings.Default.DarkTheme = false; }
                else if (comboBox7.SelectedIndex == 1) { Settings.Default.DarkTheme = true; }
                Settings.Default.BraceLightBack = braceLightBack.BackColor;
                Settings.Default.BraceLightFore = braceLightFore.BackColor;
                Settings.Default.BraceDarkBack = braceDarkBack.BackColor;
                Settings.Default.BraceDarkFore = braceDarkFore.BackColor;

                SavePython();
                SaveHTML();
                SaveCSS();
                SaveJS();
                SaveBlext();

                //Save and Close
                Settings.Default.Save();
                ConfigurationManager.RefreshSection("AppSettings");

                //Reload open instances of scintilla
                var tb = Application.OpenForms[0].Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
                Forms.Reload.ReloadOnDemand.ReloadSciOnDemand(tb);
            }
            catch (ApplicationException ex)
            {
                Classes.Exception.ShowError.Show("Unable to save settings. Here are the details:", ex.Message);
            }
        }

        private void pythonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listIndexChanged(pythonList, tabControl1);
        }

        private void HTMLlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            listIndexChanged(HTMLlist, tabControl2);
        }

        private void CSSlist_SelectedindexChanged(object sender, EventArgs e)
        {
            listIndexChanged(CSSlist, tabControl4);
        }

        private void JSlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            listIndexChanged(JSlist, tabControl5);
        }

        private void Blextlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            listIndexChanged(Blextlist, tabControl6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Disable button
            button2.Enabled = false;

            //Save settings
            SaveSettings();

            //Enable button
            Timer t = new Timer();
            t.Interval = 3000;
            t.Tick += new EventHandler((obj, ev) =>
            {
                button2.Enabled = true;

                t.Stop();
                t.Enabled = false;
                t.Dispose();
            });
            t.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();

            //Close
            this.Hide();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Backcolors
            pythonDarkback.BackColor = Color.FromArgb(52, 61, 70);
            pythonDarkfore.BackColor = Color.FromArgb(183, 183, 183);
            pythonLightback.BackColor = Color.FromArgb(249, 250, 250);
            pythonLightfore.BackColor = Color.Black;

            braceLightBack.BackColor = Color.FromArgb(226, 230, 214);
            braceLightFore.BackColor = Color.Black;
            braceDarkBack.BackColor = Color.FromArgb(92, 101, 110);
            braceDarkFore.BackColor = Color.FromArgb(184, 184, 184);

            if (comboBox7.SelectedIndex == 0)
            {
                #region Python
                //Caret
                caretBack.BackColor = Color.Black;

                //Others
                defaultBack.BackColor = Color.FromArgb(0x80, 0x80, 0x80);
                defaultBold.Checked = false;
                defaultItalic.Checked = false;

                numberBack.BackColor = Color.FromArgb(0x00, 0x7F, 0x7F);
                numberBold.Checked = false;
                numberItalic.Checked = false;

                stringBack.BackColor = Color.FromArgb(163, 21, 21);
                stringBold.Checked = false;
                stringItalic.Checked = false;

                characterBack.BackColor = Color.FromArgb(163, 21, 21);
                characterBold.Checked = false;
                characterItalic.Checked = false;

                wordBack.BackColor = Color.FromArgb(10, 140, 220);
                wordBold.Checked = false;
                wordItalic.Checked = false;

                classBack.BackColor = Color.FromArgb(0x00, 0x00, 0xFF);
                classBold.Checked = false;
                classItalic.Checked = false;

                defBack.BackColor = Color.FromArgb(0x00, 0x7F, 0x7F);
                defBold.Checked = false;
                defItalic.Checked = false;

                word2Back.BackColor = Color.FromArgb(0x40, 0x70, 0x90);
                word2Bold.Checked = false;
                word2Italic.Checked = false;

                decoratorBack.BackColor = Color.FromArgb(255, 0, 128);
                decoratorBold.Checked = false;
                decoratorItalic.Checked = false;

                lineBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                lineBold.Checked = false;
                lineItalic.Checked = false;

                tripleBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                tripleBold.Checked = false;
                tripleItalic.Checked = false;

                tripledoubleBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                tripledoubleBold.Checked = false;
                tripledoubleItalic.Checked = false;

                blockBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                blockBold.Checked = false;
                blockItalic.Checked = false;

                eolBack.BackColor = Color.FromArgb(0x00, 0x00, 0x00);
                eolBold.Checked = false;
                eolItalic.Checked = false;
                #endregion

                #region HTML
                //Caret
                caretBackHTML.BackColor = Color.Black;

                //Tag
                tagBack.BackColor = Color.Purple;
                tagBold.Checked = false;
                tagItalic.Checked = false;

                //TagUnknown
                tagUnknownBack.BackColor = Color.FromArgb(192, 0, 0);
                tagUnknownBold.Checked = false;
                tagUnknownItalic.Checked = false;

                //Attribute
                attributeBack.BackColor = Color.Olive;
                attributeBold.Checked = false;
                attributeItalic.Checked = false;

                //AttributeUnknown
                attributeUnknownBack.BackColor = Color.FromArgb(192, 0, 0);
                attributeUnknownBold.Checked = false;
                attributeUnknownItalic.Checked = false;

                //Number
                numberBackHTML.BackColor = Color.Teal;
                numberBold.Checked = false;
                numberItalic.Checked = false;

                //DoubleString
                doubleStringBack.BackColor = Color.FromArgb(192, 0, 0);
                doubleStringBold.Checked = false;
                doubleStringItalic.Checked = false;

                //SingleString
                singleStringBack.BackColor = Color.Red;
                singleStringBold.Checked = false;
                singleStringItalic.Checked = false;

                //Other
                otherBack.BackColor = Color.FromArgb(140, 140, 140);
                otherBold.Checked = false;
                otherItalic.Checked = false;

                //Comment
                commentBack.BackColor = Color.Green;
                commentBold.Checked = false;
                commentItalic.Checked = false;

                //Entity
                entityBack.BackColor = Color.Navy;
                entityBold.Checked = false;
                entityItalic.Checked = false;

                //Script
                scriptBack.BackColor = Color.FromArgb(192, 64, 0);
                scriptBold.Checked = false;
                scriptItalic.Checked = false;

                //CData
                cdataBack.BackColor = Color.RosyBrown;
                cdataBold.Checked = false;
                cdataItalic.Checked = false;

                //Question
                questionBack.BackColor = Color.FromArgb(0, 192, 0);
                questionBold.Checked = false;
                questionItalic.Checked = false;

                //Value
                valueBack.BackColor = Color.Purple;
                valueBold.Checked = false;
                valueItalic.Checked = false;

                //XcComment
                xccommentBack.BackColor = Color.Green;
                xccommentBold.Checked = false;
                xccommentItalic.Checked = false;

                //TagEnd
                tagEndBack.BackColor = Color.Maroon;
                tagEndBold.Checked = false;
                tagEndItalic.Checked = false;

                //XmlStart
                xmlStartBack.BackColor = Color.Blue;
                xmlStartBold.Checked = false;
                xmlStartItalic.Checked = false;

                //XmlEnd
                xmlEndBack.BackColor = Color.Blue;
                xmlEndBold.Checked = false;
                xmlEndItalic.Checked = false;
                #endregion

                #region CSS
                //Caret
                caretBackCSS.BackColor = Color.Black;

                //Others
                defaultBackCSS.BackColor = Color.FromArgb(0x80, 0x80, 0x80);
                defaultBoldCSS.Checked = false;
                defaultItalicCSS.Checked = false;

                attributeBackCSS.BackColor = Color.FromArgb(34, 134, 58);
                attributeBoldCSS.Checked = false;
                attributeItalicCSS.Checked = false;

                classBackCSS.BackColor = Color.FromArgb(0, 92, 197);
                classBoldCSS.Checked = false;
                classItalicCSS.Checked = false;

                commentBackCSS.BackColor = Color.FromArgb(127, 127, 127);
                commentBoldCSS.Checked = false;
                commentItalicCSS.Checked = false;

                directiveBack.BackColor = Color.FromArgb(0, 92, 197);
                directiveBold.Checked = false;
                directiveItalic.Checked = false;

                doublestringBackCSS.BackColor = Color.FromArgb(34, 134, 58);
                doublestringBoldCSS.Checked = false;
                doublestringItalicCSS.Checked = false;

                extendedidentifierBack.BackColor = Color.FromArgb(101, 201, 8);
                extendedidentifierBold.Checked = false;
                extendedidentifierItalic.Checked = false;

                extendedpseudoclassBack.BackColor = Color.FromArgb(0, 92, 197);
                extendedpseudoclassBold.Checked = false;
                extendedpseudoclassItalic.Checked = false;

                extendedpseudocelementBack.BackColor = Color.FromArgb(0, 92, 197);
                extendedpseudocelementBold.Checked = false;
                extendedpseudocelementItalic.Checked = false;

                idBack.BackColor = Color.FromArgb(101, 201, 8);
                idBold.Checked = false;
                idItalic.Checked = false;

                identifierBack.BackColor = Color.FromArgb(101, 201, 8);
                identifierBold.Checked = false;
                identifierItalic.Checked = true;

                identifier2Back.BackColor = Color.FromArgb(250, 147, 37);
                identifier2Bold.Checked = false;
                identifier2Italic.Checked = false;

                identifier3Back.BackColor = Color.FromArgb(0, 92, 197);
                identifier3Bold.Checked = false;
                identifier3Italic.Checked = false;

                importantBack.BackColor = Color.FromArgb(215, 58, 73);
                importantBold.Checked = false;
                importantItalic.Checked = false;

                mediaBack.BackColor = Color.FromArgb(240, 67, 175);
                mediaBold.Checked = false;
                mediaItalic.Checked = false;

                operatorBack.BackColor = Color.FromArgb(48, 48, 48);
                operatorBold.Checked = false;
                operatorItalic.Checked = false;

                pseudoclassBack.BackColor = Color.FromArgb(0, 92, 197);
                pseudoclassBold.Checked = false;
                pseudoclassItalic.Checked = false;

                pseudoelementBack.BackColor = Color.FromArgb(0, 92, 197);
                pseudoelementBold.Checked = false;
                pseudoelementItalic.Checked = false;

                singlestringBackCSS.BackColor = Color.FromArgb(34, 134, 58);
                singleStringBold.Checked = false;
                singleStringItalic.Checked = false;

                tagBackCSS.BackColor = Color.FromArgb(34, 134, 58);
                tagBoldCSS.Checked = false;
                tagItalicCSS.Checked = false;

                unknownidentifierBack.BackColor = Color.FromArgb(127, 127, 127);
                unknownidentifierBold.Checked = false;
                unknownidentifierItalic.Checked = false;

                unknownpseudoclassBack.BackColor = Color.FromArgb(127, 127, 127);
                unknownpseudoclassBold.Checked = false;
                unknownpseudoclassItalic.Checked = false;

                valueBackCSS.BackColor = Color.FromArgb(250, 147, 37);
                valueBoldCSS.Checked = false;
                valueItalicCSS.Checked = false;

                variableBack.BackColor = Color.FromArgb(215, 58, 73);
                variableBold.Checked = false;
                variableItalic.Checked = false;
                #endregion

                #region JS
                //Caret
                caretBackJS.BackColor = Color.Black;

                //Others
                defaultBackJS.BackColor = Color.FromArgb(0x80, 0x80, 0x80);
                defaultBoldJS.Checked = false;
                defaultItalicJS.Checked = false;

                commentBackJS.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentBoldJS.Checked = false;
                commentItalicJS.Checked = false;

                commentLineBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentLineBold.Checked = false;
                commentLineItalic.Checked = false;

                commentDocBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentDocBold.Checked = false;
                commentDocItalic.Checked = false;

                numberBackJS.BackColor = Color.FromArgb(0x00, 0x7F, 0x7F);
                numberBoldJS.Checked = false;
                numberItalicJS.Checked = false;

                eolBackJS.BackColor = Color.FromArgb(0x00, 0x00, 0x00);
                eolBoldJS.Checked = false;
                eolItalicJS.Checked = false;

                commentLineDocBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentLineDocBold.Checked = false;
                commentLineDocItalic.Checked = false;

                commentDocKeywordBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentDocKeywordBold.Checked = false;
                commentDocKeywordItalic.Checked = false;

                commentDocKeywordErrorBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentDocKeywordErrorBold.Checked = false;
                commentDocKeywordErrorItalic.Checked = false;

                wordBackJS.BackColor = Color.FromArgb(215, 58, 73);
                wordBoldJS.Checked = false;
                wordItalicJS.Checked = false;

                word2BackJS.BackColor = Color.FromArgb(250, 147, 37);
                word2BoldJS.Checked = false;
                word2ItalicJS.Checked = false;

                stringBackJS.BackColor = Color.FromArgb(163, 21, 21);
                stringBoldJS.Checked = false;
                stringItalicJS.Checked = false;

                characterBackJS.BackColor = Color.FromArgb(163, 21, 21);
                characterBoldJS.Checked = false;
                characterItalicJS.Checked = false;

                UUIDBack.BackColor = Color.FromArgb(34, 134, 58);
                UUIDBold.Checked = false;
                UUIDItalic.Checked = false;

                operatorBackJS.BackColor = Color.FromArgb(48, 48, 48);
                operatorBoldJS.Checked = false;
                operatorItalicJS.Checked = false;

                identifierBackJS.BackColor = Color.FromArgb(111, 66, 193);
                identifierBoldJS.Checked = false;
                identifierItalicJS.Checked = false;

                verbatimBack.BackColor = Color.FromArgb(34, 134, 58);
                verbatimBold.Checked = false;
                verbatimItalic.Checked = false;

                regexBack.BackColor = Color.FromArgb(34, 134, 58);
                regexBold.Checked = false;
                regexItalic.Checked = false;
                #endregion

                #region Blext
                //Caret
                caretBackBlext.BackColor = Color.Black;

                //Others
                defaultBackBlext.BackColor = Color.FromArgb(0x80, 0x80, 0x80);
                defaultBoldBlext.Checked = false;
                defaultItalicBlext.Checked = false;
                #endregion
            }
            else if (comboBox7.SelectedIndex == 1)
            {
                #region Python
                //Caret
                caretBack.BackColor = Color.Orange;

                //Others
                defaultBack.BackColor = Color.Gray;
                defaultBold.Checked = false;
                defaultItalic.Checked = false;

                numberBack.BackColor = Color.Orange;
                numberBold.Checked = false;
                numberItalic.Checked = false;

                stringBack.BackColor = Color.FromArgb(144, 177, 118);
                stringBold.Checked = false;
                stringItalic.Checked = false;

                characterBack.BackColor = Color.FromArgb(144, 177, 118);
                characterBold.Checked = false;
                characterItalic.Checked = false;

                wordBack.BackColor = Color.FromArgb(180, 144, 196);
                wordBold.Checked = false;
                wordItalic.Checked = false;

                classBack.BackColor = Color.Orange;
                classBold.Checked = false;
                classItalic.Checked = false;

                defBack.BackColor = Color.FromArgb(90, 178, 164);
                defBold.Checked = false;
                defItalic.Checked = false;

                word2Back.BackColor = Color.FromArgb(96, 153, 200);
                word2Bold.Checked = false;
                word2Italic.Checked = false;

                decoratorBack.BackColor = Color.FromArgb(255, 0, 128);
                decoratorBold.Checked = false;
                decoratorItalic.Checked = false;

                lineBack.BackColor = Color.FromArgb(127, 127, 127);
                lineBold.Checked = false;
                lineItalic.Checked = false;

                tripleBack.BackColor = Color.FromArgb(127, 127, 127);
                tripleBold.Checked = false;
                tripleItalic.Checked = false;

                tripledoubleBack.BackColor = Color.FromArgb(127, 127, 127);
                tripledoubleBold.Checked = false;
                tripledoubleItalic.Checked = false;

                blockBack.BackColor = Color.FromArgb(127, 127, 127);
                blockBold.Checked = false;
                blockItalic.Checked = false;

                eolBack.BackColor = Color.FromArgb(127, 127, 127);
                eolBold.Checked = false;
                eolItalic.Checked = false;
                #endregion

                #region HTML
                //Caret
                caretBackHTML.BackColor = Color.Orange;
                
                //Tag
                tagBack.BackColor = Color.FromArgb(197, 92, 92);
                tagBold.Checked = false;
                tagItalic.Checked = false;

                //TagUnknown
                tagUnknownBack.BackColor = Color.FromArgb(247, 203, 27);
                tagUnknownBold.Checked = false;
                tagUnknownItalic.Checked = false;

                //Attribute
                attributeBack.BackColor = Color.FromArgb(180, 144, 196);
                attributeBold.Checked = false;
                attributeItalic.Checked = false;

                //AttributeUnknown
                attributeUnknownBack.BackColor = Color.FromArgb(247, 203, 27);
                attributeUnknownBold.Checked = false;
                attributeUnknownItalic.Checked = false;

                //Number
                numberBackHTML.BackColor = Color.Teal;
                numberBold.Checked = false;
                numberItalic.Checked = false;

                //DoubleString
                doubleStringBack.BackColor = Color.FromArgb(123, 187, 134);
                doubleStringBold.Checked = false;
                doubleStringItalic.Checked = false;

                //SingleString
                singleStringBack.BackColor = Color.FromArgb(123, 187, 134);
                singleStringBold.Checked = false;
                singleStringItalic.Checked = false;

                //Other
                otherBack.BackColor = Color.FromArgb(90, 179, 176);
                otherBold.Checked = false;
                otherItalic.Checked = false;

                //Comment
                commentBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                commentBold.Checked = false;
                commentItalic.Checked = false;

                //Entity
                entityBack.BackColor = Color.FromArgb(180, 144, 196);
                entityBold.Checked = false;
                entityItalic.Checked = false;

                //Script
                scriptBack.BackColor = Color.FromArgb(197, 92, 92);
                scriptBold.Checked = false;
                scriptItalic.Checked = false;

                //CData
                cdataBack.BackColor = Color.FromArgb(180, 144, 196);
                cdataBold.Checked = false;
                cdataItalic.Checked = false;

                //Question
                questionBack.BackColor = Color.FromArgb(123, 187, 134);
                questionBold.Checked = false;
                questionItalic.Checked = false;

                //Value
                valueBack.BackColor = Color.FromArgb(197, 92, 92);
                valueBold.Checked = false;
                valueItalic.Checked = false;

                //XcComment
                xccommentBack.BackColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                xccommentBold.Checked = false;
                xccommentItalic.Checked = false;

                //TagEnd
                tagEndBack.BackColor = Color.FromArgb(90, 179, 176);
                tagEndBold.Checked = false;
                tagEndItalic.Checked = false;

                //XmlStart
                xmlStartBack.BackColor = Color.FromArgb(197, 92, 92);
                xmlStartBold.Checked = false;
                xmlStartItalic.Checked = false;

                //XmlEnd
                xmlEndBack.BackColor = Color.FromArgb(197, 92, 92);
                xmlEndBold.Checked = false;
                xmlEndItalic.Checked = false;
                #endregion

                #region CSS
                //Caret
                caretBackCSS.BackColor = Color.Orange;

                //Others
                defaultBackCSS.BackColor = Color.FromArgb(0x80, 0x80, 0x80);
                defaultBoldCSS.Checked = false;
                defaultItalicCSS.Checked = false;

                attributeBackCSS.BackColor = Color.FromArgb(162, 201, 150);
                attributeBoldCSS.Checked = false;
                attributeItalicCSS.Checked = false;

                classBackCSS.BackColor = Color.FromArgb(204, 161, 195);
                classBoldCSS.Checked = false;
                classItalicCSS.Checked = false;

                commentBackCSS.BackColor = Color.FromArgb(127, 127, 127);
                commentBoldCSS.Checked = false;
                commentItalicCSS.Checked = false;

                directiveBack.BackColor = Color.FromArgb(129, 160, 227);
                directiveBold.Checked = false;
                directiveItalic.Checked = false;

                doublestringBackCSS.BackColor = Color.FromArgb(162, 201, 150);
                doublestringBoldCSS.Checked = false;
                doublestringItalicCSS.Checked = false;

                extendedidentifierBack.BackColor = Color.FromArgb(204, 161, 195);
                extendedidentifierBold.Checked = false;
                extendedidentifierItalic.Checked = false;

                extendedpseudoclassBack.BackColor = Color.FromArgb(204, 161, 195);
                extendedpseudoclassBold.Checked = false;
                extendedpseudoclassItalic.Checked = false;

                extendedpseudocelementBack.BackColor = Color.FromArgb(204, 161, 195);
                extendedpseudocelementBold.Checked = false;
                extendedpseudocelementItalic.Checked = false;

                idBack.BackColor = Color.FromArgb(204, 161, 195);
                idBold.Checked = false;
                idItalic.Checked = false;

                identifierBack.BackColor = Color.FromArgb(121, 175, 219);
                identifierBold.Checked = false;
                identifierItalic.Checked = true;

                identifier2Back.BackColor = Color.FromArgb(249, 176, 99);
                identifier2Bold.Checked = false;
                identifier2Italic.Checked = false;

                identifier3Back.BackColor = Color.FromArgb(204, 161, 195);
                identifier3Bold.Checked = false;
                identifier3Italic.Checked = false;

                importantBack.BackColor = Color.FromArgb(236, 98, 104);
                importantBold.Checked = false;
                importantItalic.Checked = false;

                mediaBack.BackColor = Color.FromArgb(240, 120, 195);
                mediaBold.Checked = false;
                mediaItalic.Checked = false;

                operatorBack.BackColor = Color.FromArgb(129, 160, 227);
                operatorBold.Checked = false;
                operatorItalic.Checked = false;

                pseudoclassBack.BackColor = Color.FromArgb(129, 160, 227);
                pseudoclassBold.Checked = false;
                pseudoclassItalic.Checked = false;

                pseudoelementBack.BackColor = Color.FromArgb(204, 161, 195);
                pseudoelementBold.Checked = false;
                pseudoelementItalic.Checked = false;

                singlestringBackCSS.BackColor = Color.FromArgb(162, 201, 150);
                singleStringBold.Checked = false;
                singleStringItalic.Checked = false;

                tagBackCSS.BackColor = Color.FromArgb(204, 161, 195);
                tagBoldCSS.Checked = false;
                tagItalicCSS.Checked = false;

                unknownidentifierBack.BackColor = Color.FromArgb(236, 98, 104);
                unknownidentifierBold.Checked = false;
                unknownidentifierItalic.Checked = false;

                unknownpseudoclassBack.BackColor = Color.FromArgb(236, 98, 104);
                unknownpseudoclassBold.Checked = false;
                unknownpseudoclassItalic.Checked = false;

                valueBackCSS.BackColor = Color.FromArgb(249, 176, 99);
                valueBoldCSS.Checked = false;
                valueItalicCSS.Checked = false;

                variableBack.BackColor = Color.FromArgb(237, 62, 62);
                variableBold.Checked = false;
                variableItalic.Checked = false;
                #endregion

                #region JS
                //Caret
                caretBackJS.BackColor = Color.Orange;

                //Others
                defaultBackJS.BackColor = Color.Gray;
                defaultBoldJS.Checked = false;
                defaultItalicJS.Checked = false;

                commentBackJS.BackColor = Color.FromArgb(127, 127, 127);
                commentBoldJS.Checked = false;
                commentItalicJS.Checked = false;

                commentLineBack.BackColor = Color.FromArgb(127, 127, 127);
                commentLineBold.Checked = false;
                commentLineItalic.Checked = false;

                commentDocBack.BackColor = Color.FromArgb(127, 127, 127);
                commentDocBold.Checked = false;
                commentDocItalic.Checked = false;

                numberBackJS.BackColor = Color.Orange;
                numberBoldJS.Checked = false;
                numberItalicJS.Checked = false;

                eolBackJS.BackColor = Color.FromArgb(127, 127, 127);
                eolBoldJS.Checked = false;
                eolItalicJS.Checked = false;

                commentLineDocBack.BackColor = Color.FromArgb(127, 127, 127);
                commentLineDocBold.Checked = false;
                commentLineDocItalic.Checked = false;

                commentDocKeywordBack.BackColor = Color.FromArgb(127, 127, 127);
                commentDocKeywordBold.Checked = false;
                commentDocKeywordItalic.Checked = false;

                commentDocKeywordErrorBack.BackColor = Color.FromArgb(127, 127, 127);
                commentDocKeywordErrorBold.Checked = false;
                commentDocKeywordErrorItalic.Checked = false;

                wordBackJS.BackColor = Color.FromArgb(180, 144, 196);
                wordBoldJS.Checked = false;
                wordItalicJS.Checked = false;

                word2BackJS.BackColor = Color.FromArgb(96, 153, 200);
                word2BoldJS.Checked = false;
                word2ItalicJS.Checked = false;

                stringBackJS.BackColor = Color.FromArgb(144, 177, 118);
                stringBoldJS.Checked = false;
                stringItalicJS.Checked = false;

                characterBackJS.BackColor = Color.FromArgb(144, 177, 118);
                characterBoldJS.Checked = false;
                characterItalicJS.Checked = false;

                UUIDBack.BackColor = Color.FromArgb(162, 201, 150);
                UUIDBold.Checked = false;
                UUIDItalic.Checked = false;

                operatorBackJS.BackColor = Color.FromArgb(129, 160, 227);
                operatorBoldJS.Checked = false;
                operatorItalicJS.Checked = false;

                identifierBackJS.BackColor = Color.FromArgb(121, 175, 219);
                identifierBoldJS.Checked = false;
                identifierItalicJS.Checked = false;

                verbatimBack.BackColor = Color.FromArgb(162, 201, 150);
                verbatimBold.Checked = false;
                verbatimItalic.Checked = false;

                regexBack.BackColor = Color.FromArgb(162, 201, 150);
                regexBold.Checked = false;
                regexItalic.Checked = false;
                #endregion

                #region Blext
                //Caret
                caretBackBlext.BackColor = Color.Orange;

                //Others
                defaultBackBlext.BackColor = Color.Gray;
                defaultBoldBlext.Checked = false;
                defaultItalicBlext.Checked = false;
                #endregion
            }
        }
    }
}
