using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.CoursePage
{
    public class DeleteCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        public DeleteCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int courId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(i => i.CourseId == courId);
            var course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == courId);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();


            return RedirectToPage("./ListCourse");
        }
    }
}
