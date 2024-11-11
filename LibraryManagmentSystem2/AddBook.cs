using LibraryManagementSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagmentSystem2
{
    public partial class AddBook : Form
    {
        
        public AddBook()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book = new Book(0, txtTitle.Text.Trim(), txtAuthor.Text.Trim(), true);
            // Use BookManager to add the book
            BookManager bookManager = new BookManager();
            bool success = bookManager.AddBook(book);

            
            if (success)
            {
                MessageBox.Show("Book added successfully!");
                txtTitle.Clear();
                txtAuthor.Clear();
            }
            else
            {
                MessageBox.Show("Failed to add the book.");
            }
        }

    }
}
