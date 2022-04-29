using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Runner
{
    public class CurrentFile
    {
        public static void Run(TabControl tb, ToolStripComboBox opt, string proc, string args, Action Save, Action Out)
        {
            string path = tb.SelectedTab.Name.ToString();
            //run with python
            if (opt.SelectedIndex == 0)
            {
                //if file has been saved
                if (path != "" || path != string.Empty)
                {
                    args = args.Replace("($FILENAME)", path);
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(proc, args);
                    new Process() { StartInfo = processStartInfo }.Start();
                    Out();
                }
                //if file has not been saved
                else if (path == "" || path == string.Empty)
                {
                    Save();
                    args = args.Replace("($FILENAME)", path);
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(proc, args);
                    new Process() { StartInfo = processStartInfo }.Start();
                    Out();
                }
            }
            //run with associated program
            else
            {
                //if file has been saved
                if (path != "" || path != string.Empty)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(path);
                    new Process() { StartInfo = processStartInfo }.Start();
                    Out();
                }
                //if file has not been saved
                else if (path == "" || path == string.Empty)
                {
                    Save();
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(path);
                    new Process() { StartInfo = processStartInfo }.Start();
                    Out();
                }
            }
        }
    }
}