using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Categories
{
    [PermissionChecker(7)]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _productService.GetAllCategories();
        }
    }
}
