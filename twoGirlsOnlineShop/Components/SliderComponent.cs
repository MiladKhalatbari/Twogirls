using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Context;

namespace twoGirlsOnlineShop.Components
{
    public class SliderComponent : ViewComponent
    {
        private TwogirsContext _myContext;

        public SliderComponent(TwogirsContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(HttpContext.User.FindFirstValue(claimType: ClaimTypes.NameIdentifier));
                ViewData["Favorites"] = _myContext.Users.Where(x => x.Id == userId).Include(x => x.Favorites).SelectMany(x => x.Favorites).ToList();
            }
            switch (id)
            {
                case "Discounted":
                    {
                        var model = _myContext.Products.Include(x => x.ImagePaths).Include(x => x.Reviews).Where(y => y.DiscountedPrice < y.SalesPrice).ToList();
                        ViewData["cornerImage"] = "/image/for-sale.png";
                        return View("SliderComponent", model);
                        
                    }
                case "NewArrival":
                    {
                        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                        var model = _myContext.Products.Include(x => x.ImagePaths).Include(x => x.Reviews).Where(y => y.PurchaseDate >= oneMonthAgo).ToList();
                        ViewData["cornerImage"] = "/image/new-arrical.png";
                        return View("SliderComponent", model);

                    }
                case "NewCollection":
                    {
                        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                        var model = _myContext.Products.Include(x => x.ImagePaths).Include(x => x.Reviews).Where(y => y.ReleaseDate >= oneMonthAgo).ToList();
                        ViewData["cornerImage"] = "/image/new-collection.png";
                        return View("SliderComponent", model);

                    }
                default: throw new NotImplementedException();
            }
           
        }
    }
}
