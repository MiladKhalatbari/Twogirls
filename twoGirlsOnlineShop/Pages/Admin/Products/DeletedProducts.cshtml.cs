using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    public class DeletedProductsModel : PageModel
    {
        IProductService _productService;
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
