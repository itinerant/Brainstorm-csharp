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
        // variables for tracking current category and note
        private static string currentCategory = "";
        private static string currentNote = "";

        // variables for renaming
        private static string selectedCategory;
        private static string renamedCategory;
        private static string selectedNote;
        private static string renamedNote;

        // variable for suppressing tab key in bulleted lists
        private static bool tabKey = false;

        // Find & Replace dialog
        private static FindReplaceDialog findDlg;

        public MainWindow()
        {
            InitializeComponent();

            Hotkey hk = new Hotkey();
            hk.KeyCode = Keys.Z;
            hk.Windows = true;
            hk.Pressed += delegate { trayIcon_Click(this, null); };
            hk.Register(this);
        }

        public RichTextBox5 GetRTB()
        {
            return textNote;
        }

        // ///////////////////////////////////////////////////////////////////
        // program functionality to do list
        // ///////////////////////////////////////////////////////////////////

        // TODO: save custom colors in color dialog

        // ///////////////////////////////////////////////////////////////////
        // help menu
        // ///////////////////////////////////////////////////////////////////
        private void aboutBrainstormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBrainstorm about = new AboutBrainstorm();
            about.Show();
        }

        // ///////////////////////////////////////////////////////////////////
        // selection handlers
        // /////////////////////////////////////////////////////////////////// 
        private void SelectCategory(int index)
        {
            if ((listCategory.SelectedItems.Count > 0) && (listNote.SelectedItems.Count > 0))
                SaveNote();

            listNote.Items.Clear();
            if (listCategory.Items.Count > index)
            {
                listCategory.Items[index].Selected = true;
                listCategory.Select();
                listCategory.EnsureVisible(index);
                currentCategory = listCategory.SelectedItems[0].Text;
                GetNotes();
            }
        }

        private void SelectNote(int index)
        {
            if((listCategory.SelectedItems.Count > 0) && (listNote.SelectedItems.Count > 0))
                SaveNote();

            if (listNote.Items.Count > index)
            {
                listNote.Items[index].Selected = true;
                listNote.Select();
                listNote.EnsureVisible(index);
                currentNote = listNote.SelectedItems[0].Text;
                GetNoteText();
                textNote.Focus();
            }
            else
                textNote.Clear();
        }

        // ///////////////////////////////////////////////////////////////////
        // list event handlers
        // ///////////////////////////////////////////////////////////////////
        private void listCategory_Click(object sender, EventArgs e)
        {
            if (listCategory.SelectedItems.Count > 0)
            {
                SelectCategory(listCategory.SelectedItems[0].Index);
                SelectNote(0);
            }
        }
        
        private void listCategory_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Return) && (e.KeyCode != Keys.Enter))
                listCategory_Click(sender, e);
        }

        private void listNote_Click(object sender, EventArgs e)
        {
            if(listNote.SelectedItems.Count > 0)
                SelectNote(listNote.SelectedItems[0].Index);
        }

        private void listNote_KeyUp(object sender, KeyEventArgs e)
        {
            if((e.KeyCode != Keys.Return) && (e.KeyCode != Keys.Enter))
                listNote_Click(sender, e);
        }

        // ///////////////////////////////////////////////////////////////////
        // textNote event handlers
        // ///////////////////////////////////////////////////////////////////   
        private void textNote_KeyDown(object sender, KeyEventArgs e)
        {
            tabKey = false;

            if (e.KeyCode == Keys.Tab && (e.Shift))
            {
                if (textNote.SelectionBullet)
                {
                    if (textNote.SelectionIndent < 15)
                        textNote.SelectionIndent = 0;
                    else
                        textNote.SelectionIndent -= 15;

                    e.SuppressKeyPress = true;
                    tabKey = true;
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (textNote.SelectionBullet)
                {
                    textNote.SelectionIndent += 15;
                    e.SuppressKeyPress = true;
                    tabKey = true;
                }
            }       
        }

        private void textNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tabKey == true)
            {
                e.Handled = true;
            }    
        }

         private void textNote_SelectionChanged(object sender, EventArgs e)
         {
             try
             {
                 updateMenus();
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.ToString());
             }
         }

         private void textNote_LinkClicked(object sender, LinkClickedEventArgs e)
         {
             System.Diagnostics.Process.Start(e.LinkText);
         }

         // ///////////////////////////////////////////////////////////////////
         // list editing handlers
         // /////////////////////////////////////////////////////////////////// 
         private void listCategory_DoubleClick(object sender, EventArgs e)
         {
             renameCategoryToolStripMenuItem_Click(sender, e);
         }

        private void listCategory_BeforeLabelEdit(object sender, LabelEditEventArgs e)
         {
             selectedCategory = listCategory.SelectedItems[0].Text;
         }

         private void listCategory_AfterLabelEdit(object sender, LabelEditEventArgs e)
         {
             try
             {
                 renamedCategory = e.Label;
                 if (renamedCategory != selectedCategory)
                 {
                     int curNote = listNote.SelectedItems.Count > 0 ? listNote.SelectedItems[0].Index : -1;
                     RenameCategory();
                     GetCategories();
                     SelectCategory(listCategory.FindItemWithText(renamedCategory).Index);
                     if(curNote >= 0)
                        SelectNote(curNote);
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.ToString());
             }
         }

         private void listNote_BeforeLabelEdit(object sender, LabelEditEventArgs e)
         {
             selectedNote = listNote.SelectedItems[0].Text;
         }

         private void listNote_AfterLabelEdit(object sender, LabelEditEventArgs e)
         {
             try
             {
                 renamedNote = e.Label;
                 if (renamedNote != selectedNote)
                 {
                     RenameNote();
                     GetNotes();
                     SelectNote(listNote.FindItemWithText(renamedNote).Index);
                     GetNoteText();
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.ToString());
             }
         }

         // ///////////////////////////////////////////////////////////////////
         // window event handlers
         // /////////////////////////////////////////////////////////////////// 
         private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
         {
             if((listCategory.SelectedItems.Count > 0) && (listNote.SelectedItems.Count > 0))
                SaveNote();
         }

         private void MainWindow_Load(object sender, EventArgs e)
         {
             // set default state for textbox
             textNote.Font = new Font("Arial Unicode MS", 10);

             // set default state for Find Dialog
             findDlg = new FindReplaceDialog(this);

             OpenDatabase();
             GetCategories();
             SelectCategory(0);
             SelectNote(0);
         }

         private void MainWindow_Resize(object sender, EventArgs e)
         {
             if (FormWindowState.Minimized == this.WindowState)
             {
                 trayIcon.Visible = true;
                 this.Hide();
             }
             else if (FormWindowState.Normal == this.WindowState)
             {
                 trayIcon.Visible = false;
             }
         }

         private void trayIcon_Click(object sender, EventArgs e)
         {
             this.TopMost = true;
             this.Show();
             this.WindowState = FormWindowState.Normal;
             this.BringToFront();
             textNote.Focus();
             this.TopMost = false;
         }

         private void splitNote_SplitterMoved(object sender, SplitterEventArgs e)
         {
             listCategory.Columns[0].Width = listCategory.Width - 20;
             listNote.Columns[0].Width = listNote.Width - 20;
         }
    }
}
