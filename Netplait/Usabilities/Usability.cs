using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Usabilities
{
    public class Usability
    {
        public static ScintillaNET.Scintilla TextArea;

        /// <summary>
        /// Main method for opening a file with specified filters and displaying the contents to the tabcontrol
        /// </summary>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the file contents</param>
        /// <param name="status_lbl">The ToolStripStatusLabel responsible to react to Update events</param>
        public static void OpenFileEvent(TabControl tabControl1, ContextMenu contextMenu, ToolStripStatusLabel status_lbl)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd = new OpenFileDialog();
                ofd.Filter = "All Files (*.*)|*.*|" +
                "HTML Files (*.html)(*.htm)(*.shtml)(*.shtm)(*.xhtml)|*.html;*.htm;*.shtml;*.shtm;*.xhtml|" +
                "CSS Files (*.css)|*.css|" +
                "JavaScript Files (*.js)|*.js|" +
                "Python Files (*.py)(*.pyw)(*.py3)(*.pyi)(*.pyx)|*.py;*.pyw;*.py3;*.pyi;*.pyx";
                ofd.Title = "Select a File to open";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fullpath = System.IO.Path.GetFullPath(ofd.FileName);
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
                            Usabilities.FileHandler.New_Universal(1, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //HTML
                        else if (FileTypes.FileExtensionHandler.IsHTMLFile(fullpath.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(2, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //CSS
                        else if (FileTypes.FileExtensionHandler.IsCSSFile(fullpath.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(3, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //Js
                        else if (FileTypes.FileExtensionHandler.IsJSFile(fullpath.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(4, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //Blext
                        else
                        {
                            Usabilities.FileHandler.New_Universal(5, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Netplait has detected an error opening the file.", ex.Message);
            }
        }

        /// <summary>
        /// Main method for saving a file with specified filters and getting the path each time
        /// </summary>
        /// <param name="form">The Form name to update statuses</param>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the file contents</param>
        /// <param name="status_lbl">The ToolStripStatusLabel responsible to react to Update events</param>
        public static void SaveFileEvent(Form form, TabControl tabControl1, ToolStripStatusLabel status_lbl)
        {
            try
            {
                var TextArea = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                string tbName = tabControl1.SelectedTab.Name.ToString();

                if (tabControl1.TabPages.Count > 0)
                {
                    if (tbName != "" || tbName != string.Empty)
                    {
                        File.WriteAllText(tbName, TextArea.Text);

                        //If tab contains *, remove it
                        string Newtabname = tabControl1.SelectedTab.Text.Remove(tabControl1.SelectedTab.Text.Length - 1);
                        if (tabControl1.SelectedTab.Text.Contains("*"))
                            tabControl1.SelectedTab.Text = Newtabname;

                        //Change title
                        string fullpath2 = tbName.Replace(@"\\", @"\");
                        form.Text = "Netplait 1.1 - " + fullpath2.ToString();
                    }
                    else
                    {
                        SaveAsFileEvent(form, tabControl1);
                    }                    
                }
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Unable to save the file. The system could not find the path specified.", ex.Message);
            }
        }

        /// <summary>
        /// Main method for 'save as' event
        /// </summary>
        /// <param name="form">The Form name to update statuses</param>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the file contents</param>
        public static void SaveAsFileEvent(Form form, TabControl tabControl1)
        {
            try
            {                
                var TextArea = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                string tbName = tabControl1.SelectedTab.Name.ToString();

                SaveFileDialog sfd = default(SaveFileDialog);
                sfd = new SaveFileDialog();
                sfd.Filter = "All Files (*.*)|*.*|" +
                "HTML Files (*.html)(*.htm)(*.shtml)(*.shtm)(*.xhtml)|*.html;*.htm;*.shtml;*.shtm;*.xhtml|" +
                "CSS Files (*.css)|*.css|" +
                "JavaScript Files (*.js)|*.js|" +
                "Python Files (*.py)(*.pyw)(*.py3)(*.pyi)(*.pyx)|*.py;*.pyw;*.py3;*.pyi;*.pyx";
                sfd.Title = "Select a directory to save the file";
                sfd.FileName = tabControl1.SelectedTab.Text;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(tabControl1.SelectedTab.Name = System.IO.Path.GetFullPath(sfd.FileName.ToString()), TextArea.Text);
                    tabControl1.SelectedTab.Text = System.IO.Path.GetFileName(sfd.FileName.ToString());
                    form.Text = "Netplait 1.1 - " + System.IO.Path.GetFullPath(sfd.FileName.ToString());
                }
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("An error occurred. The operation could not be completed. Here are the details:", ex.Message);
            }
        }

        public static void CloseDocumentEvent(Form form, TabControl tabControl1, Action Listen)
        {
            try
            {
                if (tabControl1.TabPages.Count > 0)
                {
                    if (tabControl1.SelectedTab.Text == "Start Page")
                    {
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    }
                    else if (tabControl1.SelectedTab.Text.Contains("*"))
                    {
                        DialogResult result;
                        result = MessageBox.Show("Do you want to save changes to " + tabControl1.SelectedTab.Text + "?",
                            "Netplait", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            SaveAsFileEvent(form, tabControl1);
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                        }
                        if (result == DialogResult.No)
                        {
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        }
                        if (result == DialogResult.Cancel) { return; }
                    }
                    else
                    {
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    }
                }
                else
                {
                    Listen();
                }
                int lastTab = tabControl1.TabPages.Count - 1;
                tabControl1.SelectedIndex = lastTab;
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Netplait has detected nothing to close.", ex.Message);
            }
        }

        public static void SaveAllEvent(Form form, TabControl tabControl1, ToolStripStatusLabel status_lbl, Action Listen)
        {
            try
            {
                TabControl.TabPageCollection tabcoll = tabControl1.TabPages;
                tabControl1.SelectedIndex = 0;
                foreach (TabPage tp in tabcoll)
                {
                    tabControl1.SelectedTab = tp;
                    if (tp.Text == "Start Page")
                    {
                        //Nothing.
                    }
                    else
                    {
                        //save
                        SaveFileEvent(form, tabControl1, status_lbl);
                    }
                }
                Listen();
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Operation could not be performed.", ex.Message);
            }
        }

        public static void CloseAllEvent(Form form, TabControl tabControl1, ToolStripStatusLabel status_lbl, Action Listen)
        {
            try
            {
                if(tabControl1.TabPages.Count > 0)
                {
                    TabControl.TabPageCollection tabcoll = tabControl1.TabPages;
                    foreach (TabPage tp in tabcoll)
                    {
                        tabControl1.SelectedTab = tp;
                        if (tp.Text == "Start Page")
                        {
                            tabControl1.TabPages.Remove(tp);
                        }
                        else if (tp.Text.Contains("*"))
                        {
                            DialogResult result;
                            result = MessageBox.Show("Do you want to save changes to " + tp.Text + "?",
                                "Netplait", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                SaveFileEvent(form, tabControl1, status_lbl);
                                tabControl1.TabPages.Remove(tp);
                            }
                            if (result == DialogResult.No)
                            {
                                tabControl1.TabPages.Remove(tp);
                            }
                            if (result == DialogResult.Cancel) { return; }
                        }
                        else
                        {
                            tabControl1.TabPages.Remove(tp);
                        }
                    }
                }
                Listen();
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Operation could not be performed.", ex.Message);
            }
        }

        public static void FormClosingEvent(Form form, TabControl tabControl1, FormClosingEventArgs e)
        {
            try
            {
                if (tabControl1.TabPages.Count > 0)
                {
                    TabControl.TabPageCollection tabcoll = tabControl1.TabPages;
                    foreach(TabPage tp in tabcoll)
                    {
                        tabControl1.SelectedTab = tp;
                        if (tp.Text == "Start Page")
                        {
                            tabControl1.TabPages.Remove(tp);
                        }
                        else if (tp.Text.Contains("*"))
                        {
                            var TextArea = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                            string tbName = tp.Name.ToString();

                            DialogResult result;
                            if (tbName != "" || tbName != string.Empty)
                            {
                                result = MessageBox.Show("Do you want to save changes to " + tp.Text + "?",
                                "Netplait", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    File.WriteAllText(tbName, TextArea.Text);
                                    form.Text = "Netplait 1.1 - " + tbName;
                                    tabControl1.TabPages.Remove(tp);
                                }
                                if (result == DialogResult.No)
                                {
                                    tabControl1.TabPages.Remove(tp);
                                }
                                if (result == DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                    tabControl1.Select();
                                    break;
                                }
                            }
                            else
                            {
                                result = MessageBox.Show("Do you want to save changes to " + tp.Text + "?",
                                "Netplait", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    SaveFileDialog sfd = default(SaveFileDialog);
                                    sfd = new SaveFileDialog();
                                    sfd.Filter = "All Files (*.*)|*.*|" +
                                    "HTML Files (*.html)(*.htm)(*.shtml)(*.shtm)(*.xhtml)|*.html;*.htm;*.shtml;*.shtm;*.xhtml|" +
                                    "CSS Files (*.css)|*.css|" +
                                    "JavaScript Files (*.js)|*.js|" +
                                    "Python Files (*.py)(*.pyw)(*.py3)(*.pyi)(*.pyx)|*.py;*.pyw;*.py3;*.pyi;*.pyx";
                                    sfd.Title = "Select a directory to save the file";

                                    if (sfd.ShowDialog() == DialogResult.OK)
                                    {
                                        File.WriteAllText(tp.Name = System.IO.Path.GetFullPath(sfd.FileName.ToString()), TextArea.Text);
                                        form.Text = "Netplait 1.1 - " + System.IO.Path.GetFullPath(sfd.FileName);
                                    }

                                    tabControl1.TabPages.Remove(tp);
                                }
                                if (result == DialogResult.No)
                                {
                                    tabControl1.TabPages.Remove(tp);
                                }
                                if (result == DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                    tabControl1.Select();
                                    break;
                                }
                            }
                        }
                        else { tabControl1.TabPages.Remove(tp); }
                    }
                }
            }
            catch (Exception ex)
            {
                Classes.Exception.ShowError.Show("Unable to save the file. The system could not find the path specified.", ex.Message);
            }
        }
    }
}
