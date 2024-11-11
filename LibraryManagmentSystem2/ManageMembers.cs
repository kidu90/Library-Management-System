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
    public partial class ManageMembers : Form
    {
        public ManageMembers()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LibrarianDashboard librarianDashboard = new LibrarianDashboard();
            librarianDashboard.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMember addMember = new AddMember();
            addMember.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateMemberDetails updateMemberDetails = new UpdateMemberDetails();
            updateMemberDetails.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageMembers_Load(object sender, EventArgs e)
        {
            LoadMembersToDataGridView();
        }
        private void LoadMembersToDataGridView()
        {

           
            MemberManager memberManager = new MemberManager();

            try
            {
                // Fetch the members and bind to DataGridView
                dataGridView1.DataSource = memberManager.GetMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }


        }



        private void button3_Click(object sender, EventArgs e)
        {
            MemberManager memberManager = new MemberManager();
            memberManager.RemoveMember(Convert.ToInt32(textBox1.Text));
            LoadMembersToDataGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadMembersToDataGridView();
        }
    }
    }

