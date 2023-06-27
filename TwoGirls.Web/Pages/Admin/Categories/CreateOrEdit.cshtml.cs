using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Categories
{
    [PermissionChecker(28)]
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public Category Category { get; set; }
        public IActionResult OnGet(int parentId = 0, int id = 0)
        {
            Category = new Category();
            if (id != 0)
            {
                Category = _productService.GetCategoryById(id);
            }
            else if (parentId != 0)
            {
                Category = new Category()
                {
                    ParentId = parentId
                };
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var category = new Category()
            {
                ParentId = Category.ParentId,
                Description = Category.Description,
                Name = Category.Name

            };
            if (Category.Id == 0)
            {
                _productService.AddCategory(category);
            }
            else
            {
                category.Id = Category.Id;
                _productService.UpdateCategory(category);
            }
            return RedirectToPage("index");
        }
    }
}
