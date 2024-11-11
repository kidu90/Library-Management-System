using LibraryManagementSystem;
using LibraryManagementSystem2;
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
    public partial class MemberDasboard : Form
    {
        public MemberDasboard()
        {
            InitializeComponent();
        }

        private void MemberDasboard_Load(object sender, EventArgs e)
        {
            LoadBooksToDataGridView();
            LoadBooksBorrowedToDataGridView();
           
        }

        private void LoadBooksToDataGridView()
        {
            BookManager bookManager = new BookManager();
            DataTable booksTable = bookManager.GetAllBooks();
            dataGridView1.DataSource = booksTable;
        }

        private void LoadBooksBorrowedToDataGridView()
        {
            MemberManager memberManager = new MemberManager();
            DataTable borrowedBooks = memberManager.GetBorrowedBooksForCurrentUser();
            dataGridView2.DataSource = borrowedBooks;

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int bookId = int.Parse(textBox1.Text.Trim());

            if (UserManager.CurrentUser != null)
            {
                string username = UserManager.CurrentUser.UserID; 
                BookManager bookManager = new BookManager();

                bool success = bookManager.BorrowBook(bookId, username);

                if (success)
                {
                    MessageBox.Show("Book borrowed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to borrow the book. Please check availability or your borrow limit.");
                }
            }
            else
            {
                MessageBox.Show("No member is currently logged in.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int bookId;
            string userId = UserManager.CurrentUser.UserID;// Get the user's ID from the session or login

            if (int.TryParse(textBox2.Text, out bookId))
            {
                BookManager bookManager = new BookManager();
                 bool success = bookManager.ReturnBook(bookId,userId);

                if (success)
                {
                    MessageBox.Show("Book returned successfully.");
                }
                else
                {
                    MessageBox.Show("Error: The book was not borrowed by this member or could not be returned.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Book ID.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadBooksToDataGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadBooksBorrowedToDataGridView();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
