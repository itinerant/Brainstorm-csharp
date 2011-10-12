using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Brainstorm
{
    public partial class MainWindow : Form
    {
        private static SQLiteConnection dbConnection;

         private void OpenDatabase()
         {
            SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder();
            csb.Add("data source", "brainstorm.sqlite");
            dbConnection = new SQLiteConnection(csb.ConnectionString);
            dbConnection.Open();
         }

         private void CloseDatabase()
         {
             dbConnection.Close();
         }

         // ///////////////////////////////////////////////////////////////////
         // Get operations
         // ///////////////////////////////////////////////////////////////////   
         private void GetCategories()
         {
             listCategory.Items.Clear();

             StringBuilder query = new StringBuilder();
             query.Append("SELECT name ");
             query.Append("FROM categories ");
             query.Append("ORDER BY upper(name)");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 using (SQLiteDataReader dr = cmd.ExecuteReader())
                 {
                     while (dr.Read())
                     {
                         ListViewItem item = new ListViewItem(dr.GetString(0));
                         listCategory.Items.Add(item);
                         //System.Diagnostics.Debug.WriteLine(item);
                     }
                 }
             }
         }

         private void GetNotes()
         {
             listNote.Items.Clear();

             StringBuilder query = new StringBuilder();
             query.Append("SELECT name ");
             query.Append("FROM notes ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("ORDER BY upper(name)");
             //System.Diagnostics.Debug.WriteLine(query.ToString());
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 using (SQLiteDataReader dr = cmd.ExecuteReader())
                 {
                     while (dr.Read())
                     {
                         ListViewItem item = new ListViewItem(dr.GetString(0));
                         listNote.Items.Add(item);
                     }
                 }
             }
         }

         private void GetNoteText()
         {
             StringBuilder query = new StringBuilder();
             query.Append("SELECT note_text ");
             query.Append("FROM notes ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("AND name = '" + currentNote.Replace("'", "''") + "' ");
             //System.Diagnostics.Debug.WriteLine(query.ToString());
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 using (SQLiteDataReader dr = cmd.ExecuteReader())
                 {
                     while (dr.Read())
                     {
                         textNote.Rtf = dr.IsDBNull(0) ? "" : dr.GetString(0);
                     }
                 }
             }
         }

         // ///////////////////////////////////////////////////////////////////
         // Rename and Save operations
         // ///////////////////////////////////////////////////////////////////
         private void AddCategory(string category)
         {
             StringBuilder query = new StringBuilder();
             query.Append("INSERT into categories (id, name) ");
             query.Append("values ((select max(id) from categories)+1, '" 
                 + category.Replace("'", "''") + "')");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         }
        
        private void RenameCategory()
         {
             StringBuilder query = new StringBuilder();
             query.Append("UPDATE categories ");
             query.Append("SET name = '" + renamedCategory.Replace("'", "''") + "' ");
             query.Append("WHERE name = '" + selectedCategory.Replace("'", "''") + "' ");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         }

        private void AddNote(string note)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT into notes (id, category_id, name, note_text) ");
            query.Append("values ((select max(id) from notes)+1, "
                + "(select id from categories where name = '" + currentCategory.Replace("'", "''") 
                + "'), '" + note.Replace("'", "''") + "', '')");
            //System.Diagnostics.Debug.WriteLine(query.ToString());
            using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

         private void RenameNote()
         {
             StringBuilder query = new StringBuilder();
             query.Append("UPDATE notes ");
             query.Append("SET name = '" + renamedNote.Replace("'", "''") + "' ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("AND name = '" + selectedNote.Replace("'", "''") + "' "); 
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         } 
        
         private void SaveNote()
         {
            StringBuilder query = new StringBuilder();
             query.Append("UPDATE notes ");
             query.Append("SET note_text = '" + textNote.Rtf.Replace("'", "''") + "' ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("AND name = '" + currentNote.Replace("'", "''") + "' ");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         }

         // ///////////////////////////////////////////////////////////////////
         // Delete operations
         // ///////////////////////////////////////////////////////////////////
         private void DeleteNote()
         {
             StringBuilder query = new StringBuilder();
             query.Append("DELETE from notes ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("AND name = '" + currentNote.Replace("'", "''") + "' ");
             //System.Diagnostics.Debug.WriteLine(query.ToString());
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         }

         private void DeleteCategory()
         {
             StringBuilder query = new StringBuilder();
             query.Append("DELETE from categories ");
             query.Append("WHERE name = '" + currentCategory.Replace("'", "''") + "'");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 cmd.ExecuteNonQuery();
             }
         }

        // ///////////////////////////////////////////////////////////////////
        // Exists? operations
        // ///////////////////////////////////////////////////////////////////
         private bool CategoryDoesNotExist(string category)
         {
             StringBuilder query = new StringBuilder();
             query.Append("SELECT name ");
             query.Append("FROM categories ");
             query.Append("WHERE name = '" + category.Replace("'", "''") + "'");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 using (SQLiteDataReader dr = cmd.ExecuteReader())
                 {
                     dr.Read();
                     if (dr.HasRows)
                         return false;
                 }
             }
             return true;
         }

         private bool NoteExists(string note)
         {
             StringBuilder query = new StringBuilder();
             query.Append("SELECT name ");
             query.Append("FROM notes ");
             query.Append("WHERE category_id=(select id from categories where name = '"
                 + currentCategory.Replace("'", "''") + "') ");
             query.Append("AND name = '" + note.Replace("'", "''") + "' ");
             using (SQLiteCommand cmd = new SQLiteCommand(query.ToString(), dbConnection))
             {
                 using (SQLiteDataReader dr = cmd.ExecuteReader())
                 {
                     dr.Read();
                     if (dr.HasRows)
                        return true;
                 }
             }
             return false;
         }
    }
}