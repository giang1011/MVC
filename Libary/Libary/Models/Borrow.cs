
using System;
using System.ComponentModel.DataAnnotations;

namespace Libary.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public string Borrower { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}