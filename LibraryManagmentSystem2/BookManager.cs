using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using LibraryManagmentSystem2;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public class BookManager
    {
        private DB db;  

        public BookManager()
        {
            db = new DB();  
        }


        // Method to add a new book
        
        public bool AddBook(Book book)
        {
            try
            {
                db.OpenConnection();

                // SQL query to insert the book
                string query = "INSERT INTO Books (Title, Author, Availability) VALUES (@title, @author, @availability)";

                // Prepare SQL command
                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@author", book.Author);
                    cmd.Parameters.AddWithValue("@availability", book.Availability);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // If rowsAffected is greater than 0, the book was added successfully
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }





        // Method to get all books from the database
        public DataTable GetAllBooks()
        {
            DataTable booksTable = new DataTable();

            try
            {
                string query = "SELECT BookID, Title, Author, Availability FROM Books";  // Query to fetch book details
                db.OpenConnection();  

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(booksTable);  // Fill DataTable with book data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the books: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();  
            }

            return booksTable;

        }

        // Method to update book availability
        public bool UpdateBookAvailability(int bookID, bool availability)
        {
            try
            {
                // SQL query to update book availability
                string query = "UPDATE Books SET Availability = @Availability WHERE BookID = @BookID";
                db.OpenConnection();  // Open the connection

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                   
                    cmd.Parameters.AddWithValue("@Availability", availability);
                    cmd.Parameters.AddWithValue("@BookID", bookID);

                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error updating book availability: " + ex.Message);
                return false;
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }




        // Method to remove a book
        public bool RemoveBook(int bookID)
        {
            try
            {
                string query = "DELETE FROM Books WHERE BookID = @BookID";
                db.OpenConnection();  // Open the connection

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error removing book: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();  
            }
        }


        // Method to borrow a book
        public bool BorrowBook(int bookId, string userId)
        {
            try
            {
                db.OpenConnection();

                // Check if the member has already borrowed 3 books
                string checkLimitQuery = "SELECT COUNT(*) FROM Borrowing WHERE UserID = @userId AND Returned = FALSE";
                using (MySqlCommand checkLimitCmd = new MySqlCommand(checkLimitQuery, db.GetConnection()))
                {
                    checkLimitCmd.Parameters.AddWithValue("@userId", userId);
                    int borrowedBooks = Convert.ToInt32(checkLimitCmd.ExecuteScalar());

                    if (borrowedBooks >= 3)
                    {
                        return false; 
                    }
                }

                // Check if the book is available
                string checkAvailabilityQuery = "SELECT Availability FROM Books WHERE BookID = @bookId";
                using (MySqlCommand checkAvailabilityCmd = new MySqlCommand(checkAvailabilityQuery, db.GetConnection()))
                {
                    checkAvailabilityCmd.Parameters.AddWithValue("@bookId", bookId);
                    bool isAvailable = Convert.ToBoolean(checkAvailabilityCmd.ExecuteScalar());

                    if (!isAvailable)
                    {
                        return false; 
                    }
                }

                // Fetch the member's name from the Users table
                string fetchNameQuery = "SELECT Name FROM users WHERE UserID = @userId";
                string memberName = string.Empty;
                using (MySqlCommand fetchNameCmd = new MySqlCommand(fetchNameQuery, db.GetConnection()))
                {
                    fetchNameCmd.Parameters.AddWithValue("@userId", userId);
                    memberName = fetchNameCmd.ExecuteScalar().ToString();
                }

                // Insert the borrowing record with UserID and Name
                string borrowQuery = "INSERT INTO Borrowing (BookID, UserID, Name, BorrowDate, ReturnDate, LateFee) VALUES (@bookId, @userId, @name, @borrowDate, @returnDate, @lateFee)";
                using (MySqlCommand borrowCmd = new MySqlCommand(borrowQuery, db.GetConnection()))
                {
                    borrowCmd.Parameters.AddWithValue("@bookId", bookId);
                    borrowCmd.Parameters.AddWithValue("@userId", userId);
                    borrowCmd.Parameters.AddWithValue("@name", memberName);
                    borrowCmd.Parameters.AddWithValue("@borrowDate", DateTime.Now);
                    borrowCmd.Parameters.AddWithValue("@returnDate", DateTime.Now.AddDays(7)); // Return date 7 days from now
                    borrowCmd.Parameters.AddWithValue("@lateFee", 0); // Initial late fee is 0

                    borrowCmd.ExecuteNonQuery();
                }

                // Update BorrowedCount in the Borrowing table for the user
                string updateCountQuery = "UPDATE Borrowing SET BorrowedCount = (SELECT COUNT(*) FROM Borrowing WHERE UserID = @userId AND Returned = FALSE) WHERE UserID = @userId";
                using (MySqlCommand updateCountCmd = new MySqlCommand(updateCountQuery, db.GetConnection()))
                {
                    updateCountCmd.Parameters.AddWithValue("@userId", userId);
                    updateCountCmd.ExecuteNonQuery();
                }

                // Mark the book as unavailable
                string updateBookQuery = "UPDATE Books SET Availability = FALSE WHERE BookID = @bookId";
                using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, db.GetConnection()))
                {
                    updateBookCmd.Parameters.AddWithValue("@bookId", bookId);
                    updateBookCmd.ExecuteNonQuery();
                }

                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error borrowing book: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }


        // Method to return a book
        public bool ReturnBook(int bookId, string userId)
        {
            try
            {
                db.OpenConnection();

                // Fetch the return date for the borrowed book
                string fetchBorrowingQuery = "SELECT ReturnDate FROM Borrowing WHERE BookID = @bookId AND UserID = @userId AND Returned = FALSE";
                DateTime returnDate;
                using (MySqlCommand fetchBorrowingCmd = new MySqlCommand(fetchBorrowingQuery, db.GetConnection()))
                {
                    fetchBorrowingCmd.Parameters.AddWithValue("@bookId", bookId);
                    fetchBorrowingCmd.Parameters.AddWithValue("@userId", userId);
                    var result = fetchBorrowingCmd.ExecuteScalar();
                    if (result == null)
                    {
                        return false; // The book was not borrowed by this member or record not found
                    }
                    returnDate = Convert.ToDateTime(result);
                }

                double lateFee = 0;
                if (DateTime.Now > returnDate)
                {
                    TimeSpan overdueDays = DateTime.Now - returnDate;
                    lateFee = overdueDays.Days * 50; // 50 rupees per day late fee
                }

                // Update the borrowing record with the late fee and mark as returned
                string updateBorrowingQuery = "UPDATE Borrowing SET Returned = TRUE, LateFee = @lateFee WHERE BookID = @bookId AND UserID = @userId AND Returned = FALSE";
                using (MySqlCommand updateBorrowingCmd = new MySqlCommand(updateBorrowingQuery, db.GetConnection()))
                {
                    updateBorrowingCmd.Parameters.AddWithValue("@bookId", bookId);
                    updateBorrowingCmd.Parameters.AddWithValue("@userId", userId);
                    updateBorrowingCmd.Parameters.AddWithValue("@lateFee", lateFee);
                    int rowsAffected = updateBorrowingCmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return false; // No rows affected, possibly an issue with the return
                    }
                }

                // Mark the book as available
                string updateBookQuery = "UPDATE Books SET Availability = TRUE WHERE BookID = @bookId";
                using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, db.GetConnection()))
                {
                    updateBookCmd.Parameters.AddWithValue("@bookId", bookId);
                    updateBookCmd.ExecuteNonQuery();
                }

                // Decrement the BorrowedCount for the user
                string decrementCountQuery = "UPDATE Borrowing SET BorrowedCount = BorrowedCount - 1 WHERE UserID = @userId AND BorrowedCount > 0";
                using (MySqlCommand decrementCountCmd = new MySqlCommand(decrementCountQuery, db.GetConnection()))
                {
                    decrementCountCmd.Parameters.AddWithValue("@userId", userId);
                    decrementCountCmd.ExecuteNonQuery();
                }

                return true; // Book successfully returned
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error returning book: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }


        // Method to update book details
        public bool UpdateBookDetails(Book book)
        {
            try
            {
                db.OpenConnection();
                string query = "UPDATE Books SET Title = @Title, Author = @Author WHERE BookID = @BookID";
                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@bookId", book.BookId); 
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book details: " + ex.Message);
                return false;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public DataTable GetAllBorrowings()
        {
            DataTable borrowingsTable = new DataTable();  

            try
            {
                string query = "SELECT BorrowID, BookID, UserID, BorrowDate, ReturnDate, Returned, LateFee, Name, BorrowedCount FROM borrowing";  // Query to fetch borrowing details
                db.OpenConnection();  

                using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(borrowingsTable);  // Fill DataTable with borrowing data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the borrowings: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();  
            }

            return borrowingsTable;
        }




    }
}
