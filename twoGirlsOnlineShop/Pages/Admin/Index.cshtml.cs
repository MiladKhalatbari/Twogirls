using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;

namespace twoGirlsOnlineShop.Pages.Admin
{
    [PermissionCheckerAttribute(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
