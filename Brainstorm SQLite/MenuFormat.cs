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
        // font styles
        // ///////////////////////////////////////////////////////////////////
        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold, !boldToolStripButton.Checked);

            updateMenus();
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic, !italicToolStripButton.Checked);

            updateMenus();
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline, !underlineToolStripButton.Checked);

            updateMenus();
        }

        private void strikethroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Strikeout, !strikethroughToolStripButton.Checked);

            updateMenus();
        }
        
        private void boldToolStripButton_Click(object sender, EventArgs e)
        {
            boldToolStripMenuItem_Click(sender, e);
        }

        private void italicToolStripButton_Click(object sender, EventArgs e)
        {
           italicToolStripMenuItem_Click(sender, e);
        }

        private void underlineToolStripButton_Click(object sender, EventArgs e)
        {
            underlineToolStripMenuItem_Click(sender, e);
        }

        private void strikethroughToolStripButton_Click(object sender, EventArgs e)
        {
            strikethroughToolStripMenuItem_Click(sender, e);
        }

        // ///////////////////////////////////////////////////////////////////
        // font and color dialog
        // ///////////////////////////////////////////////////////////////////
        private void showFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.ShowColor = true;
            font.ShowEffects = true;

            font.Font = textNote.SelectionFont;
            font.Color = textNote.SelectionColor;
            if (font.ShowDialog() == DialogResult.OK)
            {
                textNote.SelectionFont = font.Font;
                textNote.SelectionColor = font.Color;
            }
        }

        private void selectColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = textNote.SelectionColor;

            if (cd.ShowDialog() == DialogResult.OK)
                textNote.SelectionColor = cd.Color;
        }

        private void selectColorToolStripButton_Click(object sender, EventArgs e)
        {
            selectColorToolStripMenuItem_Click(sender, e);
        }

        private void increaseFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // step .5 up to 8
            // step 1.0 up to 14
            // step 2.0 up to 30
            // step 6 over 30
            float increment = 0.0f;

            hiddenNote.Rtf = textNote.SelectedRtf;
            int lengt = hiddenNote.Text.Length;
            int length = textNote.SelectionLength;
            int start = textNote.SelectionStart;
            for (int i = 0; i < lengt; i++)
            {
                hiddenNote.Select(i, 1);

                if (hiddenNote.SelectionFont.Size < 8)
                    increment = 0.5f;
                else if (hiddenNote.SelectionFont.Size < 14)
                    increment = 1.0f;
                else if (hiddenNote.SelectionFont.Size <= 30)
                    increment = 2.0f;
                else
                    increment = 6.0f;

                hiddenNote.SelectionFont = new Font(
                    hiddenNote.SelectionFont.FontFamily,
                    hiddenNote.SelectionFont.Size + increment,
                    hiddenNote.SelectionFont.Style);
            }
            hiddenNote.Select(0, hiddenNote.Text.Length);
            textNote.SelectedRtf = hiddenNote.SelectedRtf;
            textNote.Select(start, length);
            this.textNote.Focus();  
        }

        private void decreaseFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // step .5 up to 8
            // step 1.0 up to 14
            // step 2.0 up to 30
            // step 6 over 32
            float decrement = 0.0f;

            hiddenNote.Rtf = textNote.SelectedRtf;
            int lengt = hiddenNote.Text.Length;
            int length = textNote.SelectionLength;
            int start = textNote.SelectionStart;
            for (int i = 0; i < lengt; i++)
            {
                hiddenNote.Select(i, 1);

                if (hiddenNote.SelectionFont.Size <= 8)
                    decrement = 0.5f;
                else if (hiddenNote.SelectionFont.Size <= 14)
                    decrement = 1.0f;
                else if (hiddenNote.SelectionFont.Size <= 32)
                    decrement = 2.0f;
                else
                    decrement = 6.0f;

                hiddenNote.SelectionFont = new Font(
                    hiddenNote.SelectionFont.FontFamily,
                    hiddenNote.SelectionFont.Size - decrement,
                    hiddenNote.SelectionFont.Style);
            }
            hiddenNote.Select(0, hiddenNote.Text.Length);
            textNote.SelectedRtf = hiddenNote.SelectedRtf;
            textNote.Select(start, length);
            this.textNote.Focus();  
        }

        private void resetFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hiddenNote.Rtf = textNote.SelectedRtf;
            int lengt = hiddenNote.Text.Length;
            int length = textNote.SelectionLength;
            int start = textNote.SelectionStart;
            FontStyle fs;
            for (int i = 0; i < lengt; i++)
            {
                hiddenNote.Select(i, 1);
                fs = hiddenNote.SelectionFont.Style;
                hiddenNote.SelectionFont = new Font("Calibri", 11, fs);
            }
            hiddenNote.Select(0, hiddenNote.Text.Length);
            textNote.SelectedRtf = hiddenNote.SelectedRtf;
            textNote.Select(start, length);
            this.textNote.Focus();  
        }

        // ///////////////////////////////////////////////////////////////////
        // change style without losing previous styles per character
        // ///////////////////////////////////////////////////////////////////
        private void ChangeFontStyle(FontStyle style, bool add)
        {
            hiddenNote.Rtf = textNote.SelectedRtf;
            int lengt = hiddenNote.Text.Length;
            int length = textNote.SelectionLength;
            int start = textNote.SelectionStart;

            if (textNote.SelectionLength == 0)
            {
                // make style change within visible RichTextBox
                FontStyle fs = textNote.SelectionFont.Style;
                if (add)
                    fs = fs | style;
                else
                    fs = fs & ~style;
                textNote.SelectionFont = new Font(textNote.SelectionFont.FontFamily, 
                    textNote.SelectionFont.Size, fs);
            }
            else
            {
                // use hidden RichTextBox to make changes without flashing
                for (int i = 0; i < lengt; i++)
                {
                    hiddenNote.Select(i, 1);
                    Font cfont = hiddenNote.SelectionFont;
                    FontStyle fs = cfont.Style;
                    if (add)
                    {
                        fs = fs | style;
                    }
                    else
                    {
                        fs = fs & ~style;
                    }
                    hiddenNote.SelectionFont = new Font(cfont.FontFamily, cfont.Size, fs);
                }
                hiddenNote.Select(0, hiddenNote.Text.Length);
                textNote.SelectedRtf = hiddenNote.SelectedRtf;
                textNote.Select(start, length);
                this.textNote.Focus();
            }
        } 

        // ///////////////////////////////////////////////////////////////////
        // alignment styles
        // ///////////////////////////////////////////////////////////////////
        private void alignLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.SelectionAlignment = HorizontalAlignment.Left;
            updateMenus();
        }

        private void alignCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.SelectionAlignment = HorizontalAlignment.Center;
            updateMenus();
        }

        private void alignRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.SelectionAlignment = HorizontalAlignment.Right;
            updateMenus();
        }

        private void alignLeftToolStripButton_Click(object sender, EventArgs e)
        {
            alignLeftToolStripMenuItem_Click(sender, e);
        }

        private void alignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            alignCenterToolStripMenuItem_Click(sender, e);
        }

        private void alignRightToolStripButton_Click(object sender, EventArgs e)
        {
            alignRightToolStripMenuItem_Click(sender, e);
        }

        private void clearAlignmentCheckboxes()
        {
            // remove all checks from alignment menu
            alignLeftToolStripMenuItem.Checked = false;
            alignCenterToolStripMenuItem.Checked = false;
            alignRightToolStripMenuItem.Checked = false;

            // remove all checks from toolbar
            alignLeftToolStripButton.Checked = false;
            alignCenterToolStripButton.Checked = false;
            alignRightToolStripButton.Checked = false;
        }

        // ///////////////////////////////////////////////////////////////////
        // bullets
        // ///////////////////////////////////////////////////////////////////
        private void bulletListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textNote.SelectionBullet = !textNote.SelectionBullet;
            if (!textNote.SelectionBullet)
                textNote.SelectionIndent = 0;
            updateMenus();
        }

        private void bulletListToolStripButton_Click(object sender, EventArgs e)
        {
            bulletListToolStripMenuItem_Click(sender, e);
        }

        // ///////////////////////////////////////////////////////////////////
        // update menus
        // ///////////////////////////////////////////////////////////////////
        private void updateMenus()
        {
            // font styles
            boldToolStripMenuItem.Checked = textNote.SelectionFont.Bold;
            boldToolStripButton.Checked = textNote.SelectionFont.Bold;
            italicToolStripMenuItem.Checked = textNote.SelectionFont.Italic;
            italicToolStripButton.Checked = textNote.SelectionFont.Italic;
            underlineToolStripMenuItem.Checked = textNote.SelectionFont.Underline;
            underlineToolStripButton.Checked = textNote.SelectionFont.Underline;
            strikethroughToolStripMenuItem.Checked = textNote.SelectionFont.Strikeout;
            strikethroughToolStripButton.Checked = textNote.SelectionFont.Strikeout;

            // alignment
            clearAlignmentCheckboxes();
            if (textNote.SelectionAlignment == HorizontalAlignment.Left)
            {
                alignLeftToolStripMenuItem.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Left);
                alignLeftToolStripButton.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Left);
            }
            else if (textNote.SelectionAlignment == HorizontalAlignment.Center)
            {
                alignCenterToolStripMenuItem.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Center);
                alignCenterToolStripButton.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Center);
            }
            else if (textNote.SelectionAlignment == HorizontalAlignment.Right)
            {
                alignRightToolStripMenuItem.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Right);
                alignRightToolStripButton.Checked = textNote.SelectionAlignment.HasFlag(HorizontalAlignment.Right);
            }

            // bullets
            bulletListToolStripMenuItem.Checked = textNote.SelectionBullet;
            bulletListToolStripButton.Checked = textNote.SelectionBullet;
            bulletListContextMenuItem.Checked = textNote.SelectionBullet;
        }
    }
}
