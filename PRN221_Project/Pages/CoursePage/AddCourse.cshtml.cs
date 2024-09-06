using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.CoursePage
{
    public class AddCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public int CourseId { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public int ProfileId { get; set; }
        [BindProperty]
        public int SubjectId { get; set; }
        [BindProperty]
        public int NumberQuestion { get; set; }
        public AddCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var course = new Course
            {
                CourseId = CourseId,
                Title = Title,
                Description = Description,
                ProfileId = ProfileId,
                SubjectId = SubjectId,
                NumberQuestion = NumberQuestion
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToPage("./ListCourse");
        }
    }
}
