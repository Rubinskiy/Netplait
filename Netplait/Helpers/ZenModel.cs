using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Netplait.Helpers
{
    public class ZenModel
    {
        private static List<string> IndexClosingTags(string path)
        {
            int counter = 0;
            string line;
            List<string> list = new List<string>();
            System.IO.StreamReader keys = new System.IO.StreamReader(path);
            while ((line = keys.ReadLine()) != null)
            {
                list.Add(line);
                counter++;
            }
            keys.Close();
            list.Sort();
            return list.OrderBy(m => m).ToList();
        }

        public static void InsertClosingTag(ScintillaNET.Scintilla TextArea, CharAddedEventArgs e)
        {
            try
            {
                var currentPos = TextArea.CurrentPosition;
                int caretPos = TextArea.SelectionStart;

                string curLine = TextArea.Lines[TextArea.CurrentLine].Text;
                string whole = curLine.Substring(curLine.LastIndexOf("<"));

                string Tags = Application.StartupPath + "\\Syntax\\HTML\\closing_tags.dat";
                List<string> ClosingList = IndexClosingTags(Tags);

                foreach (string selectedKeyword in ClosingList)
                {
                    var UpperFirst = e.Char == '>' && whole.Contains("<" + selectedKeyword.ToUpper() + " ");
                    var LowerFirst = e.Char == '>' && whole.Contains("<" + selectedKeyword.ToLower() + " ");

                    var UpperSecond = e.Char == '>' && whole.Contains("<" + selectedKeyword.ToUpper() + ">");
                    var LowerSecond = e.Char == '>' && whole.Contains("<" + selectedKeyword.ToLower() + ">");

                    if (UpperFirst || UpperSecond)
                    {
                        TextArea.InsertText(caretPos, "</" + selectedKeyword.ToUpper() + ">");
                    }
                    else if (LowerFirst || LowerSecond)
                    {
                        TextArea.InsertText(caretPos, "</" + selectedKeyword.ToLower() + ">");
                    }
                }                
            }
            catch (Exception ex) { }
        }

        public static void InitSnippets(ScintillaNET.Scintilla TextArea, CharAddedEventArgs e)
        {
            int caretPos = TextArea.SelectionStart;
            string curLine = TextArea.Lines[TextArea.CurrentLine].Text;
            string word = TextArea.GetWordFromPosition(caretPos);

            foreach(var line in TextArea.Text)
            {
                if (TextArea.Text.Contains("from flask import Flask"))
                {
                    TextArea.CallTipShow(caretPos, "You are now using the Flask framework");
                }
            }            
        }

        /// <summary>
        /// This method will decide whether to auto-insert a bracket/quote if one is entered
        /// </summary>
        public static void InsertMatchedChars(CharAddedEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            var caretPos = TextArea.CurrentPosition;
            var docStart = caretPos == 1;
            var docEnd = caretPos == TextArea.Text.Length;

            var charPrev = docStart ? TextArea.GetCharAt(caretPos) : TextArea.GetCharAt(caretPos - 2);
            var charNext = TextArea.GetCharAt(caretPos);

            var isCharPrevBlank = charPrev == ' ' || charPrev == '\t' ||
                                  charPrev == '\n' || charPrev == '\r';

            var isCharNextBlank = charNext == ' ' || charNext == '\t' ||
                                  charNext == '\n' || charNext == '\r' ||
                                  docEnd;

            var isEnclosed = (charPrev == '(' && charNext == ')') ||
                                  (charPrev == '{' && charNext == '}') ||
                                  (charPrev == '[' && charNext == ']');

            var isSpaceEnclosed = (charPrev == '(' && isCharNextBlank) || (isCharPrevBlank && charNext == ')') ||
                                  (charPrev == '{' && isCharNextBlank) || (isCharPrevBlank && charNext == '}') ||
                                  (charPrev == '[' && isCharNextBlank) || (isCharPrevBlank && charNext == ']');

            var isCharOrString = (isCharPrevBlank && isCharNextBlank) || isEnclosed || isSpaceEnclosed;

            var charNextIsCharOrString = charNext == '"' || charNext == '\'';

            switch (e.Char)
            {
                case '(':
                    if (Properties.Settings.Default.ShowBrackets == true)
                    {
                        if (charNextIsCharOrString) return;
                        TextArea.InsertText(caretPos, ")");
                    }
                    else { }
                    break;
                case '{':
                    if (Properties.Settings.Default.ShowCurlyBrackets == true)
                    {
                        if (charNextIsCharOrString) return;
                        TextArea.InsertText(caretPos, "}");
                    }
                    else { }
                    break;
                case '[':
                    if (Properties.Settings.Default.ShowSquareBrackets == true)
                    {
                        if (charNextIsCharOrString) return;
                        TextArea.InsertText(caretPos, "]");
                    }
                    else { }
                    break;
                case '<':
                    if (Properties.Settings.Default.ShowAngleBrackets == true)
                    {
                        if (charNextIsCharOrString) return;
                        TextArea.InsertText(caretPos, ">");
                    }
                    else { }
                    break;
                case '"':
                    if (Properties.Settings.Default.ShowQuotes == true)
                    {
                        // 0x22 = "                        
                        if (charPrev == 0x22 && charNext == 0x22)
                        {
                            TextArea.DeleteRange(caretPos, 1);
                            TextArea.GotoPosition(caretPos);
                            return;
                        }

                        if (TextArea.GetCharAt(caretPos - 2) == '=')
                            TextArea.InsertText(caretPos, "\"");

                        if (isCharOrString)
                            TextArea.InsertText(caretPos, "\"");
                    }
                    else { }
                    break;
                case '\'':
                    if (Properties.Settings.Default.ShowSingleQuote == true)
                    {
                        // 0x27 = '
                        if (charPrev == 0x27 && charNext == 0x27)
                        {
                            TextArea.DeleteRange(caretPos, 1);
                            TextArea.GotoPosition(caretPos);
                            return;
                        }

                        //if (isCharOrString)
                        //    TextArea.InsertText(caretPos, "'");
                        TextArea.InsertText(caretPos, "'");
                    }
                    else { }
                    break;
            }
        }

        public static void GetDefinition(ScintillaNET.Scintilla TextArea)
        {
            var DefList = Application.OpenForms[0].Controls.Find("funcTree", true).FirstOrDefault() as TreeView;
            var lists = DefList.Nodes.OfType<string>().ToList();

            int result = 0;
            TextArea.TargetStart = 0;
            TextArea.TargetEnd = TextArea.TextLength;
            result = TextArea.SearchInTarget("def ");
            Line line = TextArea.Lines[TextArea.CurrentLine];

            foreach (var item in lists)
            {
                if (!TextArea.Text.Contains(item.ToString()))
                {
                    DefList.Nodes.RemoveByKey(item.ToString());
                }
            }            
            if (result != -1)//&& result != 0
            {
                foreach (var ln in TextArea.Lines)
                {
                    if (ln.Text.StartsWith("def ") && ln.Text.EndsWith(")"))
                    {
                        string definitionWbrackets = ln.Text.Substring(ln.Text.IndexOf(" "));
                        string remove = definitionWbrackets.Substring(definitionWbrackets.LastIndexOf("("));
                        string definition = definitionWbrackets.Replace(remove, string.Empty);

                        if (!lists.Contains(definition.ToString()))
                        {
                            DefList.Nodes.Add(definition);                            
                        }
                        else { }
                    }
                    else { }
                }
            }
        }
        
        private static void AddToList(List<string> list, string ListItem)
        {
            list.Add(ListItem);
            list.Sort();
            list.OrderBy(m => m).ToList();
        }

        public static void InitZenCoding(ScintillaNET.Scintilla TextArea, CharAddedEventArgs e)
        {
            int caretPos = TextArea.SelectionStart;
            string curLine = TextArea.Lines[TextArea.CurrentLine].Text;
            string word = TextArea.GetWordFromPosition(caretPos);

            //complete html5 document.
            if (e.Char == '!' && curLine.Contains("@html"))
            {
                string pathToTemp = Application.StartupPath + "\\Files\\$HTML.txt";
                string template = File.ReadAllText(pathToTemp);
                if (template.Contains("($USER)")) { template = template.Replace("($USER)", Environment.UserName); }

                TextArea.InsertText(caretPos, template.ToString());
                TextArea.DeleteRange(caretPos - 6, 6);
                TextArea.GotoPosition(caretPos + 266);
            }
        }

        public static void SkipBraces(ScintillaNET.Scintilla TextArea, CharAddedEventArgs e)
        {
            //Skip repeating braces
            var caretPosition = TextArea.CurrentPosition;
            var docStart = caretPosition == 1;
            var docEnd = caretPosition == TextArea.Text.Length;
            var charPrev = docStart ? TextArea.GetCharAt(caretPosition) : TextArea.GetCharAt(caretPosition - 2);
            var charNext = TextArea.GetCharAt(caretPosition);
            if (e.Char == 0x29 && charPrev == 0x28 && charNext == 0x29)
            {
                TextArea.DeleteRange(caretPosition, 1);
                TextArea.GotoPosition(caretPosition);
            }
        }

        public static void SkipQuotes(ScintillaNET.Scintilla TextArea)
        {
            var caretPosition = TextArea.CurrentPosition;
            if (TextArea.GetCharAt(caretPosition) == '"' || TextArea.GetCharAt(caretPosition) == '\'')
            {
                TextArea.AutoCCancel();
            }
        }

        public static void FindWord(ScintillaNET.Scintilla TextArea)
        {
            //try
            //{
            var DefList = Application.OpenForms[0].Controls.Find("funcTree", true).FirstOrDefault() as TreeView;
            var lists = DefList.Nodes.OfType<string>().ToList();

            TextArea.TargetStart = 0;
            TextArea.TargetEnd = TextArea.TextLength;
            TextArea.SearchFlags = SearchFlags.None;

            while (TextArea.SearchInTarget("def ") != -1)
            {
                int line = TextArea.LineFromPosition(TextArea.TargetStart);
                string lineText = TextArea.Lines[line].Text;
                if (lineText.EndsWith(":"))
                {
                    string defBrack = lineText.Substring(lineText.IndexOf(" "));
                    string remove = defBrack.Substring(defBrack.LastIndexOf("("));
                    string definition = defBrack.Replace(remove, string.Empty);

                    if (!lists.Contains(definition))
                    {
                        DefList.Nodes.Add(definition);
                    }
                }
                else { }

                TextArea.TargetStart = TextArea.TargetEnd;
                TextArea.TargetEnd = TextArea.TextLength;
            }

            //Timer t = new Timer();
            //t.Interval = 3000;
            //t.Tick += new EventHandler((obj, ev) =>
            //{


            //    t.Stop();
            //    t.Enabled = false;
            //    t.Dispose();
            //});
            //t.Start();

            //}catch(Exception ex) { }
        }

        private static void CheckList(ScintillaNET.Scintilla TextArea)
        {
            var DefList = Application.OpenForms[0].Controls.Find("funcTree", true).FirstOrDefault() as TreeView;
            var lists = DefList.Nodes.OfType<string>().ToList();

            foreach (string item in lists)
            {
                if (!TextArea.Text.Contains(item))
                {
                    DefList.Nodes.RemoveByKey(item);
                }
            }
        }

        public static void SearchForImport(string key, ScintillaNET.Scintilla TextArea, CharAddedEventArgs e)
        {
            int caretPos = TextArea.SelectionStart;
            string word = TextArea.GetWordFromPosition(caretPos - 1);

            const int NUM = 8;
            TextArea.IndicatorCurrent = NUM;
            if (!TextArea.Text.Contains("import " + key) || !TextArea.Text.Contains("from " + key))//&& !TextArea.Text.StartsWith("#")
            {
                int result = 0;
                TextArea.TargetStart = 0;
                TextArea.TargetEnd = TextArea.TextLength;
                TextArea.SearchFlags = SearchFlags.WholeWord;
                result = TextArea.SearchInTarget(key);

                if (result != -1)
                {
                    foreach (var line in TextArea.Lines)
                    {
                        if (line.Text.Contains(key) && !line.Text.Contains("def ") && !line.Text.EndsWith(")") && !line.Text.StartsWith("class "))
                        {
                            //TextArea.IndicatorClearRange(0, TextArea.TextLength);
                            TextArea.Indicators[NUM].Style = IndicatorStyle.Squiggle;
                            TextArea.Indicators[NUM].Under = true;
                            TextArea.Indicators[NUM].ForeColor = Color.Red;
                            TextArea.IndicatorFillRange(TextArea.TargetStart, TextArea.TargetEnd - TextArea.TargetStart);
                            TextArea.TargetStart = TextArea.TargetEnd;
                            TextArea.TargetEnd = TextArea.TextLength;
                        }
                    }
                }
            }
            else
            {
                foreach (var line in TextArea.Lines)
                {
                    if (line.Text.Contains(key))
                    {
                        int before = line.Text.Substring(line.Text.IndexOf(key)).Length;
                        int after = line.Text.Substring(line.Text.LastIndexOf(key)).Length;
                        int givenLine = line.EndPosition - after;

                        int startPos = givenLine;
                        int lastPos = line.EndPosition;
                        TextArea.IndicatorClearRange(startPos, lastPos);        //<---FIND A BETTER WAY TO DO THIS! (THE LASTPOS IS LAST LINE POSITION)

                        //MessageBox.Show(givenLine.ToString());
                        //TextArea.TargetStart = line.EndPosition;
                        //TextArea.TargetEnd = TextArea.TextLength;

                        //TextArea.IndicatorClearRange(0, TextArea.TextLength);

                        if (e.Char == '.' && word.Contains(key))
                        {
                            TextArea.AutoCOrder = Order.PerformSort;
                            TextArea.AutoCShow(0, "gethostname?1 gethostbyname?1");
                        }
                    }
                }
            }
        }
    }
}
