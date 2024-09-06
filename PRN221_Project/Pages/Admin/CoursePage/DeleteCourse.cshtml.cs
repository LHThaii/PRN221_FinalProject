using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.CoursePage
{
    public class DeleteCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        public DeleteCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, int pid)
        {
            var Course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            _context.Courses.Remove(Course);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/CoursePage/ListCourse", new { id = pid });
        }
    }
}
