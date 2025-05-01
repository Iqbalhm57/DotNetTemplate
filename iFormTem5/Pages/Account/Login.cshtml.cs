using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using iFormTem5.Models;
using Microsoft.EntityFrameworkCore;
using iFormTem5.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace iFormTem5.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (user == null || user.Password != Input.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
            if (user.Isblocked == true )
            {
                ModelState.AddModelError(string.Empty, "User Is Blocked.Contact Admin");
                return Page();
            }
            HttpContext.Session.SetString("UserName", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            if (user.Role == "Admin")
                {
                    return RedirectToPage("/Admin/Homepage");
                }
                // Redirect to your dashboard or homepage
                return RedirectToPage("/Templates/Index", new { userId = user.Id });
        }
    }
}
