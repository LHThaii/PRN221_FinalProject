using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.QuestionPage
{
    public class DeleteQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        public DeleteQuestionModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, int pid)
        {
            var Questions = await _context.Questions.FirstOrDefaultAsync(i => i.QuestionId == id);
            _context.Questions.Remove(Questions);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/QuestionPage/ListQuestion", new { id = pid });
        }
        
    }
}
