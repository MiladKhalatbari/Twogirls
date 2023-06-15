using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    public class RecoverModel : PageModel
    {
        IProductService _productService;
        public RecoverModel(IProductService productService)
        {
            _productService = productService;
        }
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productService.GetProductByIdIgnorequeryFilterIncludeImage(id);
        }
        public IActionResult OnPost(int id)
        {
            _productService.RecoverProduct(id);
            return RedirectToPage("index");
        }
    }
}
