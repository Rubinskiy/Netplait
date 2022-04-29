using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Netplait.Custom
{
    public class NetTabControl : TabControl
    {
        public NetTabControl()
        {
            this.DoubleBuffered = this.ResizeRedraw = true;
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            base.DragOver += tb_DragOver;
            base.MouseMove += tb_MouseMove;
            base.MouseDown += tb_MouseDown;
            base.MouseLeave += tb_MouseLeave;
        }

        private const int WS_EX_COMPOSITED = 0x02000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        private int hoveredIndex = -1;
        private void tb_MouseLeave(object sender, EventArgs e)
        {
            if (hoveredIndex != -1)
            {
                base.Invalidate(base.GetTabRect(hoveredIndex));
                hoveredIndex = -1;
            }
        }

        private Point DragStartPosition = Point.Empty;
        private void tb_DragOver(object sender, DragEventArgs e)
        {
            TabPage hover_Tab = HoverTab();
            if (hover_Tab == null)
                e.Effect = DragDropEffects.None;
            else
            {
                if (e.Data.GetDataPresent(typeof(TabPage)))
                {
                    e.Effect = DragDropEffects.Move;
                    TabPage drag_tab = (TabPage)e.Data.GetData(typeof(TabPage));

                    if (hover_Tab == drag_tab) return;

                    Rectangle TabRect = base.GetTabRect(base.TabPages.IndexOf(hover_Tab));
                    TabRect.Inflate(-3, -3);
                    if (TabRect.Contains(base.PointToClient(new Point(e.X, e.Y))))
                    {
                        SwapTabPages(drag_tab, hover_Tab);
                        base.SelectedTab = drag_tab;
                    }
                }
            }
        }

        private void tb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Rectangle r = new Rectangle(DragStartPosition, Size.Empty);
            r.Inflate(SystemInformation.DragSize);

            TabPage tp = HoverTab();

            if (tp != null)
            {
                if (!r.Contains(e.X, e.Y))
                    base.DoDragDrop(tp, DragDropEffects.All);
            }
            DragStartPosition = Point.Empty;
        }

        private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            DragStartPosition = new Point(e.X, e.Y);
        }

        private TabPage HoverTab()
        {
            for (int index = 0; index <= base.TabCount - 1; index++)
            {
                if (base.GetTabRect(index).Contains(base.PointToClient(Cursor.Position)))
                    return base.TabPages[index];
            }
            return null;
        }

        private void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            int Index1 = base.TabPages.IndexOf(tp1);
            int Index2 = base.TabPages.IndexOf(tp2);
            base.TabPages[Index1] = tp2;
            base.TabPages[Index2] = tp1;
        }
    }
}
