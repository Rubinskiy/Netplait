using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;
using ScintillaNET;
using Netplait.Packager.Commands;
using System.Diagnostics;

namespace Netplait.Forms
{
    public partial class Preferences : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        public Preferences(int mode)
        {
            InitializeComponent();

            try { SetWindowTheme(treeView1.Handle, "explorer", null); }catch(Exception ex) { }

            foreach (FontFamily font in System.Drawing.FontFamily.Families) { comboBox9.Items.Add(font.Name);}
            FilterBox.ForeColor = Color.Silver;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            treeView1.SelectedNode = treeView1.Nodes[0];

            if (mode == 1)
            {
                TreeNode tn = treeView1.Nodes[2];
                tn.Collapse();
                TreeNode tn1 = tn.Nodes[0];
                treeView1.SelectedNode = tn1;
                tabControl1.SelectedTab = tabPage8;
            }

            #region SETTINGS
            //checkBox2
            if (Properties.Settings.Default.StartPage == true) { checkBox2.Checked = true; }
            else { checkBox2.Checked = false; }

            //checkBox3
            if (Properties.Settings.Default.AutoComplete == true) { checkBox3.Checked = true; }
            else { checkBox3.Checked = false; }

            //checkBox5
            if (Properties.Settings.Default.HighlightLine == true) { checkBox5.Checked = true;}
            else { checkBox5.Checked = false;}

            //panel3
            panel3.BackColor = Properties.Settings.Default.CaretLineBackColor;

            //panel7
            panel7.BackColor = Properties.Settings.Default.SetSelectionBackColor;

            //checkBox6
            if (Properties.Settings.Default.DisplayLineNumber == true) { checkBox6.Checked = true; }
            else { checkBox6.Checked = false; }

            //checkBox10
            if (Properties.Settings.Default.ScrollPastLastLine == true) { checkBox10.Checked = true; }
            else { checkBox10.Checked = false; }

            //comboBox2
            comboBox2.Items.Add("None");
            comboBox2.Items.Add("NoWrapLineStart");
            comboBox2.Items.Add("RectangularSelection");
            comboBox2.Items.Add("UserAccessible");
            if(Properties.Settings.Default.VirtualSpaceOptions == ScintillaNET.VirtualSpace.None) { comboBox2.SelectedIndex = 0; }
            else if(Properties.Settings.Default.VirtualSpaceOptions == ScintillaNET.VirtualSpace.NoWrapLineStart) { comboBox2.SelectedIndex = 1; }
            else if(Properties.Settings.Default.VirtualSpaceOptions == ScintillaNET.VirtualSpace.RectangularSelection) { comboBox2.SelectedIndex = 2; }
            else if(Properties.Settings.Default.VirtualSpaceOptions == ScintillaNET.VirtualSpace.UserAccessible) { comboBox2.SelectedIndex = 3; }

            //comboBox3
            comboBox3.Items.Add("Char");
            comboBox3.Items.Add("None");
            comboBox3.Items.Add("Whitespace");
            comboBox3.Items.Add("Word");
            if (Properties.Settings.Default.WrapMode == ScintillaNET.WrapMode.Char) { comboBox3.SelectedIndex = 0; }
            else if (Properties.Settings.Default.WrapMode == ScintillaNET.WrapMode.None) { comboBox3.SelectedIndex = 1; }
            else if (Properties.Settings.Default.WrapMode == ScintillaNET.WrapMode.Whitespace) { comboBox3.SelectedIndex = 2; }
            else if (Properties.Settings.Default.WrapMode == ScintillaNET.WrapMode.Word) { comboBox3.SelectedIndex = 3; }

            //comboBox4
            comboBox4.Items.Add("Cr");
            comboBox4.Items.Add("CrLf");
            comboBox4.Items.Add("Lf");
            if (Properties.Settings.Default.EolMode == ScintillaNET.Eol.Cr) { comboBox4.SelectedIndex = 0; }
            else if (Properties.Settings.Default.EolMode == ScintillaNET.Eol.CrLf) { comboBox4.SelectedIndex = 1; }
            else if (Properties.Settings.Default.EolMode == ScintillaNET.Eol.Lf) { comboBox4.SelectedIndex = 2; }

            //checkBox9
            if (Properties.Settings.Default.ViewEol == true) { checkBox9.Checked = true; }
            else { checkBox9.Checked = false; }

            //checkBox4
            if (Properties.Settings.Default.UseTabs == true) { checkBox4.Checked = true; }
            else { checkBox4.Checked = false; }

