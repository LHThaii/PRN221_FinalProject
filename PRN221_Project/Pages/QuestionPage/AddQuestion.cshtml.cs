using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;
using System;

namespace PRN221_Project.Pages.QuestionPage
{
    public class AddQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Question question { get; set; }

        public AddQuestionModel(PRN221Context context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return RedirectToPage("ViewAllQuestion");
        }
    }
}
