using System;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Brainstorm
{
    public partial class MainWindow : Form
    {
        // ///////////////////////////////////////////////////////////////////
        // category
        // ///////////////////////////////////////////////////////////////////
        private void newCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newText = "New Category";
            listCategory.Items.Add(newText);
            AddCategory(newText);
            SelectCategory(listCategory.FindItemWithText(newText).Index);
            textNote.Clear();
            renameCategoryToolStripMenuItem_Click(sender, e);
        }

        private void renameCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listCategory.SelectedItems[0].BeginEdit();
        }

        private void deleteCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listNote.Items.Count > 0)
             {
                 MessageBox.Show("Categories cannot be deleted when there are associated Notes.", "Delete Category",
                     MessageBoxButtons.OK, MessageBoxIcon.Hand);
                 return;
             }

            if (MessageBox.Show("Are you sure you want to delete the category: " + listCategory.SelectedItems[0].Text
               + "?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteCategory();
                GetCategories();
                SelectCategory(0);
                SelectNote(0);
            }
        }

        // ///////////////////////////////////////////////////////////////////
        // note
        // ///////////////////////////////////////////////////////////////////
        private void newNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newText = "New Note";
            AddNote(newText);
            GetNotes();
            SelectNote(listNote.FindItemWithText(newText).Index);
            renameNoteToolStripMenuItem_Click(sender, e);
        }

        private void saveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((listCategory.SelectedItems.Count > 0) && (listNote.SelectedItems.Count > 0))
                SaveNote();
        }
        
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveNoteToolStripMenuItem_Click(sender, e);
        }

        private void renameNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listNote.SelectedItems[0].BeginEdit();
        }
        
        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the note: " + listNote.SelectedItems[0].Text
                + "?", "Delete Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteNote();
                GetNotes();
                SelectNote(0);
            }
        }

        // ///////////////////////////////////////////////////////////////////
        // print, import and export
        // ///////////////////////////////////////////////////////////////////
        private void exportCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((listCategory.SelectedItems.Count > 0) && (listNote.Items.Count > 0))
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
                        + @"\Downloads\Brainstorm\" + listCategory.SelectedItems[0].Text;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                for (int i = 0; i < listNote.Items.Count; i++)
                {
                    SelectNote(i);
                    textNote.SaveFile(path + @"\" + listNote.SelectedItems[0].Text + ".rtf");
                }
                MessageBox.Show("Files exported to:\n" + path, 
                    "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
			
        }
        
        private void importNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Personal;
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Select the folder where the .rtf files are stored.\n\n" 
                + "The category of the notes will be the folder name.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] dirs = fbd.SelectedPath.Split('\\');
                string category = dirs[dirs.Length - 1];

                // check if category exists and add it if it doesn't
                if(CategoryDoesNotExist(category))
                {
                    AddCategory(category);
                    GetCategories();
                }

                // select category
                SelectCategory(listCategory.FindItemWithText(category).Index);
                SelectNote(0);
                
                // for each file to import
                // check if note exits - rename it if it does
                // then add note
                foreach (string file in Directory.GetFiles(fbd.SelectedPath, "*.rtf"))
                {
                    string note = file.Replace(fbd.SelectedPath + "\\", "").Replace(".rtf", "");
                    if(NoteExists(note))
                    {
                        note += "-1";
                    }
                    
                    AddNote(note);
                    GetNotes();
                    SelectNote(listNote.FindItemWithText(note).Index);
                    textNote.LoadFile(file, RichTextBoxStreamType.RichText);
                    SaveNote();
                }
                GetNotes();
                SelectNote(0);
            }
        }


		private void exportToHMTLDatabseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string rtf = "", html = "";
			int id = 0, category_id = 0;
			string name = "", note_text = "";
			// get test text from database
			for (int i = 1; i < 109; i++)
			{
				StringBuilder query = new StringBuilder();
				query.Append("SELECT id, category_id, name, note_text ");
				query.Append("FROM notes ");
				query.Append("WHERE id=" + i);
				
				using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
				{
					using (SQLiteDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							if (dr.IsDBNull(0))
								continue;
							else
							{
								id = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
								category_id = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
								name = dr.IsDBNull(2) ? "" : dr.GetString(2);
								note_text = dr.IsDBNull(3) ? "" : dr.GetString(3);
							}
						}
					}
				}
				
				rtf = note_text;
				SautinSoft.RtfToHtml r = new SautinSoft.RtfToHtml();
				r.Encoding = SautinSoft.eEncoding.UTF_8;
				r.OutputFormat = SautinSoft.eOutputFormat.HTML_32;
				html = r.ConvertString(rtf);
				note_text = html;
								
				StringBuilder insert = new StringBuilder();
				insert.Append("INSERT into Notes_Backup (id, category_id, name, note_text) ");
				insert.Append("VALUES (" + id + ", " + category_id + ", '" + name.Replace("'", "''") + "', '" + note_text.Replace("'", "''") + "')");
				
				using (SQLiteCommand cmd = new SQLiteCommand(insert.ToString(), dbConnection))
				{
					using (SQLiteDataReader dr = cmd.ExecuteReader())
					{
						//MessageBox.Show(dr.ToString());
					}
				}
			}
		}

        // ///////////////////////////////////////////////////////////////////
        // exit
        // ///////////////////////////////////////////////////////////////////
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
