﻿using System;
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
    public partial class LibrarianDashboard : Form
    {
        public LibrarianDashboard()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          ManageBooks manageBooks = new ManageBooks();
          manageBooks.Show();
          this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageMembers manageMembers = new ManageMembers();
            manageMembers.Show();
            this.Close();
           
        }

        private void LibrarianDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
