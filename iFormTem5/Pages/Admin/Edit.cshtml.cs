using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iFormTem5.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.Users.FindAsync(id);
            if (User == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userInDb = await _context.Users.FindAsync(User.Id);
            if (userInDb == null)
                return NotFound();

            userInDb.UserName = User.UserName;
            userInDb.Email = User.Email;
            userInDb.Role = User.Role;
            userInDb.Isblocked = User.Isblocked;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/Homepage");
        }
    }
}
