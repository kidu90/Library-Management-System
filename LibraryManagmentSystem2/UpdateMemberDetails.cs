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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryManagmentSystem2
{
    public partial class UpdateMemberDetails : Form
    {
        
        public UpdateMemberDetails()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                int userId = Convert.ToInt32(textBox2.Text);
                string name = textBox1.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // Create a new Member object with the gathered data
                Member member = new Member(userId.ToString(), name, "Member", username, password);

                MemberManager memberManager = new MemberManager();

                // Pass the member object to the UpdateMemberDetails method
                bool success = memberManager.UpdateMemberDetails(member);

                if (success)
                {
                    MessageBox.Show("Member details updated successfully.");
                }
                else
                {
                    MessageBox.Show("Error updating member details.");
                }
            }


        
    }
}
