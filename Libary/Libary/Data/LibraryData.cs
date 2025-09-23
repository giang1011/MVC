using Libary.Models;

namespace Libary.Data
{
    public static class LibraryData
    {
        public static List<Category> Categories { get; set; } = new List<Category>()
        {
            new Category { Id = 1, Name = "Khoa học" },
            new Category { Id = 2, Name = "Văn học" },
            new Category { Id = 3, Name = "Thiếu nhi" }
        };

        public static List<Book> Books { get; set; } = new List<Book>()
        {
            new Book { Id = 1, Title = "Vũ trụ trong vỏ hạt dẻ", Author="Stephen Hawking", Year=2001, Quantity=3, CategoryId=1 },
            new Book { Id = 2, Title = "Dế mèn phiêu lưu ký", Author="Tô Hoài", Year=1941, Quantity=5, CategoryId=3 },
            new Book { Id = 3, Title = "Truyện Kiều", Author="Nguyễn Du", Year=1820, Quantity=2, CategoryId=2 }
        };

        public static List<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}