            //numericUpDown3
            numericUpDown3.Value = Properties.Settings.Default.TabWidth;

            //checkBox8
            if (Properties.Settings.Default.IndentationGuides == ScintillaNET.WhitespaceMode.Invisible) { checkBox8.Checked = false; }
            else { checkBox8.Checked = true; }

            //comboBox6
            comboBox6.Items.Add("LongArrow");
            comboBox6.Items.Add("Strikeout");
            if (Properties.Settings.Default.TabDrawMode == ScintillaNET.TabDrawMode.LongArrow) { comboBox6.SelectedIndex = 0; }
            else if (Properties.Settings.Default.TabDrawMode == ScintillaNET.TabDrawMode.Strikeout) { comboBox6.SelectedIndex = 1; }

            //panel4
            panel4.BackColor = Properties.Settings.Default.CaretForeColor;

            //comboBox1
            comboBox1.Items.Add("Line");
            comboBox1.Items.Add("Block");
            comboBox1.Items.Add("Invisible");
            if (Properties.Settings.Default.CaretStyle == ScintillaNET.CaretStyle.Line) { comboBox1.SelectedIndex = 0; }
            else if (Properties.Settings.Default.CaretStyle == ScintillaNET.CaretStyle.Block) { comboBox1.SelectedIndex = 1; }
            else if (Properties.Settings.Default.CaretStyle == ScintillaNET.CaretStyle.Invisible) { comboBox1.SelectedIndex = 2; }

            //numericUpDown1
            numericUpDown1.Value = Properties.Settings.Default.CaretWidth;

            //numericUpDown5
            numericUpDown5.Value = Properties.Settings.Default.MarginWidth;

            //comboBox9
            comboBox9.SelectedItem = Properties.Settings.Default.GlobalFont.ToString();

            //comboBox5
            comboBox5.Items.Add("AntiAliased");
            comboBox5.Items.Add("Default");
            comboBox5.Items.Add("LcdOptimized");
            comboBox5.Items.Add("NonAntiAliased");
            if (Properties.Settings.Default.FontQuality == ScintillaNET.FontQuality.AntiAliased) { comboBox5.SelectedIndex = 0; }
            else if (Properties.Settings.Default.FontQuality == ScintillaNET.FontQuality.Default) { comboBox5.SelectedIndex = 1; }
            else if (Properties.Settings.Default.FontQuality == ScintillaNET.FontQuality.LcdOptimized) { comboBox5.SelectedIndex = 2; }
            else if (Properties.Settings.Default.FontQuality == ScintillaNET.FontQuality.NonAntiAliased) { comboBox5.SelectedIndex = 3; }

            //numericUpDown4
            numericUpDown4.Value = Properties.Settings.Default.GlobalFontSize;

            //checkBox11
            if (Properties.Settings.Default.ShowBrackets == true) { checkBox11.Checked = true; }
            else { checkBox11.Checked = false; }

            //checkBox12
            if (Properties.Settings.Default.ShowCurlyBrackets == true) { checkBox12.Checked = true; }
            else { checkBox12.Checked = false; }

            //checkBox13
            if (Properties.Settings.Default.ShowSquareBrackets == true) { checkBox13.Checked = true; }
            else { checkBox13.Checked = false; }

            //checkBox14
            if (Properties.Settings.Default.ShowAngleBrackets == true) { checkBox14.Checked = true; }
            else { checkBox14.Checked = false; }

            //checkBox15
            if (Properties.Settings.Default.ShowQuotes == true) { checkBox15.Checked = true; }
            else { checkBox15.Checked = false; }

            //checkBox16
            if (Properties.Settings.Default.ShowSingleQuote == true) { checkBox16.Checked = true; }
            else { checkBox16.Checked = false; }

            //panel5
            panel5.BackColor = Properties.Settings.Default.BraceLightBack;

            //panel6
            panel6.BackColor = Properties.Settings.Default.BraceLightFore;

            //numericUpDown2
            numericUpDown2.Value = Properties.Settings.Default.NumberMarginWidth;

            //numericUpDown6
            numericUpDown6.Value = Properties.Settings.Default.BookmarkMarginWidth;

            //pythonPath
            pythonPath.Items.Add(Properties.Settings.Default.PythonPath.ToString());
            pythonPath.SelectedIndex = 0;

            //pyWith
            pyArgs.Text = Properties.Settings.Default.PythonRunArgs.ToString();

            //pyArgs
            pyWith.Text = Properties.Settings.Default.PythonRunWith.ToString();

