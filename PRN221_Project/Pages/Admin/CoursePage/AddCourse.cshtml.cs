using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.CoursePage
{
    public class AddCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Course Course { get; set; }
        [BindProperty]
        public int profileId { get; set; }
        public List<Subject> listSub { get; set; }

        public AddCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            profileId = id;
            listSub = await _context.Subjects.Where(i => i.ProfileId == id).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var newCourse = new Course
            {
                Title = Course.Title,
                Description = Course.Description,
                NumberQuestion = Course.NumberQuestion,
                SubjectId = Course.SubjectId,
                Status = Course.Status,
                ProfileId = profileId,
            };
            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/CoursePage/ListCourse", new { id = profileId });
        }
    }
}
