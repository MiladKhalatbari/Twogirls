using Microsoft.AspNetCore.Mvc;
using twoGirlsOnlineShop.Data;

namespace twoGirlsOnlineShop.Components
{
    public class CardItemComponent:ViewComponent
    {


        private TwogirsContext _myContext;

        public CardItemComponent(TwogirsContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_myContext.CardItems);
        }
    }
}
