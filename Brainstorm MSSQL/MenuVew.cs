using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Brainstorm
{
    public partial class MainWindow : Form
    {
        // ///////////////////////////////////////////////////////////////////
        // window events
        // ///////////////////////////////////////////////////////////////////
        private void hideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // ///////////////////////////////////////////////////////////////////
        // selection events
        // ///////////////////////////////////////////////////////////////////
        private void previousCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listCategory.SelectedItems.Count > 0)
                if (listCategory.SelectedItems[0].Index > 0)
                {
                    SelectCategory(listCategory.SelectedItems[0].Index - 1);
                    SelectNote(0);
                }
        }

        private void nextCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listCategory.SelectedItems.Count > 0)
                if (listCategory.SelectedItems[0].Index <= listCategory.Items.Count-2)
                {
                    SelectCategory(listCategory.SelectedItems[0].Index + 1);
                    SelectNote(0);
                }
        }

        private void previousNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listNote.SelectedItems.Count > 0)
                if (listNote.SelectedItems[0].Index > 0)
                    SelectNote(listNote.SelectedItems[0].Index-1);
        }

        private void nextNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listNote.SelectedItems.Count > 0)
                if (listNote.SelectedItems[0].Index <= listNote.Items.Count - 2)
                    SelectNote(listNote.SelectedItems[0].Index + 1);
        }
    }
}
