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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibraryManagmentSystem2
{
    public partial class UserLogin : Form
    {
        UserManager UserManager = new UserManager();
        public UserLogin()
        {
            InitializeComponent();
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            
            string password = txtPassword.Text.Trim();
            string username = txtUsername.Text.Trim();

            if (UserManager.ValidateLogin(username, password))
            {
                // Access the current user and determine the role
                User currentUser = UserManager.CurrentUser;
                string role = currentUser.UserRole;

                if (role == "Librarian")
                {
                    // Open LibrarianDashboard
                    LibrarianDashboard librarianDashboard = new LibrarianDashboard();
                    librarianDashboard.Show();
                    this.Hide();
                }
                else if (role == "Member")
                {
                    // Open MemberDashboard
                    MemberDasboard memberDasboard = new MemberDasboard();
                    memberDasboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid User Role.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }


        }
    }
}
