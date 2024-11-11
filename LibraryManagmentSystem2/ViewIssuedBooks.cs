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
    public partial class ViewIssuedBooks : Form
    {
        public ViewIssuedBooks()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageBooks manageBooks = new ManageBooks();
            manageBooks.Show();
            this.Close();
        }

        private void ViewIssuedBooks_Load(object sender, EventArgs e)
        {
            LoadBorrwingsToDataGridView();
        }

        private void LoadBorrwingsToDataGridView()
        {
            BookManager bookManager = new BookManager();
            DataTable borrowingsTable = bookManager.GetAllBorrowings(); 
            dataGridView1.DataSource = borrowingsTable;  
        }
    }
}
