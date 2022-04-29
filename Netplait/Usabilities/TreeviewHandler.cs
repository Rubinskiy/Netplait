using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Netplait.Usabilities
{
    public class TreeviewHandler
    {
        public static void Tree_BlankEvent(TreeNodeMouseClickEventArgs e)
        {
            /* This function is used when there are no projects to load */
        }

        /// <summary>
        /// The NodeMouseClick event for the Directory tree
        /// </summary>        
        /// <param name="DirTree">The Treeview (Netplait Treeview)</param>
        /// <param name="tb1">The ToolStripTextBox to capture all paths from nodes</param>
        /// <param name="cf1">The ContextMenu for directory attributes (cFolder1)</param>
        /// <param name="cf2">The ContextMenu for file attributes (cFile1)</param>
        /// /// <param name="e">TreeNodeMouseClickEventArgs</param>
        public static void DirectoryTree_MouseClickEvent(TreeView DirTree, ToolStripTextBox tb1, ContextMenu cf1, ContextMenu cf2, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode CurrentNode = DirTree.SelectedNode;
                string fullpath = CurrentNode.FullPath.ToString();

                tb1.Text = fullpath.ToString();

                if (e.Button == MouseButtons.Right)
                {
                    DirTree.SelectedNode = e.Node;
                }

                FileAttributes attr = File.GetAttributes(fullpath.ToString());
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    e.Node.ContextMenu = cf1;
                }
                else
                {
                    e.Node.ContextMenu = cf2;
                }
            }
            catch (Exception ex)
            {
                //nyet
            }
}

        /// <summary>
        /// The NodeMouseDoubleClick event for the Directory tree
        /// </summary>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the Textarea in it</param>
        /// <param name="status_lbl">The ToolStripLabel responsible for Update events</param>
        /// <param name="e">TreeNodeMouseClickEventArgs</param>
        public static void DirectoryTree_DoubleClickEvent(TabControl tabControl1, ContextMenu contextMenu, ToolStripStatusLabel status_lbl, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode CurrentNode = e.Node;
                string fullpath = CurrentNode.FullPath;
                string noExt = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
                FileAttributes attr = File.GetAttributes(fullpath.ToString());

                bool found = false;
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    string signi = noExt + "*";
                    //same filename but not same path
                    if (noExt.Equals(tab.Text) && !fullpath.Equals(tab.Name) || signi.Equals(tab.Text) && !fullpath.Equals(tab.Name)) { }
                    //same filename and same path
                    else if (noExt.Equals(tab.Text) && fullpath.Equals(tab.Name) || signi.Equals(tab.Text) && fullpath.Equals(tab.Name))
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
                            Usabilities.FileHandler.New_Universal(1, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //HTML
                        else if (FileTypes.FileExtensionHandler.IsHTMLFile(CurrentNode.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(2, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //CSS
                        else if (FileTypes.FileExtensionHandler.IsCSSFile(CurrentNode.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(3, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //Js
                        else if (FileTypes.FileExtensionHandler.IsJSFile(CurrentNode.ToString()))
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
            catch (Exception ex) { /*dont show an error, as it will disrupt the double clicking*/ }
        }

        /// <summary>
        /// The NodeMouseClick event for the Project tree
        /// </summary>
        /// <param name="ProjTree">The Treeview (Netplait Treeview)</param>
        /// <param name="tssl">The ToolStripStatusLabel project name</param>
        /// <param name="tsb">The ToolStripTextBox for easing purposes</param>
        /// <param name="cFolder">The ContextMenu for directory attributes (cFolder)</param>
        /// <param name="cFile">The ContextMenu for file attributes (cFile)</param>
        /// <param name="cProject">The ContextMenu for project attribtutes (cProject)</param>
        /// <param name="e">TreeNodeMouseClickEventArgs</param>
        public static void ProjectTree_MouseClickEvent(TreeView ProjTree, ToolStripStatusLabel tssl, ToolStripTextBox tsb,
            ContextMenu cFolder, ContextMenu cFile, ContextMenu cProject, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode CurrentNode = ProjTree.SelectedNode;
                string toremove = tssl.Text.Substring(tssl.Text.LastIndexOf("\\") + 1);
                //string filename = CurrentNode.FullPath.Replace(toremove, "");
                //string fullpath = tssl.Text + filename;
                string outPath = tssl.Text.Substring(0, tssl.Text.Length - toremove.Length);
                string fullpath = outPath + CurrentNode.FullPath.ToString();
                tsb.Text = fullpath;

                //=====================================
                //SOOOOOOOOOOOOOOOOOOO CONFUSINGGGGGGGGGGGGGGGGGGGGGG
                //ALSO THE INITIALIZATION IN THE MESSAGES MAINFORM MAKE SOME SPACES IN
                //SEARCHING FOR SNIPPETS PATH \N
                //SNIPPETS PATH LOADED TO LIST \N
                //=====================================

                if (e.Button == MouseButtons.Right)
                {
                    ProjTree.SelectedNode = e.Node;
                }

                FileAttributes attr = File.GetAttributes(fullpath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory && e.Node.Level != 0)
                {
                    e.Node.ContextMenu = cFolder;
                }
                else if ((attr & FileAttributes.Directory) != FileAttributes.Directory && e.Node.Level != 0)
                {
                    e.Node.ContextMenu = cFile;
                }
                else
                {
                    e.Node.ContextMenu = cProject;
                }
            }
            catch (Exception ex)
            {
                //nyet
            }
        }

        /// <summary>
        /// The NodeMouseDoubleClick event for the Project tree
        /// </summary>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the Textarea in it</param>
        /// <param name="tssl">The ToolStripStatusLabel project name</param>
        /// <param name="tsb">The ToolStripTextBox for easing purposes</param>
        /// <param name="contextMenu">The ContextMenu for the Scintilla replacement (cEditor)</param>
        /// <param name="status_lbl">The ToolStripLabel responsible for Update events</param>
        /// <param name="e">TreeNodeMouseClickEventArgs</param>
        public static void ProjectTree_DoubleClickEvent(TabControl tabControl1, ToolStripStatusLabel tssl, ToolStripTextBox tsb,
            ContextMenu contextMenu, ToolStripStatusLabel status_lbl, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode CurrentNode = e.Node;
                string toremove = tssl.Text.Substring(tssl.Text.LastIndexOf("\\") + 1);
                //string filename = CurrentNode.FullPath.Replace(toremove, "");
                //string fullpath = tssl.Text + filename;
                string outPath = tssl.Text.Substring(0, tssl.Text.Length - toremove.Length);
                string fullpath = outPath + CurrentNode.FullPath.ToString();
                tsb.Text = fullpath;

                string noExt = fullpath.ToString().Substring(fullpath.ToString().LastIndexOf("\\") + 1);
                FileAttributes attr = File.GetAttributes(fullpath.ToString());

                bool found = false;
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    string signi = noExt + "*";
                    //same filename but not same path
                    if (noExt.Equals(tab.Text) && !fullpath.Equals(tab.Name) || signi.Equals(tab.Text) && !fullpath.Equals(tab.Name)) { }
                    //same filename and same path
                    else if (noExt.Equals(tab.Text) && fullpath.Equals(tab.Name) || signi.Equals(tab.Text) && fullpath.Equals(tab.Name))
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
                            Usabilities.FileHandler.New_Universal(1, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //HTML
                        else if (FileTypes.FileExtensionHandler.IsHTMLFile(CurrentNode.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(2, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //CSS
                        else if (FileTypes.FileExtensionHandler.IsCSSFile(CurrentNode.ToString()))
                        {
                            Usabilities.FileHandler.New_Universal(3, true, false, noExt, fullpath, fullpath, status_lbl, tabControl1, contextMenu);
                        }
                        //Js
                        else if (FileTypes.FileExtensionHandler.IsJSFile(CurrentNode.ToString()))
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
            catch (Exception ex) { /*dont show an error, as it will disrupt the double clicking*/ }
        }

        public static TreeNode OpenType(int type, TreeView Tree, TreeNodeMouseClickEventArgs e)
        {
            if (type == 1)
                return e.Node;
            else if (type == 2)
                return Tree.SelectedNode;
            else
                return null;
        }
    }
}
