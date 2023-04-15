using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twoGirlsOnlineShop.Data;

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
            switch (id)
            {
                case "Discounted":
                    {
                        var model = _myContext.Products.Include(x => x.ImagePaths).Where(y => y.DiscountedPrice < y.SalesPrice).ToList();
                        ViewData["cornerImage"] = "/image/for-sale.png";
                        return View("/Views/Component/SliderComponent.cshtml", model);
                        
                    }
                case "NewArrival":
                    {
                        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                        var model = _myContext.Products.Include(x => x.ImagePaths).Where(y => y.PurchaseDate >= oneMonthAgo).ToList();
                        ViewData["cornerImage"] = "/image/new-arrical.png";
                        return View("/Views/Component/SliderComponent.cshtml", model);

                    }
                case "NewCollection":
                    {
                        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                        var model = _myContext.Products.Include(x => x.ImagePaths).Where(y => y.ReleaseDate >= oneMonthAgo).ToList();
                        ViewData["cornerImage"] = "/image/new-collection.png";
                        return View("/Views/Component/SliderComponent.cshtml", model);

                    }
                default: throw new NotImplementedException();
            }
           
        }
    }
}
