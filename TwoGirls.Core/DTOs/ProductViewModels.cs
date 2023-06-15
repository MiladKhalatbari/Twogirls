using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.DTOs
{
    public class FilterProductViewModel
    {
        public int ProdcutTypeId { get; set; } = 0;
        public List<int> SelectedCategories { get; set; }= new List<int>();
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 100;
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public PaginationViewModel? PaginationViewModel { get; set; }
        public List<CategoryToProduct> categoryToProdycts { get; set; } = new();
        public List<ProductType> ProdcutTypes { get; set; } = new();
    }
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

    }

    public class FavoriteProductViewModel
    {
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 100;
        public List<Product> Products { get; set; } = new();
        public PaginationViewModel? PaginationViewModel { get; set; }
    }
}