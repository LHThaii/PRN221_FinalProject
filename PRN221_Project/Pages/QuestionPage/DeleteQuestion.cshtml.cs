using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System;

namespace PRN221_Project.Pages.QuestionPage
{
    public class DeleteQuestionModel : PageModel
    {
        private readonly PRN221Context _context;
            
        public DeleteQuestionModel(PRN221Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(i => i.QuestionId == id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("ViewAllQuestion");
        }
    }
}
