using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.CoursePage
{
    public class UpdateCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty]
        public int? profileId { get; set; }
        public List<Subject> listSub { get; set; }
        public UpdateCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, int pid)
        {
            Course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            profileId = pid;
            if (Course == null)
            {
                return NotFound();
            }
            listSub = await _context.Subjects.Where(i => i.ProfileId == pid).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var s = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == Course.CourseId);
            s.Title = Course.Title;
            s.Description = Course.Description;
            s.Status = Course.Status;
            s.NumberQuestion = Course.NumberQuestion;
            s.SubjectId = Course.SubjectId;
            s.ProfileId = (int)profileId;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/CoursePage/ListCourse", new { id = profileId });
        }
    }
}
