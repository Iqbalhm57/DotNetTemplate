using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
//using static iFormTem5.Pages.Admin.UserManagementModel;

namespace iFormTem5.Pages.Templates
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public Dictionary<int, bool> HasTakenQuiz { get; set; } = new();

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Template> Templates { get; set; }
        [BindProperty]
        public List <User> UserData { get; set; }

        public User CurrentUserData { get; set; }
        public int UserId { get; set; }

        public async Task OnGetAsync(int? userId)
        {
            if (userId == null)
            {
                RedirectToPage("/Account/Login");
                return;
            }

            UserId = userId.Value;
            UserData = await _context.Users.ToListAsync();
            CurrentUserData = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);

            if (CurrentUserData == null)
            {
                RedirectToPage("/Account/Login");
                return;
            }

            if (CurrentUserData.Role == "Admin")
            {
                Templates = await _context.Templates.ToListAsync();
            }
            else
            {
                Templates = await _context.Templates
                    .Where(t => t.UserId == UserId || t.IsPublic == true)
                    .ToListAsync();
            }

            // Check which templates this user has answered
            var answeredTemplateIds = await _context.UserAnswers
                .Where(ua => ua.UserId == UserId.ToString())
                .Select(ua => ua.TemplateId)
                .Distinct()
                .ToListAsync();

            foreach (var template in Templates)
            {
                HasTakenQuiz[template.Id] = answeredTemplateIds.Contains(template.Id);
            }
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template != null)
            {
                _context.Templates.Remove(template);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
