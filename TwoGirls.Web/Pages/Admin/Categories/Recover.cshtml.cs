using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Categories
{
    [PermissionChecker(30)]
    public class RecoverModel : PageModel
    {
        private readonly IProductService _productService;
        public RecoverModel(IProductService productService)
        {
            _productService = productService;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _productService.GetCategoryByIdIgnoreQueryFilters(id);
        }
        public IActionResult OnPost(int id)
        {
            _productService.RecoverCategory(id);
            return RedirectToPage("index");
        }
    }
}
