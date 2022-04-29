using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Custom.Tabpages
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();

            //Picturebox init
            BannerPic.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
