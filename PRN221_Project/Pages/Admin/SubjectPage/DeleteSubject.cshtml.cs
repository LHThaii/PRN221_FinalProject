using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.SubjectPage
{
    public class DeleteSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        public DeleteSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, int pid)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(i => i.SubjectId == id);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/SubjectPage/ListSubject", new { id = pid });
        }
    }
}
