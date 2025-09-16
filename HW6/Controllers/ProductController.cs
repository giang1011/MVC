using HW6.Models;
using HW6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HW6.Controllers
{
    public class ProductController : Controller
    {
        private List<Category> categories = new List<Category>
        {
            new Category{ Id=1, Name="Điện thoại"},
            new Category{ Id=2, Name="Laptop"},
            new Category{ Id=3, Name="Phụ kiện"},
        };

        private List<Product> products = new List<Product>
        {
            new Product{ Id=1, Name="iPhone 15", Price=25000000, Description="Điện thoại Apple mới nhất", ImageUrl="/images/iphone.jpg", CategoryId=1},
            new Product{ Id=2, Name="Samsung S23", Price=20000000, Description="Điện thoại Samsung cao cấp", ImageUrl="/images/samsung.jpg", CategoryId=1},
            new Product{ Id=3, Name="Macbook Air M2", Price=30000000, Description="Laptop Apple M2", ImageUrl="/images/macbook.jpg", CategoryId=2},
            new Product{ Id=4, Name="Chuột Logitech", Price=500000, Description="Chuột không dây", ImageUrl="/images/mouse.jpg", CategoryId=3},
        };

        public IActionResult Index(int? categoryId)
        {
            var vm = new ProductCategoryViewModel
            {
                Categories = categories,
                Products = categoryId == null
                           ? products
                           : products.Where(p => p.CategoryId == categoryId).ToList(),
                SelectedCategory = categoryId
            };

            return View(vm);
        }
    }
}
