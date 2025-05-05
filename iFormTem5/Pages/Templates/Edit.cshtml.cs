using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iFormTem5.Pages.Templates
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Template Template { get; set; }

        [BindProperty]
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int userId)
        {
            Template = await _context.Templates
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Template == null)
                return NotFound();

            UserId = userId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingTemplate = await _context.Templates
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == Template.Id);

            if (existingTemplate == null)
                return NotFound();

            // Update scalar properties
            existingTemplate.Title = Template.Title;
            existingTemplate.Description = Template.Description;
            existingTemplate.Topic = Template.Topic;
            existingTemplate.IsPublic = Template.IsPublic;

            // Replace questions
            existingTemplate.Questions.Clear();
            foreach (var q in Template.Questions)
            {
                if (!string.IsNullOrWhiteSpace(q.Text))
                {
                    existingTemplate.Questions.Add(new Question
                    {
                        Text = q.Text,
                        CorrectAnswer = q.CorrectAnswer,
                        PointValue = q.PointValue
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index", new { userId = UserId });
        }
    }
}
