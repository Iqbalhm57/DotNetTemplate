using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iFormTem5.Pages
{
    public class IndexModel : PageModel
    {
        public int UserId { get; set; }

        public User UserData { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int userId)
        {
            UserId = userId;
        }
    }
}
