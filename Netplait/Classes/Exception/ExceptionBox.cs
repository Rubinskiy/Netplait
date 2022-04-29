using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Classes.Exception
{
    public partial class ExceptionBox : Form
    {
        public ExceptionBox(string title, string message)
        {
            InitializeComponent();

            titleLbl.Text = title.ToString();
            msgRtb.Text = message.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Copy_details(RichTextBox rtb)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (string line in rtb.Lines)
                    sb.AppendLine(line);
                Clipboard.SetText(sb.ToString());
            }catch(ArgumentException ex) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Copy_details(msgRtb);
        }
    }
}
