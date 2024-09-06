using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.SubjectPage
{
    public class UpdateSubjectModel : PageModel
    {
        private readonly PRN221Context _context;
        [BindProperty]
        public Subject Subject { get; set; }
        public UpdateSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Subject = await _context.Subjects.FirstOrDefaultAsync(i => i.SubjectId == id);
            if (Subject == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var s = await _context.Subjects.FirstOrDefaultAsync(i => i.SubjectId == Subject.SubjectId);
            s.Name = Subject.Name;
            s.Code = Subject.Code;
            s.ProfileId = Subject.ProfileId;
            s.Status = Subject.Status;
            s.Description = Subject.Description;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/SubjectPage/ListSubject", new { id = Subject.ProfileId });
        }
    }
}
