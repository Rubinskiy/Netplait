using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Netplait.Helpers
{
    public class Margins
    {
        const int NUMBER_MARGIN = 1;
        const int FOLDING_MARGIN = 3;
        const int BOOKMARK_MARGIN = 2;
        public const int BOOKMARK_MARKER = 2;
        public static Bitmap bookmark = new Bitmap(Application.StartupPath + @"\\Icons\\Bookmark\\bookmark.png");

        public static void InitNumberMargin(ScintillaNET.Scintilla TextArea, Color back, Color fore)
        {
            TextArea.Styles[Style.LineNumber].BackColor = back;
            TextArea.Styles[Style.LineNumber].ForeColor = fore;
            TextArea.Styles[Style.IndentGuide].ForeColor = fore;
            TextArea.Styles[Style.IndentGuide].BackColor = back;

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = Properties.Settings.Default.NumberMarginWidth;
            nums.Sensitive = false;
            nums.Mask = 0;
            nums.BackColor = back;

            //Display Line Number
            if (Properties.Settings.Default.DisplayLineNumber == true)
                nums.Type = MarginType.Number;
            else { nums.Type = MarginType.Symbol; }
        }

        public static void InitBookmarkMargin(ScintillaNET.Scintilla TextArea, Color back)
        {
            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = Properties.Settings.Default.BookmarkMarginWidth;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            margin.Cursor = MarginCursor.ReverseArrow;
            margin.BackColor = back;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.RgbaImage;
            marker.DefineRgbaImage(bookmark);
            marker.SetBackColor(back);
        }

        public static void InitCodeFolding(ScintillaNET.Scintilla TextArea, Color back, Color fore)
        {
            TextArea.SetFoldMarginColor(true, back);
            TextArea.SetFoldMarginHighlightColor(true, back);

            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = Properties.Settings.Default.MarginWidth;
            TextArea.Margins[FOLDING_MARGIN].BackColor = back;

            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                TextArea.Markers[i].SetForeColor(back);
                TextArea.Markers[i].SetBackColor(fore);
            }

            TextArea.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            TextArea.SetFoldFlags(FoldFlags.LineAfterContracted);
        }

        private static void AddMarker_ToList(string MarkerLine, string MarkerContents)
        {
            var imageList = new ImageList();
            imageList.Images.Add("0", bookmark);

            var MarkerList = Application.OpenForms[0].Controls.Find("MarkerList", true).FirstOrDefault() as ListView;
            var tabControl = Application.OpenForms[0].Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;

            MarkerList.SmallImageList = imageList;

            var GroupName = new ListViewGroup(tabControl.SelectedTab.Text);
            MarkerList.Groups.Add(GroupName);

            var List = new ListViewItem(new[] { MarkerLine, MarkerContents });
            List.ImageKey = "0";

            List.Name = MarkerLine;
            MarkerList.Items.Add(List);
        }

        private static void RemoveMarker_FromList(string MarkerLine, string MarkerContents)
        {
            var MarkerList = Application.OpenForms[0].Controls.Find("MarkerList", true).FirstOrDefault() as ListView;
            MarkerList.Items.RemoveByKey(MarkerLine);
        }

        public static void CountBookmarks(TabControl tb, ToolStripLabel lbl)
        {
            int counter = 0;
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            const uint mask = (1 << Helpers.Margins.BOOKMARK_MARKER);
            foreach (ScintillaNET.Line line in sci.Lines)
            {
                if ((line.MarkerGet() & mask) > 0)
                {
                    counter += 1;
                    lbl.Text = "Bookmarks in file: " + counter.ToString();
                }
            }

            if (counter == 0)
            {
                lbl.Text = "No bookmarks";
            }
        }

        /// <summary>
        /// This method will decide what happens when the editor's margin bar is clicked
        /// </summary>
        /// <param name="TextArea">The main scintilla control</param>
        public static void TextArea_MarginClick(object sender, MarginClickEventArgs e, ScintillaNET.Scintilla TextArea)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];

                string LineNum = line.Index.ToString();
                string LineText = TextArea.Lines[line.Index].Text;

                var tb = Application.OpenForms[0].Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
                MainForm form = (MainForm)Application.OpenForms[0];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    //RemoveMarker_FromList(LineNum, LineText.ToString());
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    //AddMarker_ToList(LineNum, LineText.ToString());
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
                CountBookmarks(tb, form.bmLbl);
            }
        }

        //TODO
        public static void MarginChecking(ScintillaNET.Scintilla TextArea)
        {
            //FOR EACH MARKER IN PAGE, LIST IT IN LISTVIEW <- CHARADDED ?
            var MarkerList = Application.OpenForms[0].Controls.Find("MarkerList", true).FirstOrDefault() as ListView;
            var tabControl = Application.OpenForms[0].Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;

            var GroupName = new ListViewGroup(tabControl.SelectedTab.Text);
            var line = TextArea.Lines[TextArea.CurrentLine];
            string LineNum = line.Index.ToString();
            string LineText = TextArea.Lines[line.Index].Text;
            var List = new ListViewItem(new[] { LineNum, LineText });

            //var col1 = MarkerList.Columns[1];
            //var col2 = MarkerList.Columns[2];
            //if (MarkerList.Groups.Contains(GroupName))
            //{
            //    int index = MarkerList.Groups.IndexOf(GroupName);
            //    foreach (var item in MarkerList.Groups[index].Items)
            //    {
            //        if (!TextArea.Text.Contains(item.ToString()))
            //        {
            //            MarkerList.Items.RemoveByKey(item.ToString());
            //        }
            //    }
            //}
            //else
            //{
            //    //Does not contain file in tabcontrol
            //}

            //FOREACH ITEM LISTVIEWITEM
            //FOREACH ITEM
            //INT INDEX = CONVERTTOINT32(LISTVIEWITEM.COLUMNS[1].TEXT
            //IF THAT LINE NUMBER'S CONTENTS CONTAINS WHAT THE SAME LINE NUMBER IN TEXTAREA CONTAINS THEN IT IS VALID
            //ELSE REMOVEIT

            if (MarkerList.Groups.Contains(GroupName))
            {
                var item = MarkerList.FindItemWithText(LineText);
                if (item != null)
                {
                    int lineIndex = MarkerList.Items.IndexOf(item);
                    if (!TextArea.Text.Contains(item.ToString()))
                    {
                        MarkerList.Items.Remove(item);
                        //Or just removebykey by line number
                    }
                }
            }
        }
    }
}
