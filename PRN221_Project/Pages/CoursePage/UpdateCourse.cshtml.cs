using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System;

namespace PRN221_Project.Pages.CoursePage
{
    public class UpdateCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public UpdateCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int courId)
        {
            Course = await _context.Courses.Include(i => i.Subject).FirstOrDefaultAsync(i => i.CourseId == courId);
            if (Course == null) throw new ArgumentException("Can not find Course !");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var c = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == Course.CourseId);
            c.Title = Course.Title;
            c.Description = Course.Description;
            c.NumberQuestion = Course.NumberQuestion;
            c.Status = Course.Status;
            await _context.SaveChangesAsync();
            return RedirectToPage("./ListCourse");
        }
    }
}
