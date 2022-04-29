using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Progress
{
    public partial class ProgressRequest : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public static ProgressRequest sForm = null;
        public static ProgressRequest Instance()
        {
            if (sForm == null) { sForm = new ProgressRequest(); }
            return sForm;
        }
        
        public ProgressRequest()
        {            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        int originalHeight { get; set; }
        public void ProgressRequest_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 40;
            this.Refresh();

            originalHeight = this.Height;
        }        

        private void button4_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/10532114/expand-and-collapse-winform
            if (this.Height > 100)
            {                
                this.Height = originalHeight - panel1.Height;
                button4.Text = "▸ Console output";
            }
            else
            {
                this.Height = originalHeight;
                button4.Text = "▾ Console output";
            }
        }
    }
}
