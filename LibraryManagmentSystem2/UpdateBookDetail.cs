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
    public partial class UpdateBookDetail : Form
    {
        public UpdateBookDetail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve the book details from the textboxes
            int bookId = Convert.ToInt32(textBox1.Text);
            string title = textBox2.Text;
            string author = textBox3.Text;

            // Create a new Book object with the retrieved details
            Book book = new Book(bookId, title, author);
            BookManager bookManager = new BookManager();
            bool success = bookManager.UpdateBookDetails(book);
            if (success)
            {
                MessageBox.Show("Book details updated successfully!");
            }
            else
            {
                MessageBox.Show("Error updating book details.");
            }


        }

        private void UpdateBookDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
