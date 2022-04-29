using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;

namespace Netplait.Core.Funcs
{
    public class FuncController
    {
        public static void Undo(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Undo();
        }

        public static void Redo(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Redo();
        }

        public static void Cut(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Cut();
        }

        public static void Copy(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Copy();
        }

        public static void Paste(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Paste();
        }

        public static void Delete(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            int pos = sci.SelectionStart;
            int len = sci.SelectedText.Length;
            sci.DeleteRange(pos, len);
        }

        public static void SelectLine(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            Line line = sci.Lines[sci.CurrentLine];
            sci.SetSelection(line.Position + line.Length, line.Position);
        }

        public static void SelectAll(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.SelectAll();
        }

        public static void ClearSelection(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.SetEmptySelection(0);
        }

        public static void TransposeLine(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Focus();
            sci.ExecuteCmd(Command.LineTranspose);
        }

        public static void DuplicateLine(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Focus();
            sci.ExecuteCmd(Command.LineDuplicate);
        }

        const int BOOKMARK_MARKER = 2;
        static Bitmap bookmark = new Bitmap(Application.StartupPath + @"\\Icons\\Bookmark\\bookmark.png");
        public static void ToggleBookmark(TabControl tb, Action CountBookmarks)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            var line = sci.Lines[sci.LineFromPosition(sci.CurrentPosition)];

            const uint mask = (1 << BOOKMARK_MARKER);
            if ((line.MarkerGet() & mask) > 0)
            {
                line.MarkerDelete(BOOKMARK_MARKER);
            }
            else
            {
                line.MarkerAdd(BOOKMARK_MARKER);
            }
            CountBookmarks();
        }

        public static void Indent(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.ExecuteCmd(Command.Tab);
        }

        public static void Outdent(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.ExecuteCmd(Command.BackTab);
        }

        public static void Uppercase(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            //TODO
        }

        public static void Lowercase(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            //TODO
        }

        public static void CommentLine(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            //Comments.Comment
        }

        public static void UncommentLine(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            //Comments.Uncomment
        }

        //Toolstrip
        public static void PreviousBookmark(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Focus();

            var line = sci.LineFromPosition(sci.CurrentPosition);
            var prevLine = sci.Lines[--line].MarkerPrevious(1 << BOOKMARK_MARKER);
            if (prevLine != -1)
                sci.Lines[prevLine].Goto();
        }

        public static void NextBookmark(TabControl tb)
        {
            var sci = (ScintillaNET.Scintilla)tb.TabPages[tb.SelectedIndex].Controls[0];
            sci.Focus();

            var line = sci.LineFromPosition(sci.CurrentPosition);
            var nextLine = sci.Lines[++line].MarkerNext(1 << BOOKMARK_MARKER);
            if (nextLine != -1)
                sci.Lines[nextLine].Goto();
        }
    }
}
