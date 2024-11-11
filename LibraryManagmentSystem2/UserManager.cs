using LibraryManagmentSystem2;
using MySql.Data.MySqlClient;
using System;

namespace LibraryManagementSystem2
{
    internal class UserManager
    {
        private DB db = new DB();

        // Static property to store the current logged-in user
        public static User CurrentUser { get; private set; }

        // Method to validate the login credentials and set the current user if valid
        public bool ValidateLogin(string username, string password)
        {
            bool isValid = false;
            db.OpenConnection();

            try
            {
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve user details
                            string userId = reader["UserID"].ToString();
                            string name = reader["Name"].ToString();
                            string userRole = reader["UserRole"].ToString();

                            // Create the appropriate user object based on userRole
                            if (userRole == "Member")
                            {
                                CurrentUser = new Member(userId, name, userRole, username, password);
                            }
                            else if (userRole == "Librarian")
                            {
                                CurrentUser = new Librarian(userId, name, userRole, username, password);
                            }
                            else
                            {
                                // Handle unexpected roles if necessary
                                throw new Exception("Unknown user role.");
                            }

                            isValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating login: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return isValid;
        }
    }
}
