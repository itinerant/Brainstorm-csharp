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
        // undo and redo
        // ///////////////////////////////////////////////////////////////////
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.Redo();
        }

        // ///////////////////////////////////////////////////////////////////
        // basic editing commands
        // ///////////////////////////////////////////////////////////////////
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.Paste();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findDlg.Show();
        }
    }
}
