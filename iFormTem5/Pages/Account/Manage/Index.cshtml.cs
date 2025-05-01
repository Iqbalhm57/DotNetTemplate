using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace iFormTem5.Pages.Account.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            // For now, no logic needed.
        }
    }
}
