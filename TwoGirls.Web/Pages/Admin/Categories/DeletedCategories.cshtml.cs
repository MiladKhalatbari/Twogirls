using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Categories
{
    [PermissionChecker(31)]
    public class DeletedCategoriesModel : PageModel
    {
        private readonly IProductService _productService;
        public DeletedCategoriesModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _productService.GetDeletedCategoriesIgnoreQueryFilters();
        }
    }
}
