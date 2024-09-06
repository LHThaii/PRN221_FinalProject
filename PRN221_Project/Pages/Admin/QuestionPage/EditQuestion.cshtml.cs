using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.QuestionPage
{
    public class EditQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Question question { get; set; }
        public List<Course> listCourses { get; set; }
        [BindProperty]
        public int ProfileId { get; set; }


        public EditQuestionModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, int pid)
        {
            ProfileId = pid;
            listCourses = await _context.Courses.Where(i => i.ProfileId == pid).ToListAsync();
            question = await _context.Questions.FirstOrDefaultAsync(i => i.QuestionId == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var s = await _context.Questions.FirstOrDefaultAsync(i => i.QuestionId == question.QuestionId);
            s.QuestionContent = question.QuestionContent;
            s.Answer = question.Answer;
            s.CourseId = question.CourseId;
            s.Status = question.Status;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/QuestionPage/ListQuestion", new { id = ProfileId });
        }

    }
}
