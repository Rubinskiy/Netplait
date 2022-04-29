using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Helpers
{
    public class Messages
    {
        public static void SetGreen(string msg, RichTextBox output)
        {
            int length = output.TextLength;
            output.AppendText(msg);
            output.SelectionStart = length;
            output.SelectionLength = msg.Length;
            output.SelectionColor = Color.Green;
        }

        public static void SetRed(string msg, RichTextBox output)
        {
            int length = output.TextLength;
            output.AppendText(msg);
            output.SelectionStart = length;
            output.SelectionLength = msg.Length;
            output.SelectionColor = Color.Red;
        }
    }
}
