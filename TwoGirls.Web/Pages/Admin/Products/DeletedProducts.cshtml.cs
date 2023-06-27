using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    [PermissionChecker(20)]
    public class DeletedProductsModel : PageModel
    {
        private readonly IProductService _productService;
        public DeletedProductsModel(IProductService productService)
        {
            _productService = productService;
        }
        public ProductsForAdminViewModel ProductsForAdminViewModel { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            ProductsForAdminViewModel = _productService.GetDeletedProductsByFilterForAdminViewModel(pageId, filter);
            ProductsForAdminViewModel.Filter = filter;
        }
    }
}
