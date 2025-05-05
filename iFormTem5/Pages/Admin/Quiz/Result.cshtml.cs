using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iFormTem5.Pages.Quiz
{
    public class ResultModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ResultModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserAnswer> Answers { get; set; }
        public int TotalScore { get; set; }
        public int TotalAnswers => Answers?.Count ?? 0;

        public async Task<IActionResult> OnGetAsync(int userId, int templateId)
        {
            Answers = await _context.UserAnswers
                .Include(a => a.Question)
                .Where(a => a.UserId == userId.ToString() && a.TemplateId == templateId)
                .ToListAsync();

            TotalScore = Answers.Sum(a => a.Score);

            return Page();
        }
    }
}
