using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Netplait.Core.InputBox;
using Netplait.Core.Project;
using Netplait.Core.MenuHelpers;
using Netplait.Core.Loaders;
using System.Diagnostics;

namespace Netplait.Core.MenuHelpers
{
    public class Project
    {
        public static void AddExistingFiles(TreeView Tree, string fullpath, Action Initialize)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "All Files (*.*)|*.*";
            ofd.Title = "Select existing File(s) to add to project";
            bool dlgTrue = ofd.ShowDialog() == DialogResult.OK;

            if (dlgTrue)
            {
                foreach(string filePath in ofd.FileNames)
                {
                    string upperNode = Tree.Nodes[0].Text.ToString();
                    string fileNm = filePath.Substring(filePath.LastIndexOf("\\") + 1);

                    if (fileNm.ToLower() == upperNode.ToLower())
                    {
                        MessageBox.Show("Forbidden entry. The file '" + fileNm + "' has the same name as the Project name. " +
                            "Aborting import of '" + fileNm + "'.", "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (!File.Exists(fullpath + "\\" + fileNm))
                            File.Copy(filePath, fullpath + "\\" + fileNm);
                        else
                            MessageBox.Show("'" + fileNm + "' already exists in the destination directory. Cancelling copy of '" + fileNm + "'",
                                "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                OpenExpansion(Tree, Initialize);
            }
        }

        static Loading load = new Loading();
        public static void AddExistingFolders(TreeView Tree, string fullpath, Action Initialize)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selPath = fbd.SelectedPath;
                string upperNode = Tree.Nodes[0].Text.ToString();
                string fileNm = selPath.Substring(selPath.LastIndexOf("\\") + 1);

                if (fileNm.ToLower() == upperNode.ToLower())
                {
                    MessageBox.Show("Forbidden entry. The folder '" + fileNm + "' has the same name as the Project name. " +
                        "Aborting import of '" + fileNm + "'.", "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!Directory.Exists(fullpath + "\\" + fileNm))
                    {
                        //show progress form
                        load.Start();

                        //copy dir
                        CopyFiles.DirectoryCopy(selPath, fullpath + "\\" + fileNm, true);
                    }
                    else
                        MessageBox.Show("'" + fileNm + "' already exists in the destination directory. Cancelling copy of '" + fileNm + "'",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //restore default
                load.Stop();

                OpenExpansion(Tree, Initialize);
            }
        }

        public static void NewFile(int type, bool FromFile, string fullpath, string contents, TreeView Tree, ToolStripTextBox tsb, Action Initialize)
        {
            try
            {
                tsb.Text = fullpath.ToString();

                //New Folder
                if (type == 1) {
                    string value = "New Folder";
                    bool inputVal = InputBox.InputBox.Show("Enter Folder Name", "New Folder Name:", ref value, null) == DialogResult.OK;
                    CreateFolder(Tree, Initialize, fullpath, value, inputVal);
                }
                //Blank File
                else if (type == 2) {
                    string value = "Untitled";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New Blank Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
                //Text File
                else if (type == 3) {
                    string value = "Untitled.txt";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New Text Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
                //Python File
                else if (type == 4) {
                    string value = "Untitled.py";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New Python Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
                //HTML File
                else if (type == 5) {
                    string value = "Untitled.html";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New HTML Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
                //CSS File
                else if (type == 6) {
                    string value = "Untitled.css";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New CSS Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
                //JS File
                else if (type == 7) {
                    string value = "Untitled.js";
                    bool inputVal = InputBox.InputBox.Show("Enter Filename", "New JavaScript Filename:", ref value, null) == DialogResult.OK;

                    if (contents.Contains("($USER)")) { contents = contents.Replace("($USER)", Environment.UserName); }
                    if (FromFile == true)
                        CreateFile(Tree, Initialize, contents, fullpath, value, inputVal);
                    else
                        CreateFile(Tree, Initialize, "", fullpath, value, inputVal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in Netplait. Here are the details: " + ex.Message, "Netplait", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateFile(TreeView Tree, Action Initialize, string contents, string fullpath, string value, bool inputVal)
        {
            try
            {
                if (inputVal)
                {
                    string upperNode = Tree.Nodes[0].Text.ToString();
                    string CreatedFile = fullpath.ToString() + "\\" + value.ToString();
                    if (value.Contains(upperNode))
                    {
                        MessageBox.Show("Forbidden entry. Try another name other than the Project name.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (!File.Exists(CreatedFile))
                        {
                            File.WriteAllText(CreatedFile, contents);
                        }
                        else
                        {
                            MessageBox.Show("Another file with the same name already exists.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    OpenExpansion(Tree, Initialize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in Netplait. Here are the details: " + ex.Message, "Netplait",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateFolder(TreeView Tree, Action Initialize, string fullpath, string value, bool inputVal)
        {
            try
            {
                if (inputVal)
                {
                    string upperNode = Tree.Nodes[0].Text.ToString();
                    string CreatedFile = fullpath.ToString() + "\\" + value.ToString();
                    if (value == upperNode)
                    {
                        MessageBox.Show("Forbidden entry. Try another name other than the Project name.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(CreatedFile))
                        {
                            System.IO.Directory.CreateDirectory(CreatedFile);
                        }
                        else
                        {
                            MessageBox.Show("Another folder with the same name already exists.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    OpenExpansion(Tree, Initialize);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An unexpected error occurred in Netplait. Here are the details: " + ex.Message, "Netplait",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void OpenInExplorer(string fullpath, ToolStripTextBox tsb)
        {
            tsb.Text = fullpath.ToString();

            Process.Start(fullpath.ToString());
        }

        public static void CommandPrompt(string fullpath, ToolStripTextBox tsb)
        {
            tsb.Text = fullpath.ToString();

            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = fullpath.ToString();
            processStartInfo.FileName = "cmd.exe";
            Process proc = Process.Start(processStartInfo);
        }

        public static void CopyPath(TreeView Tree, string fullpath, ToolStripTextBox tsb)
        {
            tsb.Text = fullpath.ToString();

            Clipboard.SetText(fullpath.ToString(), TextDataFormat.Text);
        }

        public static void PasteFolder(TreeView Tree, string fullpath, Action Initialize)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string path = Clipboard.GetText(TextDataFormat.Text);
                string ToCreate = path.ToString().Substring(path.ToString().LastIndexOf("\\") + 1);

                FileAttributes attr = File.GetAttributes(path.ToString());
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    if (!System.IO.Directory.Exists(fullpath.ToString() + "\\" + ToCreate))
                    {
                        try { System.IO.Directory.CreateDirectory(ToCreate); }
                        catch (Exception ex)
                        {
                            Directory.Delete(ToCreate);
                            MessageBox.Show("The operation could not be completed.",
                                "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        try { CopyFiles.DirectoryCopy(path.ToString(), fullpath.ToString() + "\\" + ToCreate, true); }
                        catch (Exception ex)
                        {
                            Directory.Delete(fullpath.ToString() + "\\" + ToCreate, true);
                            MessageBox.Show("Copying the files of the parent directory might result in a crash.",
                                    "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Another folder with the same name already exists.",
                        "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!File.Exists(fullpath.ToString() + "\\" + ToCreate))
                {
                    File.Copy(path.ToString(), fullpath.ToString() + "\\" + ToCreate);
                }
                else
                {
                    MessageBox.Show("Another file with the same name already exists.",
                    "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                OpenExpansion(Tree, Initialize);
            }
            else
            {
                MessageBox.Show("Clipboard does not contain a directory or filepath.",
                    "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeletePath(int type, TreeView Tree, string fullpath, ToolStripTextBox tsb, Action Initialize)
        {
            tsb.Text = fullpath.ToString();

            DialogResult result;
            if(type == 1)
            {
                result = MessageBox.Show("Are you sure you want to delete the folder?",
                "Netplait", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Delete the folder
                    Directory.Delete(fullpath.ToString(), true);
                }
                else if (result == DialogResult.No) { }
            }
            else if (type == 2)
            {
                result = MessageBox.Show("Are you sure you want to delete the file?",
                "Netplait", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //Delete the file
                    File.Delete(fullpath.ToString());
                }
                else if (result == DialogResult.No) { }
            }

            OpenExpansion(Tree, Initialize);
        }

        public static void RenameFolder(TreeView Tree, string fullpath, ToolStripTextBox tsb, Action Initialize)
        {
            string upperNode = Tree.Nodes[0].Text.ToString();
            tsb.Text = fullpath.ToString();

            string value = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
            //string NewName = fullpath.ToString().Replace(value.ToString(), "");
            string NewName = fullpath.Substring(0, fullpath.Length - value.Length);
            if (InputBox.InputBox.Show("Enter Name", "Rename Folder:", ref value, null) == DialogResult.OK)
            {
                string CreatedFile = NewName.ToString() + "\\" + value.ToString();
                if (value == upperNode)
                {
                    MessageBox.Show("Forbidden entry. Try another name other than the Project name.",
                        "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!System.IO.Directory.Exists(CreatedFile))
                    {
                        Directory.Move(fullpath.ToString(), CreatedFile);
                    }
                    else
                    {
                        MessageBox.Show("Another folder with the same name already exists.",
                            "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                OpenExpansion(Tree, Initialize);
            }
        }

        public static void ExecuteFile(string fullpath, ToolStripTextBox tsb)
        {
            tsb.Text = fullpath.ToString();

            Process.Start(fullpath.ToString());
        }

        public static void OpenExpansion(TreeView Tree, Action Initialize)
        {
            var savedExpansionState = Tree.Nodes.GetExpansionState();
            Tree.BeginUpdate();
            Tree.Nodes.Clear();
            Initialize();
            Tree.Nodes.SetExpansionState(savedExpansionState);
            Tree.EndUpdate();
        }
    }
}