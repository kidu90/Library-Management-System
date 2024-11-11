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
    public partial class ManageBooks : Form
    {
        BookManager bookManager = new BookManager();

        public ManageBooks()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.Show();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageBooks_Load(object sender, EventArgs e)
        {
            LoadBooksToDataGridView();  // Populate the DataGridView when the form loads
        }
        private void LoadBooksToDataGridView()
        {

            DataTable booksTable = bookManager.GetAllBooks();  
            dataGridView1.DataSource = booksTable; 
            
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            bookManager.UpdateBookAvailability(Convert.ToInt32(textBox1.Text), true);
            LoadBooksToDataGridView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bookManager.UpdateBookAvailability(Convert.ToInt32(textBox1.Text), false);
            LoadBooksToDataGridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bookManager.RemoveBook(Convert.ToInt32(textBox1.Text));
            LoadBooksToDataGridView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LibrarianDashboard librarianDashboard = new LibrarianDashboard();
            librarianDashboard.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateBookDetail updateBookDetail = new UpdateBookDetail();
            updateBookDetail.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewIssuedBooks viewIssuedBooks = new ViewIssuedBooks();
            viewIssuedBooks.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadBooksToDataGridView();
        }
    }
}
