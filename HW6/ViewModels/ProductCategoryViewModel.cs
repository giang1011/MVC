using HW6.Models;
using System.Collections.Generic;

namespace HW6.ViewModels
{
    public class ProductCategoryViewModel
    {
        public List<Category> Categories { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public int? SelectedCategory { get; set; }
    }
}
