using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.SubjectPage
{
    public class SubjectDetailsModel : PageModel
    {
        private readonly PRN221Context _context;
        
        public Course Course { get; set; }
        public Subject Subject { get; set; }
        public List<Course> listCourse{ get; set; }

        public SubjectDetailsModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == id);
            Course = await _context.Courses.FirstOrDefaultAsync(i => i.SubjectId == Subject.SubjectId);
            listCourse = await _context.Courses.Where(i => i.SubjectId == id).ToListAsync();
            if (Subject == null) throw new ArgumentException("Can not find subject !");
            return Page();
        }
    }
}
