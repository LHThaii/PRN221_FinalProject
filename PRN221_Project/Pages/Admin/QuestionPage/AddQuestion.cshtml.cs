using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.QuestionPage
{
    public class AddQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Question question{ get; set; }
        public List<Course> listCourses { get; set; }

        [BindProperty]
        public int ProfileId { get; set; }
        public AddQuestionModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProfileId = id;
            listCourses = await _context.Courses.Where(i => i.ProfileId == id).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return RedirectToPage("ListQuestion", new { id = ProfileId });
        }
    }
}

