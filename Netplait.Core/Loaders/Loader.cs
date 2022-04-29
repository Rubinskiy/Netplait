using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Core.Loaders
{
    public partial class Loader : Form
    {
        //drop shadow on form
        private const int DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = DropShadow;
                return cp;
            }
        }

        public Loader()
        {
            InitializeComponent();

            lbl_name.Text = "Adding files to project...";
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public static Loader sForm = null;
        public static Loader Instance()
        {
            if (sForm == null) { sForm = new Loader(); }

            return sForm;
        }

        private void Loader_Load(object sender, EventArgs e)
        {
            bar.Style = ProgressBarStyle.Marquee;
            bar.MarqueeAnimationSpeed = 30;
            this.Refresh();
        }
    }
}
