using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Forms
{
    public partial class NewProj : Form
    {
        public NewProj()
        {
            InitializeComponent();

            System.Windows.Forms.ImageList myImageList1 = new ImageList();
            myImageList1.ImageSize = new Size(86, 71);
           
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\python_proj.png"));
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\django_proj.png"));
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\flask_proj.png"));
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\pyramid_proj.png"));
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\bottle_proj.png"));
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\web_proj.png"));            
            myImageList1.Images.Add(Image.FromFile(Application.StartupPath + "\\Icons\\NewProj\\blank_proj.png"));

            ProjList.LargeImageList = myImageList1;


            ProjList.Items.Add("New Python Project", 0);
            ProjList.Items.Add("New Django Project", 1);
            ProjList.Items.Add("New Flask Project", 2);
            ProjList.Items.Add("New Pyramid Project", 3);
            ProjList.Items.Add("New Bottle Project", 4);
            ProjList.Items.Add("New Web Project With HTML, CSS, JS", 5);
            ProjList.Items.Add("New Blank Project", 6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(ProjList.SelectedItems.Count != 0)
            {
                if (ProjList.FocusedItem.Text == "New Python Project")
                {
                    this.Hide();
                    NewPythonProj npp = new NewPythonProj();
                    npp.ShowDialog();
                }
                else if (ProjList.FocusedItem.Text == "New Django Project")
                {
                    MessageBox.Show("New Django Project");
                }
                else if (ProjList.FocusedItem.Text == "New Flask Project")
                {
                    this.Hide();
                    NewFlaskProj nfp = new NewFlaskProj();                    
                    nfp.ShowDialog();                    
                }
                else if (ProjList.FocusedItem.Text == "New Pyramid Project")
                {
                    MessageBox.Show("New Pyramid Project");
                }
                else if (ProjList.FocusedItem.Text == "New Bottle Project")
                {
                    MessageBox.Show("New Bottle Project");
                }
                else if (ProjList.FocusedItem.Text == "New Web Project With HTML, CSS, JS")
                {
                    MessageBox.Show("New Web Project");
                }
                else if (ProjList.FocusedItem.Text == "New Blank Project")
                {
                    MessageBox.Show("New Blank Project");
                }                
            }
            else
            {
                MessageBox.Show("Project type not specified. Please select a template for the new project.", 
                    "Netplait", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void rbLargeIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLargeIcon.Checked == true)
            {
                ProjList.View = View.LargeIcon;
            }
        }

        private void rbList_CheckedChanged(object sender, EventArgs e)
        {
            if (rbList.Checked == true)
            {
                ProjList.View = View.List;
            }
        }

        private void rbSmallIcon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmallIcon.Checked == true)
            {
                ProjList.View = View.SmallIcon;
            }
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTile.Checked == true)
            {
                ProjList.View = View.Tile;
            }
        }

        private void ProjList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProjList.FocusedItem.Text == "New Python Project")
            {
                richTextBox1.Text = "A Python project for Python 2.7.9 and later versions.";
            }
            else if (ProjList.FocusedItem.Text == "New Django Project")
            {
                richTextBox1.Text = "A project for creating Django Web-applications with Python 2.7.9 and later versions.";
            }
            else if (ProjList.FocusedItem.Text == "New Flask Project")
            {
                richTextBox1.Text = "A project for creating Flask Web-applications with Python 2.7.9 and later versions.";
            }
            else if (ProjList.FocusedItem.Text == "New Pyramid Project")
            {
                richTextBox1.Text = "A project for creating Pyramid Web-applications with Python 2.7.9 and later versions.";
            }
            else if (ProjList.FocusedItem.Text == "New Bottle Project")
            {
                richTextBox1.Text = "A project for creating Web-applications with Micro-framework Bottle and Python 2.7.9 and later versions.";
            }
            else if(ProjList.FocusedItem.Text == "New Web Project With HTML, CSS, JS")
            {
                richTextBox1.Text = "A pre-configured Web application project with HTML5, CSS3 and JavaScript files.";
            }
            else if(ProjList.FocusedItem.Text == "New Blank Project")
            {
                richTextBox1.Text = "A Blank project with no template configurations for creating applications from scratch.";
            }            
        }
    }
}