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
        // Category list context menu
        // ///////////////////////////////////////////////////////////////////
        private void newCategoryContextMenuItem_Click(object sender, EventArgs e)
        {
            newCategoryToolStripMenuItem_Click(sender, e);
        }

        private void renameCategoryContextMenuItem_Click(object sender, EventArgs e)
        {
            renameCategoryToolStripMenuItem_Click(sender, e);
        }

        private void deleteCategoryContextMenuItem_Click(object sender, EventArgs e)
        {
            deleteCategoryToolStripMenuItem_Click(sender, e);
        }

        // ///////////////////////////////////////////////////////////////////
        // Note list context menu
        // ///////////////////////////////////////////////////////////////////
        private void newNoteContextMenuItem_Click(object sender, EventArgs e)
        {
            newNoteToolStripMenuItem_Click(sender, e);
        }

        private void renameNoteContextMenuItem_Click(object sender, EventArgs e)
        {
            renameNoteToolStripMenuItem_Click(sender, e);
        }

        private void deleteNoteContextMenuItem_Click(object sender, EventArgs e)
        {
            deleteNoteToolStripMenuItem_Click(sender, e);
        }

        // ///////////////////////////////////////////////////////////////////
        // Note Text context menu
        // ///////////////////////////////////////////////////////////////////
        private void saveNoteContextMenuItem_Click(object sender, EventArgs e)
        {
            saveNoteToolStripMenuItem_Click(sender, e);
        }

        private void cutContextMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyContextMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteContextMenuItem_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void bulletListContextMenuItem_Click(object sender, EventArgs e)
        {
            bulletListToolStripMenuItem_Click(sender, e);
        }

        private void showFontsContextMenuItem_Click(object sender, EventArgs e)
        {
            showFontsToolStripMenuItem_Click(sender, e);
        }

        private void findReplaceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            findReplaceToolStripMenuItem_Click(sender, e);
        }
    }
}