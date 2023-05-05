using Microsoft.AspNetCore.Mvc;
using TwoGirls.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using twoGirlsOnlineShop.Models;
using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Entities;
using TwoGirls.Core.Generator;

namespace twoGirlsOnlineShop.Controllers
{
    public class AdminController : Controller
    {

        private TwogirsContext _myContext;
        private IWebHostEnvironment _hostingEnvironment;
        public AdminController(TwogirsContext myContext, IWebHostEnvironment hostingEnvironment)
        {
            _myContext = myContext;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            var products = _myContext.Products.Include(x=> x.ImagePaths).ToList();
            return View(products);
        }

        #region Add Product
        [HttpGet]
        [Route("admin/AddProduct")]
        public IActionResult AddProduct()
        {
            AddOrEditProductViewModel addOrEditProduct = new AddOrEditProductViewModel()
            {
                Categories = _myContext.Categories.ToList(),
            };
            return View(addOrEditProduct);
        }


        [HttpPost]
        [Route("admin/AddProduct")]
        public IActionResult AddProduct(AddOrEditProductViewModel addOrEditProduct)
        {
            if (ModelState.IsValid)
            {
                var item = new Product()
                {
                    QuantityInStock = addOrEditProduct.QuantityInStock,
                    PurchaseDate = DateTime.Now,
                    Title = addOrEditProduct.Title,
                    DiscountedPrice = addOrEditProduct.DiscountedPrice,
                    PurchasePrice = addOrEditProduct.PurchasePrice,
                    Description = addOrEditProduct.Description,
                    SalesPrice = addOrEditProduct.SalesPrice,
                    ReleaseDate = addOrEditProduct.ReleaseDate
                };
                _myContext.Products.Add(item);
                _myContext.SaveChanges();
                var productId = item.Id;
                foreach (var img in addOrEditProduct.Files) { 
                
                    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    var extension = Path.GetExtension(img.FileName);
                    var uniqueFileName = $"{fileName}_{NameGenerator.GenerateUniqueCode()}{extension}";
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image" ,"sunglasses", uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    var itemImg = new ImagePath()
                    {
                        ProductId = productId,
                        Url = "/image/sunglasses/"+ uniqueFileName,
                        Name = uniqueFileName

                    };
                    _myContext.ImagePaths.Add(itemImg);
                    _myContext.SaveChanges();
                }


                foreach (var id in addOrEditProduct.SelectedCategories)
                {
                    var itemCagegory = new CategoryToProduct()
                    {
                        ProductId = productId,
                        CategoryId = id
                    };
                    _myContext.CategoryToProducts.Add(itemCagegory);
                    _myContext.SaveChanges();
                }
                return Redirect("index");
            }
            return View(addOrEditProduct);
        }

        #endregion


        #region Edit Product
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _myContext.Products.Include(x => x.ImagePaths)
                                               .Include(x => x.categoryToProdycts).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            AddOrEditProductViewModel addOrEdit = new AddOrEditProductViewModel()
            {
                PurchaseDate = product.PurchaseDate,
                PurchasePrice = product.PurchasePrice,
                DiscountedPrice = product.DiscountedPrice,
                SalesPrice = product.SalesPrice,
                Description = product.Description,
                Id = id,
                Title = product.Title,
                QuantityInStock = product.QuantityInStock,
                SelectedCategories = new List<int>(),
                Categories = _myContext.Categories.ToList(),
                Files = new List<IFormFile>()
            };

