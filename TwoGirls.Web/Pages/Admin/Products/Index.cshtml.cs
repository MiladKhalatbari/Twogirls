using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    [PermissionChecker(4)]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public ProductsForAdminViewModel ProductsForAdminViewModel { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            ProductsForAdminViewModel = _productService.GetProductsByFilterForAdminViewModel(pageId, filter);
            ProductsForAdminViewModel.Filter = filter;
        }
    }
}
