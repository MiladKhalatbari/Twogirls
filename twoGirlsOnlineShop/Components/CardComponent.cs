
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using twoGirlsOnlineShop.Data;
using twoGirlsOnlineShop.Models;
namespace twoGirlsOnlineShop.Components
{
    public class CardComponent : ViewComponent
    {
        private TwogirsContext _myContext;

        public CardComponent(TwogirsContext myContext)
        {
            _myContext = myContext;
        }

        //  public  async Task<IViewComponentResult> InvokeAsync()
        //    {
        //        var model = new Card();
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        //            model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);

        //            if (model == null)
        //            {
        //                var _user = _myContext.Users.Include(x => x.Cards).FirstOrDefault(x => x.Id == userId);

        //                Card card1 = new Card()
        //                {
        //                    IsClose = false,
        //                    UserId = _user.Id,
        //                    CreateDate = DateTime.Now
        //                };
        //                _user.Cards.Add(card1);
        //                _myContext.SaveChanges();
        //                model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == 1 && x.IsClose == false);
        //            }
        //        }
        //        return View("/Views/Component/CardComponent.cshtml",model);
        //    }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new Card();
            if (User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);

                if (model == null)
                {
                    var user = await _myContext.Users.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == userId);

                    model = new Card()
                    {
                        IsClose = false,
                        UserId = user.Id,
                        CreateDate = DateTime.Now
                    };

                    user.Cards.Add(model);
                     _myContext.SaveChanges();
                    model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);

                }
            }

            return View("/Views/Component/CardComponent.cshtml", model);
        }

    }



}
