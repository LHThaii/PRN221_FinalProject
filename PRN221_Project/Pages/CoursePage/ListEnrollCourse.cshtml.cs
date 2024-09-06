using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.CoursePage
{
    public class ListEnrollCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string? Search { get; set; }
        public List<ProfileCourse> listProfileCourse { get; set; }
        public ListEnrollCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? search, int id)
        {
            listProfileCourse = await _context.ProfileCourses.Include(i => i.Profile).Include(i => i.Course).Where(i => i.ProfileId == id).ToListAsync();
            return Page();
        }
    }
}
