﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Netplait.Custom
{
    class TableView : ToolStripDropDown
    {
        public TableView()
        {
            Items.Add(new ToolStripControlHost(control));

            control.TableSizeSelected += new TableSizeSelectedEventHandler(control_TableSizeSelected);
            control.SelectionCancelled += new EventHandler(control_SelectionCancelled);
        }

        public TableSizeControl Selector
        {
            get { return this.control; }
        }

        private void control_SelectionCancelled(object sender, EventArgs e)
        {
            this.Close(ToolStripDropDownCloseReason.CloseCalled);
        }

        private void control_TableSizeSelected(object sender, TableSizeEventArgs e)
        {
            this.Close(ToolStripDropDownCloseReason.CloseCalled);
        }

        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            base.OnOpening(e);

            ToolStripProfessionalRenderer renderer = Renderer as ToolStripProfessionalRenderer;

            if (renderer != null)
                control.BackColor = renderer.ColorTable.ToolStripDropDownBackground;

            //control.SelectedSize = new Size(0, 0);
            //control.VisibleRange = new Size(5, 4);
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            control.Focus();
        }

        private TableSizeControl control = new TableSizeControl();
    }
}
