using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.SubjectPage
{
    public class AddSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public int SubjectId { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int ProfileId { get; set; }
        [BindProperty]
        public bool Status { get; set; }
        [BindProperty]
        public string Code { get; set; } = null!;

        public AddSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var subject = new Subject
            {
                SubjectId = SubjectId,
                Name = Name,
                ProfileId = ProfileId,
                Status = Status,
                Code = Code
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return RedirectToPage("./ListSubject");
        }
    }
}
