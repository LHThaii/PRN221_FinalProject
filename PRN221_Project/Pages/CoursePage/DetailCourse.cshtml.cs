using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System.Security.Claims;

namespace PRN221_Project.Pages.CoursePage
{
    public class DetailCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        public Course Course { get; set; }
        public Subject Subject { get; set; }
        public DetailCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == Course.SubjectId);
            if (Subject == null) throw new ArgumentException("Can not find subject !");

            var check = true;
            var uid = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
            var user = _context.ProfileCourses.Where(p => p.ProfileId == uid && p.CourseId == Course.CourseId).FirstOrDefault();
            if (user == null) check = false;
            else check = true;

            ViewData["check"] = check;

            return Page();
        }

        public async Task<IActionResult> OnGetEnrolled(int id, int studentId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == course.SubjectId);
            Course = course;
            if (course == null) throw new ArgumentException("Can not find");
            course.IsEnrolled += 1;
            await _context.SaveChangesAsync();

            var courePro = new ProfileCourse
            {
                CourseId = course.CourseId,
                ProfileId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value),
                TimeEnroll = DateTime.Now,
                Process = 0,
                NumberQuestion = 0,
            };
            await _context.ProfileCourses.AddAsync(courePro);
            await _context.SaveChangesAsync();

            ViewData["check"] = true;

            return Page();
        }

        public async Task<IActionResult> OnGetUnenrolled(int id, int pid)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == id);
            course.IsEnrolled -= 1;
            await _context.SaveChangesAsync();

            var courseProfile = await _context.ProfileCourses.FirstOrDefaultAsync(i => i.ProfileId == pid && i.CourseId == course.CourseId);
            _context.ProfileCourses.Remove(courseProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("ListEnrollCourse", new { id = pid });

        }
    }
}
