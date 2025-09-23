using Libary.Data;
using Microsoft.AspNetCore.Mvc;
namespace Libary.Controllers
{
    public class BorrowController : Controller
    {
        public IActionResult Index()
        {
            return View(LibraryData.Borrows);
        }
    }
}
