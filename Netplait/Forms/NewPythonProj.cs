using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Forms
{
    public partial class NewPythonProj : Form
    {
        string Docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public NewPythonProj()
        {
            InitializeComponent();

            textBox2.Text = Docpath + @"\Netplait\Projects";
            textBox5.Text = Environment.UserName;

            pictureBox2.Visible = false;

            this.comboBox1.Items.Add("Virtualenv");
            this.comboBox1.SelectedIndex = 0;

            //Check Python Interpreter
            GetPythonPath();
            GetPythonVersion();

            textBox1.Focus();
            textBox1.SelectAll();
        }

        #region Voids
        void SelectFolderPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;

            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
                Environment.SpecialFolder root = fbd.RootFolder;
            }
        }

        void GetPythonPath()
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";

            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;

            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(output);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            CmdProcess.StandardInput.WriteLine("where python&exit");
        }

        private void output(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>((result) =>
                    {
                        string[] strs = result.Split('\n');
                        foreach (string str in strs)
                        {
                            if (str.EndsWith("python.exe"))
                            {
                                this.textBox4.Text = str;
                                return;
                            }
                        }
                    }), new object[] { e.Data });
                }
            }
        }

        void GetPythonVersion()
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(version_output);
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(version_output);
            CmdProcess.EnableRaisingEvents = true;
            CmdProcess.Exited += new EventHandler(version_exit);

            CmdProcess.Start();
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();

            CmdProcess.StandardInput.WriteLine("python -V&exit");
        }

        private void version_exit(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (radioButton2.Text.Equals("Existing Interpreter"))
                    {
                        GetPythonVersion();
                    }
                    else { }
                }));
            }
        }

        private void version_output(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>((result) =>
                    {
                        string[] strs = result.Split('\n');
                        foreach (string str in strs)
                        {
                            if (str.StartsWith("Python"))
                            {
                                string[] python_paths = str.Split(' ');
                                string python_path = python_paths[1];
                                this.radioButton2.Text = "Existing Interpreter: " + python_path;
                                return;
                            }
                        }
                    }), new object[] { e.Data });
                }
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Netplait could not detect a Python environment on the system. " +
                            "This might happen for one of the following reasons:\n\n" +
                            "1. Python is installed. But the Environment Variables does not contain the Python PATH.\n" +
                            "2. You are using a Python version lower than Python 2.7.9, Which doesn't support pip.\n" +
                            "3. Python is not installed. Please install Python with Environment Variables before continuing.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }));
                }
            }
        }        
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            switch (splitContainer2.Panel2Collapsed)
            {
                case false:
                    splitContainer2.Panel2Collapsed = true;
                    button4.Text = "▸ Project Interpreter";
                    break;

                case true:
                    splitContainer2.Panel2Collapsed = false;
                    button4.Text = "▾ Project Interpreter";
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SelectFolderPath();
            }
            catch (Exception ex) { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            if (textBox1.Text != "") { button1.Enabled = true; pictureBox2.Visible = false; }
            else if (textBox1.Text == "") { button1.Enabled = false; pictureBox2.Visible = true; }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox1.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                textBox3.Enabled = false;
            }
        }
    }
}
