using LibraryManagementSystem2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagmentSystem2
{
    internal class MemberManager
    {
        private DB db;

        public MemberManager()
        {
            db = new DB();  
        }

        // Method to add a new member to the database using a Member object
        public bool AddMember(Member member)
        {
            try
            {
                db.OpenConnection();

                // SQL query to check if a member with the same name or username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username OR Name = @name";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.GetConnection()))
                {
                    checkCmd.Parameters.AddWithValue("@username", member.Username);
                    checkCmd.Parameters.AddWithValue("@name", member.Name);

                    // Execute the query and get the count of matching rows
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    // If count is greater than 0, the member already exists, throw an error or return false
                    if (count > 0)
                    {
                        throw new Exception("A member with the same username or name already exists.");
                    }
                }

                // SQL query to insert the new member into the Users table
                string query = "INSERT INTO Users (Name, Username, Password, UserRole) VALUES (@name, @username, @password, @userRole)";

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@username", member.Username);
                    cmd.Parameters.AddWithValue("@password", member.Password);
                    cmd.Parameters.AddWithValue("@userRole", "Member");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding member: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }


        // Method to update member details using a Member object
        public bool UpdateMemberDetails(Member member)
        {
            try
            {
                db.OpenConnection();

                // SQL query to update the member's details in the Users table
                string query = "UPDATE Users SET Name = @name, Username = @username, Password = @password WHERE UserID = @userId";

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@username", member.Username);
                    cmd.Parameters.AddWithValue("@password", member.Password);
                    cmd.Parameters.AddWithValue("@userId", member.UserID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating member details: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }


        // Method to fetch all members from the database and return as a DataTable
        public DataTable GetMembers()
        {
            DataTable membersTable = new DataTable();
            try
            {
                db.OpenConnection();

                // Query to fetch all members from the Users table
                string query = "SELECT UserID, name, Username, UserRole FROM Users WHERE UserRole = 'Member'";
                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(membersTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching members: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
            return membersTable;
        }


        // Method to remove a member from the database by User ID
        public bool RemoveMember(int userId)
        {
            try
            {
                db.OpenConnection();

                // SQL query to delete the member from the Users table
                string query = "DELETE FROM Users WHERE UserID = @userId";

               
                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    
                    cmd.Parameters.AddWithValue("@userId", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing member: " + ex.Message);  
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }


        //Get borrowing details of the logged-in member
        public DataTable GetBorrowedBooksForCurrentUser()
        {
            DataTable borrowedBooksTable = new DataTable();
            try
            {
                db.OpenConnection();

                // SQL query to fetch borrowed books, borrow date, return date, and calculate the late fee for the logged-in user
                string query = @"
        SELECT 
            br.BorrowID,
            br.BookID, 
            b.Title, 
            br.BorrowDate, 
            br.ReturnDate,
br.Returned,
            br.LateFee, 
            br.BorrowedCount
        FROM 
            Borrowing br
        INNER JOIN 
            Books b ON br.BookID = b.BookID
        INNER JOIN 
            Users u ON br.UserID = u.UserID
        WHERE 
            u.UserID = @userId";

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@userId", UserManager.CurrentUser.UserID); // Use the logged-in user's ID
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(borrowedBooksTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching borrowed books: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
            return borrowedBooksTable;
        }




    }

}
