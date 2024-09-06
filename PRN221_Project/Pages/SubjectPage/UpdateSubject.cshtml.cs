using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.SubjectPage
{
    public class UpdateSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Subject Subject { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public UpdateSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int subid)
        {
            Subject = await _context.Subjects.Include(i => i.Profile).FirstOrDefaultAsync(i => i.SubjectId == subid);
            if (Subject == null) throw new ArgumentException("Can not find subjet !");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var s = await _context.Subjects.FirstOrDefaultAsync(i => i.SubjectId == Subject.SubjectId);
            s.Name = Subject.Name;
            s.Code = Subject.Code;
            s.ProfileId = Subject.ProfileId;
            s.Status = Subject.Status;
            await _context.SaveChangesAsync();
            return RedirectToPage("./ListSubject");
        }
    }
}
