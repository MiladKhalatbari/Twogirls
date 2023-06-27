using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Components
{
    public class MainMenuComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public MainMenuComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var fullname = User.Identity.Name.FirstOrDefault().ToString() + HttpContext.User.FindFirstValue(ClaimTypes.Surname).FirstOrDefault();
                ViewData["username"] = fullname;
            }
            ViewData["ProductType"] = _productService.GetAllProductType();

            return await Task.FromResult(View("MainMenuComponent", _productService.GetAllCategories()));

        }

    }
}
