using System;

namespace LibraryManagementSystem2
{
    internal class Librarian : User
    {
        // Constructor to initialize all fields including inherited properties
        public Librarian(string userId, string name, string userRole, string username, string password)
            : base(userId, name, userRole,password,username)
        {
            
        }

        // Overloaded constructor for partial initialization
        public Librarian(string name, string userRole, string username, string password)
            : base(null, name, userRole, password, username) 
        {
            
        }
    }
}
