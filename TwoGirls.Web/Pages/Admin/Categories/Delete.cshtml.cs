using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Categories
{
    [PermissionChecker(29)]
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _productService.GetCategoryById(id);
        }
        public IActionResult OnPost(int id)
        {
            _productService.DeleteCategory(id);
            return RedirectToPage("index");
        }
    }
}
