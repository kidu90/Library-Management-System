using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem2
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author {  get; set; }
        public bool Availability {  get; set; }
        

        // Constructor to initialize the Book object
        public Book(int bookID, string title, string author, bool availability)
        {
            this.BookId = bookID; 
            Title = title;
            Author = author;
            Availability = availability;
        }

        // Overloaded constructor without the return date
        public Book(int bookID, string title, string author)
        {
            this.BookId = bookID;
            Title = title;
            Author = author;
        }





    }
}
