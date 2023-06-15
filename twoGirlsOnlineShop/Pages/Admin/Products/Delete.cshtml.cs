using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        IProductService _productService;
        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productService.GetProductByIdIncludeImage(id);
        }
        public IActionResult OnPost(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToPage("index");
        }
    }
}
