using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace iFormTem5.Pages.Quiz
{
    public class TakeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TakeModel(ApplicationDbContext context)   
        {
            _context = context;
        }

        public Template Template { get; set; }

        [BindProperty]
        public List<Question> Questions { get; set; }

        [BindProperty]
        public List<UserAnswer> UserAnswers { get; set; }

        [BindProperty(SupportsGet = true)]
        public int TemplateId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int templateId, int userId)
        {
            Template = await _context.Templates
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == templateId);

            if (Template == null)
                return NotFound();

            Questions = Template.Questions.ToList();
            UserAnswers = Questions.Select(q => new UserAnswer { QuestionId = q.Id }).ToList();
            UserId = userId;
            TemplateId = templateId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var template = await _context.Templates
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == TemplateId);

            if (template == null)
                return NotFound();

            foreach (var answer in UserAnswers)
            {
                var question = template.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                answer.Question = question;
                answer.TemplateId = TemplateId; // Ensure valid foreign key
                answer.UserId = UserId.ToString();
                answer.SubmittedAt = DateTime.UtcNow;
                answer.Score = Normalize(answer.Answer) == Normalize(question.CorrectAnswer)
                    ? question.PointValue
                    : 0;

                _context.UserAnswers.Add(answer);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Result", new { userId = UserId, templateId = TemplateId });
        }

        private string Normalize(string input)
        {
            return input?.Trim().ToLowerInvariant() ?? "";
        }
    }
}
