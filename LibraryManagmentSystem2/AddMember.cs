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

namespace LibraryManagmentSystem2
{
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                MemberManager memberManager = new MemberManager();
                Member member = new Member(textBox1.Text, "Member", txtUsername.Text, txtPassword.Text);

                bool isAdded = memberManager.AddMember(member);

                if (isAdded)
                {
                    MessageBox.Show("Member added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add member.");
                }
            }






        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddMember_Load(object sender, EventArgs e)
        {

        }
    }
}
