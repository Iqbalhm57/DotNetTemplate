using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFormTem5.Pages.Templates
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Template Template { get; set; }

        [BindProperty]
        public List<Question> Questions { get; set; } = new();

        public int UserId { get; set; }

        public void OnGet(int userId)
        {
            UserId = userId;
            Template = new Template
            {
                UserId = userId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Template.Questions = Questions;

            _context.Templates.Add(Template);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Templates/Index", new { userId = Template.UserId });
        }
    }
}
