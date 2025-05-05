using iFormTem5.Data;
using iFormTem5.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Template> PublicTemplates { get; set; } = new();

    public async Task OnGetAsync()
    {
        PublicTemplates = await _context.Templates
            .Include(t => t.User)
            .Where(t => t.IsPublic)
            .OrderByDescending(t => t.CreatedAt)
            .Take(6) //latest 5 templates 
            .ToListAsync();
    }
}
