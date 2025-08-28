using Microsoft.AspNetCore.Mvc;
using _4.Models;
using System.Collections.Generic;
using System.Linq;

namespace _4.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product{ Id=1, Name="Laptop", Price=1500 },
            new Product{ Id=2, Name="Smartphone", Price=800 },
            new Product{ Id=3, Name="Tablet", Price=500 }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
