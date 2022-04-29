using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Netplait.Custom;
using System.IO;
using Netplait.Properties;
using System.Runtime.InteropServices;
using Netplait.Helpers;
using Netplait.Core.Funcs;
using Netplait.Core.MenuHelpers;
using Netplait.Core.InputBox;
using Netplait.Runner;
using ScintillaNET_FindReplaceDialog;

/// <summary>
/// PUT LICENCE ABOVE
/// ALSO ON ALL FILES
/// </summary>

//More than half of all functions have long parameters.
//Spent numerous hours working on them fast, kinda some dirty code work.
namespace Netplait
{
    public partial class MainForm : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        #region Declarations
        private const int ItemMargin = 5;
        private const float PictureHeight = 35f;

        //ProgressThread PT = new ProgressThread();
        string Def = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Netplait";
        string DefProj = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Netplait" + "\\Projects";

        const int MRUnumber = 10;
        System.Collections.Generic.Queue<string> MRUlist = new Queue<string>();
        ScintillaNET.Scintilla TextArea;
        #endregion

        #region MainForm

        Forms.Preferences pref;
        Classes.Colors.Syntax syn;
        Dialogs.Universal Universal;
        public MainForm()
        {
            InitializeComponent();

            //TableView -> Toolstrip
            Custom.TableView dropDown = new Custom.TableView();
            dropDown.Opening += new CancelEventHandler(DropDown_Opening);
            dropDown.Selector.TableSizeSelected += new TableSizeSelectedEventHandler(Selector_TableSizeSelected);
            this.TabletoolStripButton.DropDown = dropDown;

            //RunComboBox -> ToolStrip
            RunComboBox.Items.IndexOf("Python");
            this.RunComboBox.Items.Add("Python");
            this.RunComboBox.Items.Add("Associated Program");
            this.RunComboBox.SelectedItem = "Python";
            this.RunComboBox.SelectedIndex = 0;

            //Recent Files
            LoadRecentList();
            foreach (string item in MRUlist)
            {
                MenuItem fileRecent = new MenuItem(MenuMerge.Add, 0, Shortcut.None, item, RecentFile_click, null, null, null);
                menuItem26.MenuItems.Add(fileRecent);
            }

            //SplitContainer1 -> Panel1 -> Collapsed
            Realign_panel();

            //Tabcontrol
            tabControl1.AllowDrop = true;

            //Listview full column
            MarkerList.Columns[MarkerList.Columns.Count - 1].Width = -2;

            //NetTreeview -> Anti-glitch
            //https://stackoverflow.com/questions/10362988/treeview-flickering/10364283#10364283

            //NetTreeview -> Theme
            //IF SETTINGS == COLLAPSE ALL, THEN COLLAPSE ALL ON STARTUP
            try { SetWindowTheme(DirTree.Handle, "explorer", null); } catch (Exception ex) { }
            try { SetWindowTheme(ProjTree.Handle, "explorer", null); } catch (Exception ex) { }

            //Initialize Fonts
            //DirTree.Font = new Font(Properties.Settings.Default.GlobalFont, 8);
            //ProjTree.Font = new Font(Properties.Settings.Default.GlobalFont, 8);      //MAKE PROGRAM FONT (SEPARATE FONT)
            //listBox1.Font = new Font(Properties.Settings.Default.GlobalFont, 8);
            Messages.Font = new Font("Consolas", 8);
            Messages.BackColor = Color.White;
            Messages.ForeColor = Color.Gray;
            Messages.Cursor = Cursors.Default;

            //Start Page
            if (Properties.Settings.Default.StartPage == true)
            {
                TabPage StartPage = new TabPage("Start Page");
                StartPage.ImageIndex = 0;
                Custom.Tabpages.StartPage UC = new Custom.Tabpages.StartPage();
                UC.Dock = DockStyle.Fill;
                StartPage.Controls.Add(UC);
                tabControl1.TabPages.Add(StartPage);
                this.Refresh();
            }
            else { }

            //Messages Panel -> SplitContainer3
            Messages.AppendText("Initialization completed at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "\n");

            //Create Default Project Folder -> Documents/Netplait && Documents/Netplait/Projects
            try
            {
                //Create Def.
                if (!System.IO.Directory.Exists(Def))
                {
                    System.IO.Directory.CreateDirectory(Def);
                }

                //Create DefProj.
                if (!System.IO.Directory.Exists(DefProj))
                {
                    System.IO.Directory.CreateDirectory(DefProj);
                }

                //Message window
                Messages.AppendText("Checking for default Netplait folder path...\n");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not create default Netplait folder path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                //load snippets
                Messages.AppendText("Searching for Snippets path...\n");

                string path = Application.StartupPath + "\\Files\\Snippets\\";
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    string noExt = file.Substring(file.LastIndexOf("\\") + 1);
                    snipBox.Items.Add(noExt);
                }

                Helpers.Messages.SetGreen("Snippets path loaded to list.\n", Messages);
            }
            catch(Exception ex)
            {
                splitContainer3.Panel2Collapsed = false;
                Helpers.Messages.SetRed("Snippets directory could not be loaded. Details:\n" + ex.Message, Messages);
            }

            try
            {
                DirTree.ContextMenu = cFST2;
                ProjTree.ContextMenu = cFST2;
                ProjTree.Nodes.Add("No Project Loaded");

                InitializeDir();
                DirTree.NodeMouseDoubleClick += DirTree_NodeMouseDoubleClick;
                DirTree.NodeMouseClick += DirTree_NodeMouseClick;

                menuItem22.Enabled = false;
                menuItem24.Enabled = false;
                menuItem17.Enabled = false;
                menuItem18.Enabled = false;
                menuItem16.Enabled = false;
                OpenInCMDtoolStripButton.Enabled = false;
                OpenInExplorertoolStripButton.Enabled = false;

                //message window
                Messages.AppendText("Checking for active drives..\n");
                foreach (var drive in DriveInfo.GetDrives())
                {
                    Messages.AppendText("Detected: " + drive.ToString() + "\n");
                }
                Messages.AppendText("Drives successfully loaded.\n");
                Helpers.Messages.SetGreen("======== Startup Complete. ========\n", Messages);
            }
            catch (Exception ex)
            {
                splitContainer3.Panel2Collapsed = false;
                Helpers.Messages.SetRed("Error: Could not initialize one or more drives. Details:\n" + ex.Message +
                "\n\nError while initializing the directory tree. This error can happen for one of the following reasons:\n\n" + 
                "1. Your windows device has mounted a CD-Drive without contents, causing it to show up on your File Explorer. This confuses the Directory tree. " +
                "If you have an internal CD/DVD Drive, please disable it by going to Windows Device Manager » DVD/CD-ROM Drives » Right-clicking the drive, and disabling it. " + 
                "If you have an external CD/DVD Drive, try unplugging your external CD/DVD Drive.\n" + 
                "2. Netplait might not have permission to access the drives.\n", Messages);
            }
            Messages.ScrollToCaret();
            DirTree.Nodes[0].EnsureVisible();

            //Tip of the day

            //Declarations

            //Theme setting

            //temporary

            //File -> New... -> New Project...
            menuItem4.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                try
                {
                    if (Universal == null)
                    {
                        Universal = new Dialogs.Universal(Settings.Default.PythonPath.ToString());
                        Universal.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e)
                        {
                            Universal = null;
                        });
                    }
                    Universal.Show(this);
                }
                catch (Exception ex)
                {
                    Classes.Exception.ShowError.Show("An unexpected error occurred in Netplait. Here are the details:", ex.Message);
                }
            });

            btnClose.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                if(tabControl1.TabPages.Count == 1)
                {
                    for(int i = 0; i < 2; i++)
                        Usabilities.Usability.CloseDocumentEvent(this, tabControl1, EnableListener);
                    //tabControl1.Visible = false;
                }
                else
                {
                    Usabilities.Usability.CloseDocumentEvent(this, tabControl1, EnableListener);
                    //tabControl1.Visible = true;
                }
            });

            menuItem140.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                if (pref == null)
                {
                    pref = new Forms.Preferences(0);
                    pref.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e)
                    {
                        pref = null;
                    });
                }
                pref.Show(this);
            });

            menuItem42.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                if (syn == null)
                {
                    syn = new Classes.Colors.Syntax();
                    syn.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e)
                    {
                        syn = null;
                    });
                }
                syn.Show(this);
            });

            menuItem44.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                FuncController.TransposeLine(tabControl1);
            });

            toolStripButton5.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                FuncController.PreviousBookmark(tabControl1);
            });

            toolStripButton6.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                FuncController.NextBookmark(tabControl1);
            });
            openMessages.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                switch (splitContainer3.Panel2Collapsed)
                {
                    case false:
                        splitContainer3.Panel2Collapsed = true;
                        openMessages.Image = Properties.Resources.up;
                        this.Refresh();
                        break;

                    case true:
                        splitContainer3.Panel2Collapsed = false;
                        openMessages.Image = Properties.Resources.down;
                        this.Refresh();
                        break;
                }
            });

            FindReplace FindReplaceDialog = new FindReplace();
            menuItem54.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                try
                {
                    //Find
                    var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                    FindReplaceDialog.Scintilla = sci;
                    FindReplaceDialog.ShowFind();
                }
                catch(Exception ex) { }
            });
            menuItem56.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                try
                {
                    //Replace
                    var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                    FindReplaceDialog.Scintilla = sci;
                    FindReplaceDialog.ShowReplace();
                }
                catch(Exception ex) { }
            });
            menuItem57.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                try
                {
                    //Go to line
                    var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                    FindReplaceDialog.Scintilla = sci;
                    GoTo GoToDialog = new GoTo(FindReplaceDialog.Scintilla);
                    GoToDialog.ShowGoToDialog();
                }
                catch (Exception ex) { }
            });

            EnableListener();
            this.Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //check for Python Interpreter
            if (Settings.Default.CheckForPath == false)
            {
                //continue
            }
            else if (Settings.Default.CheckForPath == true)
            {
                DialogResult result;
                result = MessageBox.Show("You have to select a valid Python interpreter to run Python scripts. " +
                    "Do you want to configure one now?", "Netplait", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (pref == null)
                    {
                        pref = new Forms.Preferences(1);
                        pref.FormClosed += new FormClosedEventHandler(delegate (object o, FormClosedEventArgs fce)
                        {
                            pref = null;
                        });
                    }
                    pref.Show(this);
                }
                if (result == DialogResult.No)
                {
                    //continue
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Usabilities.Usability.FormClosingEvent(this, tabControl1, e);
        }
        #endregion

        #region Recent
        //Implementation from https://www.codeproject.com/Articles/32154/Create-a-Recent-File-List-Menu-and-Save-to-File
        public void SaveRecentFile(string path)
        {
            menuItem26.MenuItems.Clear();
            LoadRecentList();
            if (!(MRUlist.Contains(path)))
                MRUlist.Enqueue(path);
            while (MRUlist.Count > MRUnumber)
            {
                MRUlist.Dequeue();
            }
            foreach (string item in MRUlist)
            {
                MenuItem fileRecent = new MenuItem(MenuMerge.Add, 0, Shortcut.None, item, RecentFile_click, null, null, null);
                menuItem26.MenuItems.Add(fileRecent);
            }
            StreamWriter stringToWrite = new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.dat");
            foreach (string item in MRUlist)
            {
                stringToWrite.WriteLine(item, 0, 1);
            }
            stringToWrite.Flush();
            stringToWrite.Close();
        }

        private void RecentFile_click(object sender, EventArgs e)
        {
            try
            {
                //remove menuitem desc
                string path = sender.ToString().Substring(sender.ToString().LastIndexOf(": ") + 2);

                //load to treeview
                ProjTree.Nodes.Clear();
                ProjTree.Load(File.ReadAllLines(path).Last());
                projpath.Text = File.ReadAllLines(path).Last();
                ProjTree.NodeMouseDoubleClick -= tree_blank;
                ProjTree.NodeMouseDoubleClick += ProjTree_NodeMouseDoubleClick;
                ProjTree.NodeMouseClick += ProjTree_NodeMouseClick;
                tabControl2.SelectedTab = tabPage5;
                menuItem24.Enabled = true;
                menuItem22.Enabled = true;
                OpenInCMDtoolStripButton.Enabled = true;
                OpenInExplorertoolStripButton.Enabled = true;
            }
            catch(Exception ex)
            {
                string path = sender.ToString().Substring(sender.ToString().LastIndexOf(": ") + 2);
                MessageBox.Show("Could not load the project. The path to '" + path + "' does not exist.", "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentList()
        {
            MRUlist.Clear();
            try
            {
                StreamReader listToRead = new StreamReader(System.Environment.CurrentDirectory + "\\Recent.dat");
                string line;
                while ((line = listToRead.ReadLine()) != null)
                    MRUlist.Enqueue(line);
                listToRead.Close();
            }
            catch (Exception) { }
        }
        #endregion

        #region ListenerEvents
        void EnableListener()
        {
            if (tabControl1.TabPages.Count == 0)
            {
                //tabControl1.Visible = false;
                btnClose.Enabled = false;

                menuItem21.Enabled = false;
                menuItem20.Enabled = false;
                menuItem17.Enabled = false;
                menuItem18.Enabled = false;
                menuItem16.Enabled = false;

                toolStripButton9.Enabled = false;
                toolStripButton8.Enabled = false;
                toolStripButton5.Enabled = false;
                toolStripButton6.Enabled = false;

                toolStripDropDownButton1.Enabled = false;
                toolStripButton15.Enabled = false;
                toolStripButton16.Enabled = false;
                toolStripButton17.Enabled = false;
                toolStripButton18.Enabled = false;
                toolStripDropDownButton2.Enabled = false;
                toolStripButton11.Enabled = false;
                toolStripButton19.Enabled = false;
                toolStripButton20.Enabled = false;
                toolStripButton21.Enabled = false;
                TabletoolStripButton.Enabled = false;
                toolStripButton12.Enabled = false;
                saveToolStripButton1.Enabled = false;
                saveAllToolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton1.Enabled = false;
                cutToolStripButton1.Enabled = false;
                copyToolStripButton1.Enabled = false;
                pasteToolStripButton1.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton4.Enabled = false;
                startBtn.Enabled = false;
                RunComboBox.Enabled = false;
                closedocToolStripButton.Enabled = false;
                closeallToolStripButton.Enabled = false;
            }
            if (tabControl1.TabPages.Count > 0 && tabControl1.SelectedTab.Text != "Start Page")
            {
                //tabControl1.Visible = true;
                btnClose.Enabled = true;

                menuItem21.Enabled = true;
                menuItem20.Enabled = true;
                menuItem17.Enabled = true;
                menuItem18.Enabled = true;
                menuItem16.Enabled = true;

                toolStripButton9.Enabled = true;
                toolStripButton8.Enabled = true;
                toolStripButton5.Enabled = true;
                toolStripButton6.Enabled = true;

                toolStripDropDownButton1.Enabled = true;
                toolStripButton15.Enabled = true;
                toolStripButton16.Enabled = true;
                toolStripButton17.Enabled = true;
                toolStripButton18.Enabled = true;
                toolStripDropDownButton2.Enabled = true;
                toolStripButton11.Enabled = true;
                toolStripButton19.Enabled = true;
                toolStripButton20.Enabled = true;
                toolStripButton21.Enabled = true;
                TabletoolStripButton.Enabled = true;
                toolStripButton12.Enabled = true;
                saveToolStripButton1.Enabled = true;
                saveAllToolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
                toolStripButton1.Enabled = true;
                cutToolStripButton1.Enabled = true;
                copyToolStripButton1.Enabled = true;
                pasteToolStripButton1.Enabled = true;
                toolStripButton3.Enabled = true;
                toolStripButton4.Enabled = true;
                startBtn.Enabled = true;
                RunComboBox.Enabled = true;
            }
            else if (tabControl1.TabPages.Count > 0 && tabControl1.SelectedTab.Text == "Start Page")
            {
                //tabControl1.Visible = true;
                btnClose.Enabled = true;

                menuItem21.Enabled = false;
                menuItem20.Enabled = false;
                menuItem17.Enabled = false;
                menuItem18.Enabled = false;
                menuItem16.Enabled = false;

                toolStripButton9.Enabled = false;
                toolStripButton8.Enabled = false;
                toolStripButton5.Enabled = false;
                toolStripButton6.Enabled = false;

                toolStripDropDownButton1.Enabled = false;
                toolStripButton15.Enabled = false;
                toolStripButton16.Enabled = false;
                toolStripButton17.Enabled = false;
                toolStripButton18.Enabled = false;
                toolStripDropDownButton2.Enabled = false;
                toolStripButton11.Enabled = false;
                toolStripButton19.Enabled = false;
                toolStripButton20.Enabled = false;
                toolStripButton21.Enabled = false;
                TabletoolStripButton.Enabled = false;
                toolStripButton12.Enabled = false;
                saveToolStripButton1.Enabled = false;
                saveAllToolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton1.Enabled = false;
                cutToolStripButton1.Enabled = false;
                copyToolStripButton1.Enabled = false;
                pasteToolStripButton1.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton4.Enabled = false;
                startBtn.Enabled = false;
                RunComboBox.Enabled = false;
                closedocToolStripButton.Enabled = true;
                closeallToolStripButton.Enabled = true;
            }
        }
        #endregion

        #region LoadProject
        public void LoadProject()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd = new OpenFileDialog();
            ofd.Filter = "Netplait Workspace Files|*.networkspace;";
            ofd.Title = "Select a Netplait Workspace File to open";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //LOAD TO TREEVIEW
                ProjTree.Nodes.Clear();
                ProjTree.Load(File.ReadAllLines(ofd.FileName.ToString()).Last());
                projpath.Text = File.ReadAllLines(ofd.FileName.ToString()).Last();
                ProjTree.NodeMouseDoubleClick -= tree_blank;
                ProjTree.NodeMouseDoubleClick += ProjTree_NodeMouseDoubleClick;
                ProjTree.NodeMouseClick += ProjTree_NodeMouseClick;
                tabControl2.SelectedTab = tabPage5;
                menuItem24.Enabled = true;
                menuItem22.Enabled = true;
                OpenInCMDtoolStripButton.Enabled = true;
                OpenInExplorertoolStripButton.Enabled = true;

                //RECENT FILES
                SaveRecentFile(ofd.FileName);
            }
        }
        #endregion

        #region Functions(NetTree)
        private void tree_blank(object sender, TreeNodeMouseClickEventArgs e)
        {
            /* This function is used when there are no projects to load */
        }

        public void RTB_TextChanged(object sender, EventArgs e)
        {
            Usabilities.FileHandler.TextArea_TextChanged(sender, e, tabControl1, menuItem16, menuItem17, menuItem18);
            EnableListener();
        }       

        private void DirTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Usabilities.TreeviewHandler.DirectoryTree_MouseClickEvent(DirTree, secretTsb, cFolder1, cFile1, e);
            EnableListener();
        }

        private void DirTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Usabilities.TreeviewHandler.DirectoryTree_DoubleClickEvent(tabControl1, cEditor, status_lbl, e);
            EnableListener();
        }       

        private void ProjTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Usabilities.TreeviewHandler.ProjectTree_MouseClickEvent(ProjTree, projpath, secretTsb, cFolder, cFile, cProject, e);
            EnableListener();
        }

        private void ProjTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Usabilities.TreeviewHandler.ProjectTree_DoubleClickEvent(tabControl1, projpath, secretTsb, cEditor, status_lbl, e);
            EnableListener();
        }
        #endregion

        //#region Controls

        private string Path(int type, TreeView Tree, ToolStripStatusLabel tssl)
        {
            if (type == 1)
            {
                TreeNode CurrentNode = Tree.SelectedNode;
                string toremove = tssl.Text.Substring(tssl.Text.LastIndexOf("\\") + 1);
                string outPath = tssl.Text.Substring(0, tssl.Text.Length - toremove.Length);
                string fullpath = outPath + CurrentNode.FullPath.ToString();
                return fullpath;
            }
            else if (type == 2)
                return Tree.SelectedNode.FullPath.ToString();
            else
                return null;
        }

        private void Realign_panel()
        {
            bool IsCollapsed = Properties.Settings.Default.WorkspaceCollapsed;
            bool Right2Left = Properties.Settings.Default.RightToLeft;
            if (IsCollapsed == false)
            {
                //if panel1 is not collapsed
                splitContainer1.Panel1Collapsed = false;
                if (Right2Left)
                {
                    //if panel1 is aligned right
                    splitContainer1.RightToLeft = RightToLeft.Yes;
                    splitContainer1.Panel1.RightToLeft = RightToLeft.No;
                    splitContainer1.Panel2.RightToLeft = RightToLeft.No;
                    openWorkspace.Visible = false;
                    openWorkspace1.Visible = true;
                    openWorkspace1.Image = Properties.Resources.right;
                }
                else
                {
                    //if panel1 is aligned left
                    openWorkspace.Visible = true;
                    openWorkspace1.Visible = false;
                    openWorkspace.Image = Properties.Resources.left;
                }
            }
            else if (IsCollapsed)
            {
                //if panel1 is collapsed
                splitContainer1.Panel1Collapsed = true;
                if (Right2Left)
                {
                    //if panel1 is aligned right
                    splitContainer1.RightToLeft = RightToLeft.Yes;
                    splitContainer1.Panel1.RightToLeft = RightToLeft.No;
                    splitContainer1.Panel2.RightToLeft = RightToLeft.No;
                    openWorkspace.Visible = false;
                    openWorkspace1.Visible = true;
                    openWorkspace1.Image = Properties.Resources.left;
                }
                else
                {
                    //if panel1 is aligned left
                    openWorkspace.Visible = true;
                    openWorkspace1.Visible = false;
                    openWorkspace.Image = Properties.Resources.right;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Netplait 1.1 [" + tabControl1.SelectedTab.Text + "]";
                if (tabControl1.SelectedTab.Text != "Start Page")
                {
                    Helpers.Margins.CountBookmarks(tabControl1, bmLbl);
                }
                else
                {
                    bmLbl.Text = "-";
                }

                EnableListener();
            }
            catch (Exception ex) { this.Text = "Netplait 1.1"; }
        }

        #region MainMenu
        //File -> New... -> New Project...
        private void menuItem4_Click(object sender, EventArgs e)
        {
            //Its in the public mainform because cannot carry parameter
        }

        //File -> New... -> Blank File
        private void menuItem6_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$BLANK.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(5, false, true, "Untitled", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> New... -> Text File
        private void menuItem13_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$TEXT.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(5, false, true, "Untitled.txt", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> New... -> Python File
        private void menuItem9_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$PYTHON.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(1, false, true, "Untitled.py", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> New... -> HTML File
        private void menuItem10_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$HTML.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(2, false, true, "Untitled.html", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> New... -> CSS File
        private void menuItem11_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$CSS.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(3, false, true, "Untitled.css", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> New... -> JavaScript File
        private void menuItem12_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$JS.txt";
            string template = File.ReadAllText(pathToTemp);

            Usabilities.FileHandler.New_Universal(4, false, true, "Untitled.js", template, "", status_lbl, tabControl1, cEditor);
            EnableListener();
        }

        //File -> Open File
        private void menuItem7_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.OpenFileEvent(tabControl1, cEditor, status_lbl);
            EnableListener();
        }

        //File -> Open Existing Project
        private void menuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                LoadProject();
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("An error has occurred while importing the project.",
                    "Netplait does not recognize the specified file. " +
                    "This error can happen for one of the following reasons:\n\n" +
                    "1. The specified file is not a valid Netplait workspace project.\n" +
                    "2. The specified file has been corrupted or has been moved or copied from the previous directory. " +
                    "If you think this is the case, try switching the workspace file instead. " +
                    "Click on 'Project' » 'Switch Workspace File' from the menubar. Then set the new directory of your project.");
            }
        }

        //File -> Save
        private void menuItem16_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.SaveFileEvent(this, tabControl1, status_lbl);
            EnableListener();
        }

        //File -> Save As
        private void menuItem17_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.SaveAsFileEvent(this, tabControl1);
            EnableListener();
        }

        //File -> Save All        
        private void menuItem18_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.SaveAllEvent(this, tabControl1, status_lbl, EnableListener);
        }

        //File -> Close Document
        private void menuItem20_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.CloseDocumentEvent(this, tabControl1, EnableListener);
        }

        //File -> Close All
        private void menuItem21_Click(object sender, EventArgs e)
        {
            Usabilities.Usability.CloseAllEvent(this, tabControl1, status_lbl, EnableListener);
        }

        //File -> Close Project
        private void menuItem22_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem[] menu = { menuItem22, menuItem24 };
                ToolStripButton[] ts = { OpenInCMDtoolStripButton, OpenInExplorertoolStripButton };
                Core.DirectoryViewer.ProjectControl.CloseProject_Event(ProjTree, projpath, ProjTree_NodeMouseDoubleClick, tree_blank, menu, ts);
            }
            catch(Exception ex)
            {
                Helpers.Messages.SetRed("Unexpected Error unloading project:\n\n" + ex.Message, Messages);
            }
        }

        //File -> Delete Project
        private void menuItem24_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem[] menu = { menuItem22, menuItem24 };
                ToolStripButton[] ts = { OpenInCMDtoolStripButton, OpenInExplorertoolStripButton };
                Action CloseDoc = () => Usabilities.Usability.CloseDocumentEvent(this, tabControl1, EnableListener);
                Core.DirectoryViewer.ProjectControl.DeleteProject_Event(ProjTree, tabControl1, projpath, CloseDoc, secretTsb,
                    ProjTree_NodeMouseDoubleClick, tree_blank, menu, ts);
            }
            catch (Exception ex)
            {
                Helpers.Messages.SetRed("Netplait has detected no project to delete.\n\n" + ex.Message, Messages);
                Classes.Exception.ShowError.Show("Netplait has detected no project to delete.", ex.Message);
            }
        }

        //File -> Clear Recent List
        private void menuItem27_Click(object sender, EventArgs e)
        {
            try
            {
                //clear all recent on file
                string path = System.Environment.CurrentDirectory + "\\Recent.dat";
                System.IO.File.WriteAllText(path, string.Empty);

                //clear all menuitems
                menuItem26.MenuItems.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The operation could not be completed.\n\n" + ex.Message, "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //File -> Restart
        private void menuItem30_Click(object sender, EventArgs e)
        {

        }

        //File -> Exit
        private void menuItem29_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region cEditor
        private void menuItem55_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Undo(tabControl1);
            }
            catch(Exception ex)
            {

            }
        }

        private void menuItem46_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Redo(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem47_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Cut(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem236_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Copy(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem237_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Paste(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem246_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Delete(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem249_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.SelectLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem247_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.SelectAll(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem260_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.TransposeLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem259_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.DuplicateLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem253_Click(object sender, EventArgs e)
        {
            try
            {
                //Run file
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem238_Click(object sender, EventArgs e)
        {
            try
            {
                Action Cbmarks = () => Helpers.Margins.CountBookmarks(tabControl1, bmLbl);
                FuncController.ToggleBookmark(tabControl1, Cbmarks);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem258_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.NextBookmark(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem257_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.PreviousBookmark(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        FindReplace FindReplaceDialog = new FindReplace();
        private void menuItem255_Click(object sender, EventArgs e)
        {
            try
            {
                var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                FindReplaceDialog.Scintilla = sci;
                FindReplaceDialog.ShowFind();
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem254_Click(object sender, EventArgs e)
        {
            try
            {
                var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                FindReplaceDialog.Scintilla = sci;
                FindReplaceDialog.ShowReplace();
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem251_Click(object sender, EventArgs e)
        {
            try
            {
                var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                FindReplaceDialog.Scintilla = sci;
                GoTo GoToDialog = new GoTo(FindReplaceDialog.Scintilla);
                GoToDialog.ShowGoToDialog();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region toolstrips
        private void RefreshtoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl2.SelectedTab.Text == "Local Filesystem")
                {
                    Project.OpenExpansion(DirTree, InitializeDir);
                }

                if (tabControl2.SelectedTab.Text == "Workspace Filesystem")
                {
                    Project.OpenExpansion(ProjTree, InitializeProj);
                }
            }
            catch (Exception ex) { }
        }

        private void openWorkspace_ButtonClick(object sender, EventArgs e)
        {
            switch (splitContainer1.Panel1Collapsed)
            {
                case false:
                    splitContainer1.Panel1Collapsed = true;
                    openWorkspace.ForeColor = Color.Gray;
                    openWorkspace.Image = Properties.Resources.right;
                    Properties.Settings.Default.WorkspaceCollapsed = true;
                    Properties.Settings.Default.Save();
                    this.Refresh();
                    break;

                case true:
                    splitContainer1.Panel1Collapsed = false;
                    openWorkspace.ForeColor = Color.Black;
                    openWorkspace.Image = Properties.Resources.left;
                    Properties.Settings.Default.WorkspaceCollapsed = false;
                    Properties.Settings.Default.Save();
                    this.Refresh();
                    break;
            }
        }

        private void openWorkspace1_ButtonClick(object sender, EventArgs e)
        {
            switch (splitContainer1.Panel1Collapsed)
            {
                case false:
                    splitContainer1.Panel1Collapsed = true;
                    openWorkspace1.ForeColor = Color.Gray;
                    openWorkspace1.Image = Properties.Resources.left;
                    Properties.Settings.Default.WorkspaceCollapsed = true;
                    Properties.Settings.Default.Save();
                    this.Refresh();
                    break;

                case true:
                    splitContainer1.Panel1Collapsed = false;
                    openWorkspace1.ForeColor = Color.Black;
                    openWorkspace1.Image = Properties.Resources.right;
                    Properties.Settings.Default.WorkspaceCollapsed = false;
                    Properties.Settings.Default.Save();
                    this.Refresh();
                    break;
            }
        }
        #endregion

        #region cFolder
        //Add -> Existing Files...
        private void menuItem160_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.AddExistingFiles(ProjTree, fullpath, InitializeProj);
            }
            catch(Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Add -> Existing Folders...
        private void menuItem262_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.AddExistingFolders(ProjTree, fullpath, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Add -> New Folder...
        private void menuItem226_Click(object sender, EventArgs e)
        {
            string fullpath = Path(1, ProjTree, projpath);
            Project.NewFile(1, false, fullpath, "", ProjTree, secretTsb, InitializeProj);
        }

        //Add -> Blank File...
        private void menuItem162_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$BLANK.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(2, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Add -> Text File...
        private void menuItem163_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$TEXT.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(3, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Add -> Python File...
        private void menuItem165_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$PYTHON.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(4, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Add -> HTML File...
        private void menuItem166_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$HTML.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(5, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Add -> CSS File...
        private void menuItem167_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$CSS.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(6, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Add -> JavaScript File...
        private void menuItem168_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$JS.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(1, ProjTree, projpath);

            Project.NewFile(7, true, fullpath, template, ProjTree, secretTsb, InitializeProj);
        }

        //Open In Explorer
        private void menuItem171_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.OpenInExplorer(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Command Prompt Here
        private void menuItem172_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.CommandPrompt(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Copy
        private void menuItem174_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.CopyPath(ProjTree, fullpath, secretTsb);
            }
            catch(Exception ex)
            {

            }
        }

        //Paste
        private void menuItem175_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.PasteFolder(ProjTree, fullpath, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Delete Folder
        private void menuItem177_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.DeletePath(1, ProjTree, fullpath, secretTsb, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Rename Folder
        private void menuItem178_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.RenameFolder(ProjTree, fullpath, secretTsb, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }
        #endregion

        #region FileSystems
        public void InitializeDir()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                DirTree.Load(System.IO.Path.GetFullPath(drive.ToString()));
            }
        }

        public void InitializeProj()
        {
            ProjTree.Load(projpath.Text);
            menuItem24.Enabled = true;
            menuItem22.Enabled = true;
            OpenInCMDtoolStripButton.Enabled = true;
            OpenInExplorertoolStripButton.Enabled = true;
        }
        #endregion

        #region TableView
        void Selector_TableSizeSelected(object sender, TableSizeEventArgs e)
        {
            MessageBox.Show(String.Format("Selected: {0}x{1}", e.SelectedSize.Width, e.SelectedSize.Height), Application.ProductName);
        }

        void DropDown_Opening(object sender, CancelEventArgs e)
        {
            Custom.TableView c = (Custom.TableView)this.TabletoolStripButton.DropDown;
            c.Selector.SelectedSize = new Size(0, 0);
            c.Selector.VisibleRange = new Size(5, 4);
        }
        #endregion

        #region cFile
        private void OpenFile(TreeView Tree, string fullpath)
        {
            TreeNode CurrentNode = Tree.SelectedNode;
            secretTsb.Text = fullpath.ToString();
            string noExt = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
            FileAttributes attr = File.GetAttributes(fullpath.ToString());

            bool found = false;
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (noExt.Equals(tab.Text) && !fullpath.Equals(tab.Name)) { }
                else if (noExt.Equals(tab.Text) && fullpath.Equals(tab.Name))
                {
                    tabControl1.SelectedTab = tab;
                    found = true;
                }
            }
            if (!found)
            {
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) { }
                else
                {
                    //PYTHON
                    if (FileTypes.FileExtensionHandler.IsPythonFile(CurrentNode.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(1, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //HTML
                    else if (FileTypes.FileExtensionHandler.IsHTMLFile(CurrentNode.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(2, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //CSS
                    else if (FileTypes.FileExtensionHandler.IsCSSFile(CurrentNode.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(3, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //Js
                    else if (FileTypes.FileExtensionHandler.IsJSFile(CurrentNode.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(4, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //Blext
                    else
                    {
                        Usabilities.FileHandler.New_Universal(5, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                }
            }
        }

        private void RenameFile(TreeView Tree, string fullpath)
        {
            string upperNode = Tree.Nodes[0].Text.ToString();
            secretTsb.Text = fullpath.ToString();

            string value = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
            //string NewName = fullpath.ToString().Replace(value.ToString(), "");
            string NewName = fullpath.Substring(0, fullpath.Length - value.Length);
            if (InputBox.Show("Enter Name", "Rename File:", ref value, null) == DialogResult.OK)
            {
                string CreatedFile = NewName.ToString() + "\\" + value.ToString();
                if (value == upperNode)
                {
                    MessageBox.Show("Forbidden entry. Try another name other than the Project name.",
                        "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!File.Exists(CreatedFile))
                    {
                        File.Move(fullpath, CreatedFile);
                    }
                    else
                    {
                        CreatedFile = CreatedFile.Replace(@"\\", @"\");
                        if (fullpath.ToLower() == CreatedFile.ToLower())
                            File.Move(fullpath, CreatedFile);
                        else
                            MessageBox.Show("Another file with the same name already exists.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string noExt = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
                string Ext = noExt.ToString() + "*";
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (noExt.Equals(tab.Text) && fullpath.Equals(tab.Name) || Ext.Equals(tab.Text) && fullpath.Equals(tab.Name))
                    {
                        tabControl1.SelectedTab = tab;
                        DialogResult result;
                        result = MessageBox.Show("The file has been renamed, do you want to reload the file?",
                            "Netplait", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            //Remove the outdated one
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                            //Reload the new one
                            if (FileTypes.FileExtensionHandler.IsPythonFile(value))
                            {
                                Usabilities.FileHandler.New_Universal(1, true, false, value, CreatedFile, CreatedFile, status_lbl, tabControl1, cEditor);
                            }
                            else if (FileTypes.FileExtensionHandler.IsHTMLFile(value))
                            {
                                Usabilities.FileHandler.New_Universal(2, true, false, value, CreatedFile, CreatedFile, status_lbl, tabControl1, cEditor);
                            }
                            else if (FileTypes.FileExtensionHandler.IsCSSFile(value))
                            {
                                Usabilities.FileHandler.New_Universal(3, true, false, value, CreatedFile, CreatedFile, status_lbl, tabControl1, cEditor);
                            }
                            else if (FileTypes.FileExtensionHandler.IsJSFile(value))
                            {
                                Usabilities.FileHandler.New_Universal(4, true, false, value, CreatedFile, CreatedFile, status_lbl, tabControl1, cEditor);
                            }
                            else
                            {
                                Usabilities.FileHandler.New_Universal(5, true, false, value, CreatedFile, CreatedFile, status_lbl, tabControl1, cEditor);
                            }
                            EnableListener();
                        }
                        else if (result == DialogResult.No) { }
                    }
                }
                Project.OpenExpansion(ProjTree, InitializeProj);
            }
    }

        //Open File
        private void menuItem179_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                OpenFile(ProjTree, fullpath);
            }
            catch(Exception ex)
            {
                Classes.Exception.ShowError.Show("An unexpected error occurred in Netplait. Here are the details:", ex.Message);
            }
        }

        //Execute File
        private void menuItem180_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.ExecuteFile(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Copy
        private void menuItem182_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.CopyPath(ProjTree, fullpath, secretTsb);
            }
            catch (Exception ex)
            {

            }
        }

        //Delete
        private void menuItem183_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                Project.DeletePath(2, ProjTree, fullpath, secretTsb, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Rename
        private void menuItem184_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(1, ProjTree, projpath);
                RenameFile(ProjTree, fullpath);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        #endregion

        #region cProject
        //Add -> Existing Files...
        private void menuItem186_Click(object sender, EventArgs e)
        {
            menuItem160_Click(sender, e);
        }

        //Add -> Existing Folders...
        private void menuItem187_Click(object sender, EventArgs e)
        {
            menuItem262_Click(sender, e);
        }

        //Add -> New Folder...
        private void menuItem225_Click(object sender, EventArgs e)
        {
            menuItem226_Click(sender, e);
        }

        //Add -> Blank File...
        private void menuItem188_Click(object sender, EventArgs e)
        {
            menuItem162_Click(sender, e);
        }

        //Add -> Text File...
        private void menuItem189_Click(object sender, EventArgs e)
        {
            menuItem163_Click(sender, e);
        }

        //Add -> Python File...
        private void menuItem191_Click(object sender, EventArgs e)
        {
            menuItem165_Click(sender, e);
        }

        //Add -> HTML File...
        private void menuItem192_Click(object sender, EventArgs e)
        {
            menuItem166_Click(sender, e);
        }

        //Add -> CSS File...
        private void menuItem193_Click(object sender, EventArgs e)
        {
            menuItem167_Click(sender, e);
        }

        //Add -> JavaScript File...
        private void menuItem194_Click(object sender, EventArgs e)
        {
            menuItem168_Click(sender, e);
        }

        //Open In Explorer
        private void menuItem197_Click(object sender, EventArgs e)
        {
            menuItem171_Click(sender, e);
        }

        //Command Prompt Here
        private void menuItem198_Click(object sender, EventArgs e)
        {
            menuItem172_Click(sender, e);
        }

        //Run App
        private void menuItem200_Click(object sender, EventArgs e)
        {
            //RUN APP
        }

        //Close Project
        private void menuItem202_Click(object sender, EventArgs e)
        {
            menuItem22_Click(sender, e);
        }

        //Delete Project
        private void menuItem203_Click(object sender, EventArgs e)
        {
            menuItem24_Click(sender, e);
        }
        #endregion

        #region cFST2
        //New Project...
        private void menuItem204_Click(object sender, EventArgs e)
        {
            menuItem4_Click(sender, e);
        }

        //Open Existing Project...
        private void menuItem206_Click(object sender, EventArgs e)
        {
            menuItem8_Click(sender, e);
        }

        //Refresh
        private void menuItem207_Click(object sender, EventArgs e)
        {
            RefreshtoolStripButton_Click(sender, e);
        }
        #endregion

        #region cFolder1
        //Add -> Existing Files...
        private void menuItem169_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.AddExistingFiles(DirTree, fullpath, InitializeDir);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Add -> Existing Folders...
        private void menuItem264_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.AddExistingFolders(DirTree, fullpath, InitializeDir);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Add -> New Folder...
        private void menuItem224_Click(object sender, EventArgs e)
        {
            string fullpath = Path(2, DirTree, projpath);
            Project.NewFile(1, false, fullpath, "", DirTree, secretTsb, InitializeDir);
        }

        //Add -> Blank File...
        private void menuItem215_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$BLANK.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(2, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Add -> Text File...
        private void menuItem216_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$TEXT.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(3, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Add -> Python File...
        private void menuItem218_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$PYTHON.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(4, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Add -> HTML File...
        private void menuItem219_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$HTML.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(5, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Add -> CSS File...
        private void menuItem220_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$CSS.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(6, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Add -> JavaScript File...
        private void menuItem221_Click(object sender, EventArgs e)
        {
            string pathToTemp = Application.StartupPath + "\\Files\\$JS.txt";
            string template = File.ReadAllText(pathToTemp);
            string fullpath = Path(2, DirTree, projpath);

            Project.NewFile(7, true, fullpath, template, DirTree, secretTsb, InitializeDir);
        }

        //Open In Explorer
        private void menuItem228_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.OpenInExplorer(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Command Prompt Here
        private void menuItem229_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.CommandPrompt(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Copy
        private void menuItem231_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.CopyPath(DirTree, fullpath, secretTsb);
            }
            catch (Exception ex)
            {

            }
        }

        //Paste
        private void menuItem232_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.PasteFolder(DirTree, fullpath, InitializeDir);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Delete Folder
        private void menuItem234_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.DeletePath(1, DirTree, fullpath, secretTsb, InitializeDir);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Rename Folder
        private void menuItem235_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.RenameFolder(DirTree, fullpath, secretTsb, InitializeDir);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }
        #endregion

        #region cFile1
        //Open File
        private void menuItem208_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                OpenFile(DirTree, fullpath);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("An unexpected error occurred in Netplait. Here are the details:", ex.Message);
            }
        }

        //Execute File
        private void menuItem209_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.ExecuteFile(fullpath, secretTsb);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Copy
        private void menuItem211_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.CopyPath(DirTree, fullpath, secretTsb);
            }
            catch (Exception ex)
            {

            }
        }

        //Delete
        private void menuItem212_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                Project.DeletePath(2, DirTree, fullpath, secretTsb, InitializeProj);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }

        //Rename
        private void menuItem213_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = Path(2, DirTree, projpath);
                RenameFile(DirTree, fullpath);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("The operation could not be completed.", ex.Message);
            }
        }
        #endregion

        #region snipBox
        private void snipBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\Files\\Snippets\\";
                string fullpath = path + snipBox.SelectedItem.ToString();
                string noExt = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);

                bool found = false;
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (noExt.Equals(tab.Text) && !fullpath.Equals(tab.Name)) { }
                    else if (noExt.Equals(tab.Text) && fullpath.Equals(tab.Name))
                    {
                        tabControl1.SelectedTab = tab;
                        found = true;
                    }
                }
                if (!found)
                {
                    //PYTHON
                    if (FileTypes.FileExtensionHandler.IsPythonFile(fullpath.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(1, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //HTML
                    else if (FileTypes.FileExtensionHandler.IsHTMLFile(fullpath.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(2, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //CSS
                    else if (FileTypes.FileExtensionHandler.IsCSSFile(fullpath.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(3, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //Js
                    else if (FileTypes.FileExtensionHandler.IsJSFile(fullpath.ToString()))
                    {
                        Usabilities.FileHandler.New_Universal(4, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                    //Blext
                    else
                    {
                        Usabilities.FileHandler.New_Universal(5, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, cEditor);
                    }
                }
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Netplait has detected an error opening the file.", ex.Message);
            }
        }

        private void snipBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            //https://www.fluxbytes.com/csharp/how-to-draw-listbox-items-with-alternative-background-colors/
            bool isSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            if (e.Index > -1)
            {
                Color color = isSelected ? SystemColors.Highlight : e.Index % 2 == 0 ? Color.FromArgb(244, 245, 249) : Color.White;

                SolidBrush backgroundBrush = new SolidBrush(color);
                SolidBrush textBrush = new SolidBrush(e.ForeColor);
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
                e.Graphics.DrawString(snipBox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);

                backgroundBrush.Dispose();
                textBrush.Dispose();
            }
            e.DrawFocusRectangle();
        }
        #endregion

        private void tt_Click(object sender, EventArgs e)
        {
            Settings.Default.CheckForPath = true;
            Settings.Default.Save();
        }

        #region Running Options
        private void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Action Save = () => menuItem16_Click(sender, e);
                Action Out = () =>
                {

                    string time = DateTime.Now.ToString("HH:mm:ss");
                    Messages.AppendText(time +" : Running the file, '" + tabControl1.SelectedTab.Text + "', with " + 
                        RunComboBox.SelectedItem.ToString());
                };
                string proc = Properties.Settings.Default.PythonRunWith.ToString();
                string args = Properties.Settings.Default.PythonRunArgs.ToString();
                CurrentFile.Run(tabControl1, RunComboBox, proc, args, Save, Out);
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Could not run the file. Here are the details:", ex.Message);
            }
        }
        #endregion

        #region Edit
        //Undo
        private void menuItem34_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Undo(tabControl1);
            }
            catch(Exception ex)
            {

            }
        }

        //Redo
        private void menuItem35_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Redo(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Cut
        private void menuItem37_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Cut(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Copy
        private void menuItem38_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Copy(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Paste
        private void menuItem39_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Paste(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Delete
        private void menuItem261_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Delete(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Select line
        private void menuItem239_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.SelectLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Select all
        private void menuItem41_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.SelectAll(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Clear selection
        private void menuItem240_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.ClearSelection(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Transpose line
        private void menuItem44_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.TransposeLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Duplicate line
        private void menuItem45_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.DuplicateLine(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Toggle bookmark
        private void menuItem49_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.ToggleBookmark(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Indent
        private void menuItem51_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Indent(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Outdent
        private void menuItem241_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Outdent(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Uppercase
        private void menuItem129_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Uppercase(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Lowercase
        private void menuItem242_Click(object sender, EventArgs e)
        {
            try
            {
                FuncController.Lowercase(tabControl1);
            }
            catch (Exception ex)
            {

            }
        }

        //Comment line
        private void menuItem244_Click(object sender, EventArgs e)
        {
            var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            Helpers.Comments.Comment(sci, tabControl1);
        }

        //Uncomment line
        private void menuItem245_Click(object sender, EventArgs e)
        {
            var sci = (ScintillaNET.Scintilla)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            Helpers.Comments.Uncomment(sci, tabControl1);
        }
        #endregion
    }
}
//There are some controls that are still visible when no tabpage is open you should include check them!
//I GUESS NOW YOU COMPLETE THE MENU ITEM FUNCTIONS!!