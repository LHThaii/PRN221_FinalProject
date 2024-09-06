using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PRN221_Project.Pages.QuestionPage
{
    public class ListQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string? Search { get; set; }
        public List<Question> ListQuestion { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public ListQuestionModel(PRN221Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string? search, int cid, int pageIndex, int pagesize)
        {
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 2;

            var query = _context.Questions.Where(i => i.CourseId == cid);
            ListQuestion = await query.Skip((pageIndex - 1) * pagesize)
                .Take(pagesize).ToListAsync();
            TotalPage = (int)(Math.Ceiling(query.ToList().Count / (double)pagesize));

            return Page();
        }
    }
}
