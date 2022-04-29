using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Forms
{    
    public partial class NewFlaskProj : Form
    {
        string Docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public NewFlaskProj()
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
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
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
                    if (existingCheck.Text.Equals("Existing Interpreter"))
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
                                this.existingCheck.Text = "Existing Interpreter: " + python_path;
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
                        imgError.Visible = true;
                        status.Text = "Could not detect Python on the system.";
                        //MessageBox.Show("Netplait could not detect a Python environment on the system. " +
                        //    "This might happen for one of the following reasons:\n\n" +
                        //    "1. Python is installed. But the Environment Variables does not contain the Python PATH.\n" +
                        //    "2. You are using a Python version lower than Python 2.7.9, Which doesn't support pip.\n" +
                        //    "3. Python is not installed. Please install Python with Environment Variables before continuing.", "Error",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //https://pip.pypa.io/en/stable/reference/pip_show/
                        //IDE start page
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
            if (flaskCheck.Checked)
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

        string TempDir = Application.StartupPath + "\\Templates\\Temp\\";
        private void button1_Click(object sender, EventArgs e)
        {
            string venvFolder = textBox3.Text.ToString();
            string projLocation = textBox2.Text.ToString();
            string projName = textBox1.Text.ToString();
            string author = textBox5.Text.ToString();
            string desc = textBox6.Text.ToString();

            this.IsFinished = false;            
            string InstallArg;
            if (!radioButton3.Checked)
            {
                if (version_num.Text != "")
                {
                    InstallArg = "==" + version_num.Text;
                }
                else
                {
                    InstallArg = "";
                }
            }
            else
            {
                InstallArg = "";
            }
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true; 
            CmdProcess.StartInfo.RedirectStandardOutput = true;
            CmdProcess.StartInfo.RedirectStandardError = true;
            CmdProcess.OutputDataReceived += new DataReceivedEventHandler(OutPutToBox);
            CmdProcess.ErrorDataReceived += new DataReceivedEventHandler(ErrToBox);
            CmdProcess.EnableRaisingEvents = true;
            CmdProcess.Exited += new EventHandler(ExitEvent);
            CmdProcess.Start();

            try
            {
                //Delete any temporary files in $TempDir that might disturb the creation of projects
                if (System.IO.Directory.Exists(TempDir)) { Directory.Delete(TempDir, true); }
                if (!System.IO.Directory.Exists(TempDir)) { Directory.CreateDirectory(TempDir); }

                if (flaskCheck.Checked)
                {
                    Classes.FlaskFileHost.CreateFlaskProjectFiles(projLocation, projName, author, desc);
                    CmdProcess.StandardInput.WriteLine("cd " + projLocation + "\\" + projName + "&" + "virtualenv " + venvFolder + "&"
                        + venvFolder + @"\Scripts\activate" + "&" + "pip install flask" + InstallArg + "&exit");

                    this.ControlBox = false;
                    status.Text = "Started Pip..";
                    this.button1.Enabled = false;

                    CmdProcess.BeginErrorReadLine();
                    CmdProcess.BeginOutputReadLine();

                    //Then finally
                    //Main.toolStripMenuItem4.Enabled = true;
                    //Main.toolStripMenuItem1.Enabled = true;
                }
                else if (existingCheck.Checked)
                {
                    status.Text = "Creating project..";
                    this.ControlBox = false;
                    Classes.FlaskFileHost.CreateFlaskProjectFiles(projLocation, projName, author, desc);

                    status.Text = "Project created successfully at " + DateTime.Now.ToShortTimeString();
                    Close();
                }
            }catch(Exception ex) {
                if (System.IO.Directory.Exists(TempDir)) { Directory.Delete(TempDir, true); }
                else { System.IO.Directory.CreateDirectory(TempDir); }
                status.Text = "Interrupted";
                MessageBox.Show("Netplait has encountered an error while creating a project. " +
                        "This error can happen for one of the following reasons:\n\n" + 
                        "1. A folder with the same name exists. Please select another project name.\n" +
                        "2. Netplait does not have permission to create files and folders to the specified directory.\n" +
                        "3. Default document templates have been corrupted. If you think this is the case, please try re-installing the program. ",
                        "Netplait 2.5.1", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void OutPutToBox(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>((result) =>
                    {
                        string[] results = result.Split('\n');
                        foreach (string i in results)
                        {
                            status.Text = i;
                        }

                        //disable all action related controls
                        foreach (Control c in this.Controls)
                        {
                            c.Enabled = false;
                        }

                        progressBar1.Style = ProgressBarStyle.Marquee;
                        progressBar1.MarqueeAnimationSpeed = 40;
                        this.Refresh();
                    }), new object[] { e.Data });
                }
            }
        }

        private void ErrToBox(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>((ex) =>
                    {
                        MessageBox.Show("Pip has sent a warning/error during the installation of Flask.\n\n" + ex.ToString()
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        progressBar1.MarqueeAnimationSpeed = 0;
                        this.button1.Enabled = true;
                        this.Refresh();
                        this.ControlBox = true;
                        return;
                    }), new object[] { e.Data });
                }
            }
        }

        private void ExitEvent(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {                    
                    try
                    {
                        foreach (Control c in this.Controls) { c.Enabled = true; }
                        this.IsFinished = true;
                        this.ControlBox = true;
                        status.Text = "Project created successfully at " + DateTime.Now.ToShortTimeString();
                        Close();
                    }
                    catch { }
                }));
            }
        }

        public bool IsFinished { get; set; } = false;
        private void version_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked)
            {
                radioButton3.Checked = false;
            }
            else
            {
                radioButton3.Checked = true;
            }
        }

        private void version_num_TextChanged(object sender, EventArgs e)
        {
            if (version_num.Text != "")
            {
                if (radioButton3.Checked)
                {
                    radioButton3.Checked = false;
                }
            }
            else
            {
                if (!radioButton3.Checked)
                {
                    radioButton3.Checked = true;
                }
            }
        }

        private void NewFlaskProj_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!this.IsFinished)
            //{
            //    DialogResult flag = MessageBox.Show("Confirm to close？" + Environment.NewLine + "Closing does not stop the ongoing installation or uninstallation", "caveat", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (flag == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void existingCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
