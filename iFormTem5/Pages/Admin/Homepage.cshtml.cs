using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iFormTem5.Pages.Admin
{
    public class HomepageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomepageModel(ApplicationDbContext context,
                             SignInManager<ApplicationUser> signInManager,
                             UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;

        }

        public List<User> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostBulkActionAsync(string action, List<int> selectedUserIds)
        {
            if (string.IsNullOrEmpty(action) || selectedUserIds == null || selectedUserIds.Count == 0)
                return RedirectToPage();

            var users = await _context.Users.Where(u => selectedUserIds.Contains(u.Id)).ToListAsync();

            var currentUserId = _userManager.GetUserId(User);
            bool selfDemoted = false;

            foreach (var user in users)
            {
                switch (action.ToLower())
                {
                    case "block":
                        user.Isblocked = true;
                        break;
                    case "unblock":
                        user.Isblocked = false;
                        break;
                    case "makeadmin":
                        user.Role = "Admin";
                        break;
                    case "removeadmin":
                        if (user.Role == "Admin")
                        {
                            user.Role = "User";
                            
                            var currentUserIdStr = _contextAccessor.HttpContext.Session.GetString("UserId");

                            if (int.TryParse(currentUserIdStr, out int CuurentUserId))
                            {
                                if (user.Id == CuurentUserId)
                                {
                                    selfDemoted = true;

                                }
                            }
                            

                        }
                        break;
                }
            }

            await _context.SaveChangesAsync();

            if (selfDemoted)
            {
                HttpContext.Session.Clear();
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Index"); // or redirect to login page
            }

            return RedirectToPage();
        }
    }
}