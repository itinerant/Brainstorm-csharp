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
    public partial class FindReplaceDialog : Form
    {
        private Label label1;
        private Label label2;
        private TextBox textFind;
        private TextBox textReplace;
        private CheckBox checkWholeWord;
        private Button buttonFind;
        private Button buttonReplaceAll;
        private Button buttonClose;
        private Button buttonReplace;
        private MainWindow mainForm;
        private Button buttonFindNext;
        private CheckBox checkMatchCase;
        private RichTextBox5 rtb;

        public FindReplaceDialog(MainWindow parentForm)
        {
            this.mainForm = parentForm;
            rtb = mainForm.GetRTB();
            InitializeComponent();
        }
       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindReplaceDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textFind = new System.Windows.Forms.TextBox();
            this.textReplace = new System.Windows.Forms.TextBox();
            this.checkWholeWord = new System.Windows.Forms.CheckBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.checkMatchCase = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Replace with:";
            // 
            // textFind
            // 
            this.textFind.Location = new System.Drawing.Point(90, 14);
            this.textFind.Name = "textFind";
            this.textFind.Size = new System.Drawing.Size(184, 20);
            this.textFind.TabIndex = 1;
            this.textFind.TextChanged += new System.EventHandler(this.textFind_TextChanged);
            // 
            // textReplace
            // 
            this.textReplace.Location = new System.Drawing.Point(90, 43);
            this.textReplace.Name = "textReplace";
            this.textReplace.Size = new System.Drawing.Size(184, 20);
            this.textReplace.TabIndex = 2;
            // 
            // checkWholeWord
            // 
            this.checkWholeWord.AutoSize = true;
            this.checkWholeWord.Location = new System.Drawing.Point(12, 102);
            this.checkWholeWord.Name = "checkWholeWord";
            this.checkWholeWord.Size = new System.Drawing.Size(86, 17);
            this.checkWholeWord.TabIndex = 4;
            this.checkWholeWord.Text = "Whole Word";
            this.checkWholeWord.UseVisualStyleBackColor = true;
            // 
            // buttonFind
            // 
            this.buttonFind.Enabled = false;
            this.buttonFind.Location = new System.Drawing.Point(199, 69);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 23);
            this.buttonFind.TabIndex = 5;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Enabled = false;
            this.buttonReplace.Location = new System.Drawing.Point(198, 98);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(75, 23);
            this.buttonReplace.TabIndex = 7;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Enabled = false;
            this.buttonReplaceAll.Location = new System.Drawing.Point(280, 98);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.buttonReplaceAll.TabIndex = 8;
            this.buttonReplaceAll.Text = "Replace All";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(280, 11);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Enabled = false;
            this.buttonFindNext.Location = new System.Drawing.Point(280, 69);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(75, 23);
            this.buttonFindNext.TabIndex = 6;
            this.buttonFindNext.Text = "Find Next";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // checkMatchCase
            // 
            this.checkMatchCase.AutoSize = true;
            this.checkMatchCase.Location = new System.Drawing.Point(12, 73);
            this.checkMatchCase.Name = "checkMatchCase";
            this.checkMatchCase.Size = new System.Drawing.Size(83, 17);
            this.checkMatchCase.TabIndex = 3;
            this.checkMatchCase.Text = "Match Case";
            this.checkMatchCase.UseVisualStyleBackColor = true;
            // 
            // FindReplaceDialog
            // 
            this.AcceptButton = this.buttonFind;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(366, 132);
            this.Controls.Add(this.checkMatchCase);
            this.Controls.Add(this.buttonFindNext);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonReplaceAll);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.checkWholeWord);
            this.Controls.Add(this.textReplace);
            this.Controls.Add(this.textFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindReplaceDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find & Replace";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            FindText(0, true);
            buttonFindNext.Focus();
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
             FindText(rtb.SelectionStart + rtb.SelectionLength, true);
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            Replace();
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            rtb.SelectionStart = 0;
            rtb.SelectionLength = 0;
            int replacements = 0;
            while (rtb.SelectionStart < rtb.Text.Length-1)
            {
                FindText(rtb.SelectionStart + rtb.SelectionLength, false);
                replacements += Replace();
            }
            MessageBox.Show("Replaced " + replacements + " occurences of \"" + textFind.Text + "\"",
                "Find & Replace", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindText(int start, bool showMessage)
        {
            try
            {
                //variable to hold the start position (start of the found text)
                int startPos;

                //look for the search text
                RichTextBoxFinds finds = RichTextBoxFinds.None;
                if(checkMatchCase.Checked)
                    finds |= RichTextBoxFinds.MatchCase;
                if (checkWholeWord.Checked)
                    finds |= RichTextBoxFinds.WholeWord;
                startPos = rtb.Find(textFind.Text, start, finds);
                
                //check the position
                if (!(startPos > 0))
                {
                    //text doesn't exist so let the user know
                    if(showMessage)
                        MessageBox.Show("Reached end of document", "Find & Replace", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonFind.Focus();
                    rtb.Select(rtb.Rtf.Length, 0);
                    rtb.ScrollToCaret();
                    rtb.Focus();
                    return;
                }
                else
                {
                    //Eureka! Select the found text
                    rtb.Select(startPos, textFind.Text.Length);
                    //scroll to the found text
                    rtb.ScrollToCaret();
                    //add focus so the highlighting shows up
                    rtb.Focus();
                    buttonFindNext.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Error");
            }
        }

        private int Replace()
        {
            if (rtb.SelectionLength > 0)
            {
                Font f = rtb.SelectionFont;
                rtb.SelectedText = textReplace.Text;
                rtb.SelectionFont = f;
                return 1;
            }
            return 0;
        }

        private void textFind_TextChanged(object sender, EventArgs e)
        {
            bool textExists = (textFind.Text.Length > 0);
            buttonFind.Enabled = textExists;
            buttonFindNext.Enabled = textExists;
            buttonReplace.Enabled = textExists;
            buttonReplaceAll.Enabled = textExists;
        }
    }
}