            //workspace_align
            if (Properties.Settings.Default.RightToLeft == true) { workspace_align.Checked = true; }
            else { workspace_align.Checked = false; }
            #endregion
        }

        #region GetcolorEvents
        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        void GetColor(Panel p)
        {
            if (cdl.ShowDialog() == DialogResult.OK)
            {
                p.BackColor = IntToColor(cdl.Color.ToArgb());
            }
        }
        
        private void panel3_Click(object sender, EventArgs e)
        {
            GetColor(panel3);
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            GetColor(panel7);
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            GetColor(panel4);
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            GetColor(panel5);
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            GetColor(panel6);
        }
        #endregion

        private TabPage Findtb(string option)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                string tabName = tab.Text;
                if (string.Equals(option, tabName))
                    return (tab);
            }
            return (null);
        }

        private void FilterBox_Enter(object sender, EventArgs e)
        {
            if (FilterBox.Text == "Type Filter Text")
            {
                FilterBox.Text = "";
                FilterBox.ForeColor = Color.Black;
            }
        }

        private void FilterBox_Leave(object sender, EventArgs e)
        {
            if (FilterBox.Text == "")
            {
                FilterBox.Text = "Type Filter Text";
                FilterBox.ForeColor = Color.Silver;
            }
        }

        private void FilterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Action != TreeViewAction.Collapse && e.Action != TreeViewAction.Expand)
                {
                    string option = e.Node.FullPath;
                    Titlelbl.Text = option.Replace("\\", " ▸ ");
                    TabPage tab = Findtb(option);
                    if (tab != null)
                        tabControl1.SelectedTab = tab;
                    else
                        tabControl1.SelectedIndex = -1;
                }
            }catch(Exception ex) { }
        }

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                }
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);                    
                };
                StartNode = StartNode.NextNode;
            };

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Disable button
            button2.Enabled = false;

            //Save settings
            SaveSettings();

            //Enable button
            Timer t = new Timer();
            t.Interval = 3000;
            t.Tick += new EventHandler((obj, ev) =>
            {
                button2.Enabled = true;

                t.Stop();
                t.Enabled = false;
                t.Dispose();
            });
            t.Start();
        }

        private void OnLoad()
        {
            Forms.PythonConfiguration.Models.Customer bill = new Forms.PythonConfiguration.Models.Customer();
            bill.Age = 50;
            bill.Address = " 114 Maple Drive ";
            bill.DateOfBirth = Convert.ToDateTime(" 9/14/78");
            bill.SSN = "123-345-3566";
            bill.Email = "bill@aol.com";
            bill.Name = "Bill Smith";
            bill.Filename = @"C:\Users\Kids\Desktop\yoy.exe";
            //pythonConfigGrid.SelectedObject = bill;

            //TODAY DO ALL SYNTAX
            //THEN IMPLEMENT PROJECTS AND MAINFORM_CLOSING
        }

        private void SaveSettings()
        {
            try
            {
                //Settings
                if (checkBox2.Checked) { Properties.Settings.Default.StartPage = true; }
                else { Properties.Settings.Default.StartPage = false; }

                if (checkBox3.Checked) { Properties.Settings.Default.AutoComplete = true; }
                else { Properties.Settings.Default.AutoComplete = false; }

                if (checkBox5.Checked) { Properties.Settings.Default.HighlightLine = true; }
                else { Properties.Settings.Default.HighlightLine = false; }

                Properties.Settings.Default.CaretLineBackColor = panel3.BackColor;

                Properties.Settings.Default.SetSelectionBackColor = panel7.BackColor;

                if (checkBox6.Checked) { Properties.Settings.Default.DisplayLineNumber = true; }
                else { Properties.Settings.Default.DisplayLineNumber = false; }

                if (checkBox10.Checked) { Properties.Settings.Default.ScrollPastLastLine = true; }
                else { Properties.Settings.Default.ScrollPastLastLine = false; }

                if (comboBox2.SelectedIndex == 0) { Properties.Settings.Default.VirtualSpaceOptions = ScintillaNET.VirtualSpace.None; }
                else if (comboBox2.SelectedIndex == 1) { Properties.Settings.Default.VirtualSpaceOptions = ScintillaNET.VirtualSpace.NoWrapLineStart; }
                else if (comboBox2.SelectedIndex == 2) { Properties.Settings.Default.VirtualSpaceOptions = ScintillaNET.VirtualSpace.RectangularSelection; }
                else if (comboBox2.SelectedIndex == 3) { Properties.Settings.Default.VirtualSpaceOptions = ScintillaNET.VirtualSpace.UserAccessible; }

                if (comboBox3.SelectedIndex == 0) { Properties.Settings.Default.WrapMode = ScintillaNET.WrapMode.Char; }
                else if (comboBox3.SelectedIndex == 1) { Properties.Settings.Default.WrapMode = ScintillaNET.WrapMode.None; }
                else if (comboBox3.SelectedIndex == 2) { Properties.Settings.Default.WrapMode = ScintillaNET.WrapMode.Whitespace; }
                else if (comboBox3.SelectedIndex == 3) { Properties.Settings.Default.WrapMode = ScintillaNET.WrapMode.Word; }

                if (comboBox4.SelectedIndex == 0) { Properties.Settings.Default.EolMode = ScintillaNET.Eol.Cr; }
                else if (comboBox4.SelectedIndex == 1) { Properties.Settings.Default.EolMode = ScintillaNET.Eol.CrLf; }
                else if (comboBox4.SelectedIndex == 2) { Properties.Settings.Default.EolMode = ScintillaNET.Eol.Lf; }

                if (checkBox9.Checked) { Properties.Settings.Default.ViewEol = true; }
                else { Properties.Settings.Default.ViewEol = false; }

                if (checkBox4.Checked) { Properties.Settings.Default.UseTabs = true; }
                else { Properties.Settings.Default.UseTabs = false; }

                Properties.Settings.Default.TabWidth = Convert.ToInt32(numericUpDown3.Value);

                if (checkBox8.Checked) { Properties.Settings.Default.IndentationGuides = ScintillaNET.WhitespaceMode.VisibleAlways; }
                else { Properties.Settings.Default.IndentationGuides = ScintillaNET.WhitespaceMode.Invisible; }

                if (comboBox6.SelectedIndex == 0) { Properties.Settings.Default.TabDrawMode = ScintillaNET.TabDrawMode.LongArrow; }
                else if (comboBox6.SelectedIndex == 1) { Properties.Settings.Default.TabDrawMode = ScintillaNET.TabDrawMode.Strikeout; }

                Properties.Settings.Default.CaretForeColor = panel4.BackColor;

                if (comboBox1.SelectedIndex == 0) { Properties.Settings.Default.CaretStyle = ScintillaNET.CaretStyle.Line; }
                else if (comboBox1.SelectedIndex == 1) { Properties.Settings.Default.CaretStyle = ScintillaNET.CaretStyle.Block; }
                else if (comboBox1.SelectedIndex == 2) { Properties.Settings.Default.CaretStyle = ScintillaNET.CaretStyle.Invisible; }

                Properties.Settings.Default.CaretWidth = Convert.ToInt32(numericUpDown1.Value);

                Properties.Settings.Default.MarginWidth = Convert.ToInt32(numericUpDown5.Value);

                Properties.Settings.Default.GlobalFont = comboBox9.SelectedItem.ToString();

                if (comboBox5.SelectedIndex == 0) { Properties.Settings.Default.FontQuality = ScintillaNET.FontQuality.AntiAliased; }
                else if (comboBox5.SelectedIndex == 1) { Properties.Settings.Default.FontQuality = ScintillaNET.FontQuality.Default; }
                else if (comboBox5.SelectedIndex == 2) { Properties.Settings.Default.FontQuality = ScintillaNET.FontQuality.LcdOptimized; }
                else if (comboBox5.SelectedIndex == 3) { Properties.Settings.Default.FontQuality = ScintillaNET.FontQuality.NonAntiAliased; }

                Properties.Settings.Default.GlobalFontSize = Convert.ToInt32(numericUpDown4.Value);

                if (checkBox11.Checked) { Properties.Settings.Default.ShowBrackets = true; }
                else { Properties.Settings.Default.ShowBrackets = false; }

                if (checkBox12.Checked) { Properties.Settings.Default.ShowCurlyBrackets = true; }
                else { Properties.Settings.Default.ShowCurlyBrackets = false; }

                if (checkBox13.Checked) { Properties.Settings.Default.ShowSquareBrackets = true; }
                else { Properties.Settings.Default.ShowSquareBrackets = false; }

                if (checkBox14.Checked) { Properties.Settings.Default.ShowAngleBrackets = true; }
                else { Properties.Settings.Default.ShowAngleBrackets = false; }

                if (checkBox15.Checked) { Properties.Settings.Default.ShowQuotes = true; }
                else { Properties.Settings.Default.ShowQuotes = false; }

                if (checkBox16.Checked) { Properties.Settings.Default.ShowSingleQuote = true; }
                else { Properties.Settings.Default.ShowSingleQuote = false; }

                Properties.Settings.Default.BraceLightBack = panel5.BackColor;

                Properties.Settings.Default.BraceLightFore = panel6.BackColor;

                Properties.Settings.Default.NumberMarginWidth = Convert.ToInt32(numericUpDown2.Value);

                Properties.Settings.Default.BookmarkMarginWidth = Convert.ToInt32(numericUpDown6.Value);

                string path = pythonPath.SelectedItem.ToString();
                if (path == "" || !path.Contains("python.exe"))
                {
                    task_error(img1, lbl1, "Invalid Python interpreter.", 6000);
                }
                else if (path != "" && path.Contains("python.exe"))
                {
                    Properties.Settings.Default.PythonPath = pythonPath.SelectedItem.ToString();
                    Properties.Settings.Default.CheckForPath = false;
                }

                Properties.Settings.Default.PythonRunArgs = pyArgs.Text;

                Properties.Settings.Default.PythonRunWith = pyWith.Text;

                if (workspace_align.Checked) { Properties.Settings.Default.RightToLeft = true; }
                else { Properties.Settings.Default.RightToLeft = false; }

                //Save and Close
                Properties.Settings.Default.Save();
                ConfigurationManager.RefreshSection("AppSettings");

                //Reload open instances of scintilla
                var tb = Application.OpenForms[0].Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
                Forms.Reload.ReloadOnDemand.ReloadSciOnDemand(tb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Unable to save settings. Here are the details:", ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = this.FilterBox.Text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };

            if (LastSearchText != searchText)
            {
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, treeView1.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeView1.SelectedNode = selectedNode;
                this.treeView1.SelectedNode.Expand();
                this.treeView1.Select();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();

            //Close
            this.Hide();
        }

        #region Font region
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            sci_fonts.Styles[Style.Default].Font = comboBox9.SelectedItem.ToString();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
                sci_fonts.FontQuality = FontQuality.AntiAliased;
            else if (comboBox5.SelectedIndex == 1)
                sci_fonts.FontQuality = FontQuality.Default;
            else if (comboBox5.SelectedIndex == 2)
                sci_fonts.FontQuality = FontQuality.LcdOptimized;
            else if (comboBox5.SelectedIndex == 3)
                sci_fonts.FontQuality = FontQuality.NonAntiAliased;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            sci_fonts.Styles[Style.Default].Size = (int)numericUpDown4.Value;
        }
        #endregion

        #region DataEventArgs
        private void task_error(PictureBox img, Label lbl, string lblText, int time)
        {
            tabControl1.SelectedTab = tabPage8;
            lbl.Text = lblText;

            img.Visible = true;
            lbl.Visible = true;

            var t = new Timer();
            t.Interval = time;
            t.Tick += (s, e) =>
            {
                img.Visible = false;
                lbl.Visible = false;
                t.Stop();
            };
            t.Start();
        }

        private void Add_to_path(string path)
        {
            //avoid 'enumeration was modified'
            if (!pythonPath.Items.Contains(path))
            {
                //add to combobox
                pythonPath.Items.Add(path);
                pythonPath.SelectedItem = path;
            }
            else
            {
                //visual replacement
            }
        }

        private void console_output(object sender, DataReceivedEventArgs e)
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
                                Add_to_path(str);
                                return;
                            }
                        }
                        img2.Visible = false;
                        button5.Enabled = true;
                    }), new object[] { e.Data });
                }
            }
        }

        private void console_error(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    img2.Visible = false;
                    button5.Enabled = true;
                    this.ShowDialog();
                    tabControl1.SelectedTab = tabPage8;
                    task_error(img1, lbl1, "An error occurred. Try again.", 6000);
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        private void SelectInterpreter()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd = new OpenFileDialog();
            ofd.Filter = "Python Interpreter (*.exe)|*.exe";
            ofd.Title = "Select a valid Python interpreter";
            bool dlgTrue = ofd.ShowDialog() == DialogResult.OK;

            if (dlgTrue)
            {
                string path = System.IO.Path.GetFullPath(ofd.FileName);
                Add_to_path(path);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = false;
                img2.Visible = true;
                Packager.Commands.Command.GetPythonPath(console_output, console_error);
            }
            catch(Exception ex)
            {
                Classes.Exception.ShowError.Show("An unexpected error occurred in Netplait. Here are the details:", ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectInterpreter();
        }

        private void pyReset_Click(object sender, EventArgs e)
        {
            pyWith.Text = "CMD.exe";
            pyArgs.Text = "/K py -i \u0022($FILENAME)\u0022";
        }
    }
}