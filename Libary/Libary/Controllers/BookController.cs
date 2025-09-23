
using Libary.Data;
using Libary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Libary.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index(string? search, int page = 1)
        {
            int pageSize = 5;
            var books = LibraryData.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                                       || b.Author.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var total = books.Count();
            var pagedBooks = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Search = search;

            return View(pagedBooks);
        }

        public IActionResult Details(int id)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = LibraryData.Categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            book.Id = LibraryData.Books.Max(b => b.Id) + 1;
            LibraryData.Books.Add(book);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            ViewBag.Categories = LibraryData.Categories;
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var existing = LibraryData.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existing == null) return NotFound();

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Year = book.Year;
            existing.Quantity = book.Quantity;
            existing.CategoryId = book.CategoryId;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == id);
            if (book != null) LibraryData.Books.Remove(book);
            return RedirectToAction("Index");
        }

        public IActionResult Borrow(int id)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null || book.Quantity <= 0)
                return BadRequest("Không thể mượn sách này!");

            ViewBag.Book = book;
            return View(new Borrow { BookId = id, BorrowDate = DateTime.Today });
        }

        [HttpPost]
        public IActionResult Borrow(Borrow borrow)
        {
            var book = LibraryData.Books.FirstOrDefault(b => b.Id == borrow.BookId);
            if (book == null || book.Quantity <= 0)
                return BadRequest("Không thể mượn sách này!");

            borrow.Id = LibraryData.Borrows.Any() ? LibraryData.Borrows.Max(b => b.Id) + 1 : 1;
            borrow.Book = book;
            LibraryData.Borrows.Add(borrow);
            book.Quantity--;

            return RedirectToAction("Index");
        }
    }
}