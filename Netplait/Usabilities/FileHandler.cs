using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Netplait.Usabilities
{
    public class FileHandler
    {        
        public static ScintillaNET.Scintilla TextArea;

        /// <summary>
        /// When a user modified a file opened in a tabpage, a star symbol (*) should signify that it has been modified
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <param name="tabControl1">The responsible tabcontrol</param>
        /// <param name="mn1">menuItem16</param>
        /// <param name="mn2">menuItem17</param>
        /// <param name="mn3">menuItem18</param>
        public static void TextArea_TextChanged(object sender, EventArgs e, TabControl tabControl1, MenuItem mn1, MenuItem mn2, MenuItem mn3)
        {
            try
            {
                string tabText = tabControl1.SelectedTab.Text;
                if (!tabText.Contains("*"))
                {
                    tabControl1.SelectedTab.Text = tabText + "*";
                    mn1.Enabled = true;
                    mn2.Enabled = true;
                    mn3.Enabled = true;
                }
                else { }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Universal method to create a new file from a range of programming languages
        /// </summary>
        /// <param name="mode">1: Python, 2: HTML, 3: CSS, 4: JavaScript, 5: Text/Blank</param>
        /// <param name="FromFile">True if used for opening files and displaying the contents, 
        /// False if used for reading from a template file</param>
        /// <param name="FilterMark">True for filter marks such as '($USER)' to convert to 'Environment.UserName'</param>
        /// <param name="TabPage_Title">Title of the tabpage</param>
        /// <param name="TextArea_Text">The contents of the Textarea (this can be a path or a string, depending on the bool FromFile)</param>
        /// <param name="TabPage_Name">The name of the tabpage (usually the path)</param>
        /// <param name="status_lbl">The ToolStripLabel responsible for Update events</param>
        /// <param name="tabControl1">The tabcontrol to create a new tabpage and display the Textarea in it</param>
        public static void New_Universal(int mode, bool FromFile, bool FilterMark, string TabPage_Title, string TextArea_Text, string TabPage_Name, 
            ToolStripStatusLabel status_lbl, TabControl tabControl1, ContextMenu contextMenu)
        {
            /* "The less code you write, the less mistakes you make."
                                                          - Roubinski */
            try
            {
                TabPage tmpTabPage = new TabPage();
                tabControl1.Controls.Add(tmpTabPage);
                TextArea = new ScintillaNET.Scintilla();
                tmpTabPage.Controls.Add(TextArea);

                TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
                TextArea.ContextMenu = contextMenu;

                if (FromFile == true)
                {
                    tmpTabPage.Text = TabPage_Title;
                    TextArea.Text = File.ReadAllText(TextArea_Text.ToString());
                }
                else if (FromFile == false)
                {
                    tmpTabPage.Text = TabPage_Title;
                    if(FilterMark == true)
                        if (TextArea_Text.Contains("($USER)")) { TextArea_Text = TextArea_Text.Replace("($USER)", Environment.UserName); }
                    TextArea.Text = TextArea_Text.ToString();
                }

                tmpTabPage.SuspendLayout();
                tmpTabPage.Controls.Add(TextArea);
                tmpTabPage.ResumeLayout();

                TextArea.Parent = tmpTabPage;
                TextArea.BringToFront();
                tabControl1.SelectedTab = tmpTabPage;

                if (mode == 1) {
                    tmpTabPage.ImageIndex = 1;
                    Classes.Assignation.AssignPythonStyles(TextArea, status_lbl);
                }
                else if (mode == 2) {
                    tmpTabPage.ImageIndex = 2;
                    Classes.Assignation.AssignHTMLStyles(TextArea, status_lbl);
                }
                else if (mode == 3) {
                    tmpTabPage.ImageIndex = 3;
                    Classes.Assignation.AssignCSSStyles(TextArea, status_lbl);
                }
                else if (mode == 4) {
                    tmpTabPage.ImageIndex = 4;
                    Classes.Assignation.AssignJSStyles(TextArea, status_lbl);
                }
                else if (mode == 5) {
                    tmpTabPage.ImageIndex = 5;
                    Classes.Assignation.AssignBlextStyles(TextArea, status_lbl);
                }

                TextArea.TextChanged += ((sender, e) => TextArea_TextChanged(sender, e, tabControl1, null, null, null));
                tmpTabPage.Name = TabPage_Name.ToString();
                tabControl1.SelectedTab = tmpTabPage;
            }
            catch(Exception ex)
            {
                Classes.Exception.ShowError.Show("An error has occurred while creating the new file. Here are the details:", ex.Message);
            }                
        }
    }
}
