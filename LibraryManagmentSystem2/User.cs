using System;

namespace LibraryManagementSystem2
{
    internal class User
    {
        private string userId;
        private string name;
        private string userRole;
        private string password;
        private string username;


        public string UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }

        // Property for Password
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        // Property for Username
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public User(string userId, string name, string userRole, string password, string username)
        {
            this.userId = userId;
            this.name = name;
            this.userRole = userRole;
            this.password = password;
            this.username = username;
        }
    }
}
