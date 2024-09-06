using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.CoursePage
{
    public class ListCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string? Search { get; set; }
        public List<Course> listCourse { get; set; }

        public ListCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? search)
        {
            Search = search;
            if (Search == null)
            {
                listCourse = await _context.Courses.ToListAsync();
            }
            else
            {
                listCourse = await _context.Courses.Where(i => !string.IsNullOrEmpty(i.Title) && i.Title.ToLower().Contains(search.ToLower().Trim())).ToListAsync();
            }
            return Page();
        }
    }
}
