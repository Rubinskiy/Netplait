using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Packager
{
    public class Interpreter
    {
        public static void Retrieve_packages(ListView lstv, ComboBox combo, DataReceivedEventHandler Output, EventHandler Exit)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;


            lstv.Items.Clear();
            lstv.Items.Add("Loading data...");
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(Output);
            CmdProcess.EnableRaisingEvents = true;
            CmdProcess.Exited += new EventHandler(Exit);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            if (combo.SelectedItem != "")
            {
                //if an interpreter exists and is selected
                CmdProcess.StandardInput.WriteLine(combo.SelectedItem + " -m pip list&exit");
            }
            else
            {
                //if an interpreter is not selected, proceed with pip
                CmdProcess.StandardInput.WriteLine("python -m pip list&exit");
            }
        }
    }
}
