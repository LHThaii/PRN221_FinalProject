using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project.Pages.CoursePage
{
    public class CourseDetailsModel : PageModel
    {
        private readonly PRN221Context _context;

        public Course Course { get; set; }
        public Subject Subject { get; set; }
        public CourseDetailsModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == Course.SubjectId);
            if (Subject == null) throw new ArgumentException("Can not find subjet !");
            return Page();
        }

        public async Task<IActionResult> OnGetEnrolled(int id, int studentId)
        {
            var coure = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == coure.SubjectId);
            Course = coure;
            if (coure == null) throw new ArgumentException("Can not find");
            coure.IsEnrolled += 1;
            await _context.SaveChangesAsync();

            var courePro = new ProfileCourse
            {
                CourseId = coure.CourseId,
                ProfileId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value),
                TimeEnroll = DateTime.Now,
                Process = 0,
                NumberQuestion = 0,
            };
            await _context.ProfileCourses.AddAsync(courePro);
            await _context.SaveChangesAsync();

            ViewData["enroll"] = "yes";

            return Page();
        }
    }
}
