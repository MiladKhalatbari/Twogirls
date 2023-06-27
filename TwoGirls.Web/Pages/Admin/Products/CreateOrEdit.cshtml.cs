using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Generator;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Products
{
    [PermissionChecker(17)]
    public class CreateOrEditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateOrEditModel(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            CreateOrEditProductForAdmin = new CreateOrEditProductForAdmin();
        }
        [BindProperty]
        public CreateOrEditProductForAdmin CreateOrEditProductForAdmin { get; set; }
        public void OnGet(int id = 0)
        {
            CreateOrEditProductForAdmin.Categories = _productService.GetAllCategories();
            CreateOrEditProductForAdmin.ProductTypes = _productService.GetAllProductType();

            if (id == 0)
            {
                CreateOrEditProductForAdmin.Product = new Product();
                CreateOrEditProductForAdmin.SelectedCategories = new List<int>();
            }
            else
            {
                CreateOrEditProductForAdmin.Product = _productService.GetProductByIdIncludeImage(id);
                CreateOrEditProductForAdmin.SelectedCategories = _productService.GetAllCategoriesOfProduct(id);
            }
        }
        public IActionResult OnPostSaveChanges()
        {
            if (!ModelState.IsValid || (CreateOrEditProductForAdmin.Product.Id == 0 && (CreateOrEditProductForAdmin.Files == null || CreateOrEditProductForAdmin.Files.Count() <= 1)))
            {
                ModelState.AddModelError("CreateOrEditProductForAdmin.Files", "product images are Required");
                CreateOrEditProductForAdmin.Categories = _productService.GetAllCategories();
                CreateOrEditProductForAdmin.SelectedCategories = new List<int>();

                return Page();
            }
            var productId = CreateOrEditProductForAdmin.Product.Id;
            var item = new Product()
            {
                QuantityInStock = CreateOrEditProductForAdmin.Product.QuantityInStock,
                Title = CreateOrEditProductForAdmin.Product.Title,
                DiscountedPrice = CreateOrEditProductForAdmin.Product.DiscountedPrice,
                PurchasePrice = CreateOrEditProductForAdmin.Product.PurchasePrice,
                Description = CreateOrEditProductForAdmin.Product.Description,
                SalesPrice = CreateOrEditProductForAdmin.Product.SalesPrice,
                ReleaseDate = CreateOrEditProductForAdmin.Product.ReleaseDate,
                ItemNumber = CreateOrEditProductForAdmin.Product.ItemNumber,
                PurchaseDate = CreateOrEditProductForAdmin.Product.PurchaseDate,
                ProductTypeId = CreateOrEditProductForAdmin.Product.ProductTypeId,
                IsDelete = false
            };
            if (productId == 0)
            {
                item.PurchaseDate = DateTime.Now;
                productId = _productService.AddProduct(item);
            }
            else
            {
                item.Id = CreateOrEditProductForAdmin.Product.Id;
                _productService.EditProduct(item);
                _productService.RemoveExistingProductCategories(item.Id);
            }
            if (CreateOrEditProductForAdmin.Files != null)
            {
                foreach (var img in CreateOrEditProductForAdmin.Files)
                {
                    if (img.IsImage())
                    {
                        var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                        var extension = Path.GetExtension(img.FileName);
                        var uniqueFileName = $"{fileName}_{NameGenerator.GenerateUniqueCode()}{extension}";
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "sunglasses", uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            img.CopyTo(fileStream);
                        }
                        var itemImg = new ImagePath()
                        {
                            ProductId = productId,
                            Url = "/image/sunglasses/" + uniqueFileName,
                            Name = uniqueFileName

                        };
                        _productService.AddImagePath(itemImg);
                    }
                }
            }

            foreach (var categoryId in CreateOrEditProductForAdmin.SelectedCategories)
            {
                _productService.AddCategoryToProduct(productId, categoryId);
            }
            return RedirectToPage("index");
        }
        public IActionResult OnPostPhotoDelete(int id)
        {

            var imagepath = _productService.GetImagePathById(id);
            if (_productService.TemporaryDeleteImagePath(imagepath))
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = imagepath.Url.TrimStart('/'); // Remove the leading forward slash
                var filePath = Path.Combine(webRootPath, imagePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return new JsonResult(new { success = true });

            }
            else
            {
                return new JsonResult(new { success = false });
            }
        }
    }
}
