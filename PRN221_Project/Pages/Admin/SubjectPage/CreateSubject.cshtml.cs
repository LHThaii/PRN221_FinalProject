using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.SubjectPage
{
    public class CreateSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public int ProfileId { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        [BindProperty]
        public string Code { get; set; } = null!;
        public CreateSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProfileId = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var subject = new Subject
            {
                Name = Name,
                ProfileId = ProfileId,
                Status = Status,
                Code = Code,
                Description = Description
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/SubjectPage/ListSubject", new { id = ProfileId });
        }
    }
}