            foreach (var category in product.categoryToProdycts)
            {
                addOrEdit.SelectedCategories.Add(category.CategoryId);
            }
            foreach (var img in product.ImagePaths)
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                string path = Path.Combine(rootPath, "image", "sunglasses", img.Name);

                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    var file = new FormFile(fileStream, 0, new FileInfo(path).Length, null, Path.GetFileName(path))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };
                    addOrEdit.Files.Add(file);
                }
            }

            return View(addOrEdit);
        }
        [HttpPost]
        public IActionResult EditProduct(AddOrEditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var item = new Product()
                {
                    Id = product.Id,
                    QuantityInStock = product.QuantityInStock,
                    PurchaseDate = DateTime.Now,
                    Title = product.Title,
                    DiscountedPrice = product.DiscountedPrice,
                    PurchasePrice = product.PurchasePrice,
                    Description = product.Description,
                    SalesPrice = product.SalesPrice,
                    ReleaseDate = product.ReleaseDate
                };
                _myContext.Products.Update(item);
                _myContext.SaveChanges();
                //foreach (var img in product.Files)
                //{

                //    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                //    var extension = Path.GetExtension(img.FileName);
                //    var uniqueFileName = $"{fileName}_{Guid.NewGuid().ToString()}{extension}";
                //    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image","sunglasses", uniqueFileName);
                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        img.CopyTo(fileStream);
                //    }
                //    var itemImg = new ImagePath()
                //    {
                //        ProductId = product.Id,
                //        Url = Path.Combine("/image/sunglasses/", uniqueFileName)
                //    };
                //    _myContext.ImagePaths.Add(itemImg);
                //    _myContext.SaveChanges();
                //}

                var pro = _myContext.Products.Include(x=> x.categoryToProdycts).FirstOrDefault(x=> x.Id==product.Id);
                pro.categoryToProdycts = null;
                _myContext.SaveChanges();

                foreach (var id in product.SelectedCategories)
                {
                    var itemCagegory = new CategoryToProduct()
                    {
                        ProductId = product.Id,
                        CategoryId = id
                    };
                    _myContext.CategoryToProducts.Add(itemCagegory);
                    _myContext.SaveChanges();
                }
                var products = _myContext.Products.Include(x => x.ImagePaths).ToList();
                return View("index",products);
            }

            return View(product);
        }
        #endregion

        #region Remove Product
        public async Task<IActionResult> RemoveProduct(int? id)
        {
            if (id == null || _myContext.Products == null)
            {
                return NotFound();
            }

            var product = await _myContext.Products.
                Include(x=>x.ImagePaths)
                .FirstOrDefaultAsync(m => m.Id == id);
               if (product == null)
               {
                 return NotFound();
               }
               return View(product);
        }
        [HttpPost, ActionName("RemoveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProductConfirmed(int id)
        {
            if (_myContext.Products == null)
            {
                return Problem("Entity set 'TwogirsContext.Products'  is null.");
            }
            var product = await _myContext.Products.Include(x=>x.ImagePaths).FirstOrDefaultAsync(x=>x.Id==id);
            if (product != null)
            {
                var img = product.ImagePaths;
                if (img != null)
                {
                    foreach (var item in img)
                    {
                        string rootPath = _hostingEnvironment.WebRootPath;
                        string imagePath = Path.Combine(rootPath, "image", "sunglasses", item.Name);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        _myContext.ImagePaths.Remove(item);
                        
                    }
                }
                _myContext.SaveChanges();
                _myContext.Products.Remove(product);
            }

            await _myContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Detail Product

        public async Task<IActionResult> DetailProduct(int id)
        {
            var product = await _myContext.Products.Include(x => x.ImagePaths).FirstOrDefaultAsync(x => x.Id == id);
            return View(product);
        }
        #endregion

        #region Add Category
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _myContext.Categories.Add(category);
            _myContext.SaveChanges();
            return Redirect("index");
        }
        #endregion

        #region Remove Category
        public IActionResult RemoveCategory()
        {
            var categories = _myContext.Categories.ToList();
            return View(categories);
        }
        [HttpPost]
        public IActionResult RemoveCategory(int id)
        {
            var category = _myContext.Categories.Find(id);
            _myContext.Categories.Remove(category);
            _myContext.SaveChanges();
            return Redirect("index");
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
