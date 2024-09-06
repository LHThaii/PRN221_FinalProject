using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System;

namespace PRN221_Project.Pages.QuestionPage
{
    public class ViewAllQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string? Search { get; set; }
        public List<Question> listAllQuestion { get; set; }

        public ViewAllQuestionModel(PRN221Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string? search, int cid)
        {
            listAllQuestion = await _context.Questions.Include(i => i.Course).ToListAsync();
            return Page();
        }
    }
}
