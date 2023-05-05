
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TwoGirls.DataLayer.Entities;
using TwoGirls.DataLayer.Context;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);

                if (model == null)
                {
                    var user = await _myContext.Users.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == userId);

                    model = new Card()
                    {
                        IsClose = false,
                        UserId = user.Id,
                        CreateDate = DateTime.Now
                    };

                    _myContext.Cards.Add(model);
                    _myContext.SaveChanges();
                    model = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);

                }

                return View("/Views/Component/CardComponent.cshtml", model);
            }
            else 
            {
                var model = new Card();
                return View("/Views/Component/CardComponent.cshtml", model);
            }
        }

    }



}
