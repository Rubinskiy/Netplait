using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Netplait.Custom
{
    public class SecondaryTabControl : TabControl
    {
        public SecondaryTabControl()
        {
            this.DoubleBuffered = this.ResizeRedraw = true;
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

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
    }
}
