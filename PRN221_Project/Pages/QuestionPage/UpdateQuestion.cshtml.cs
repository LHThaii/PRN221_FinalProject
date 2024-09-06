using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System;

namespace PRN221_Project.Pages.QuestionPage
{
    public class UpdateQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public Question question { get; set; }
        public List<Course> listCourse { get; set; }
        public UpdateQuestionModel(PRN221Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            question = await _context.Questions.Include(i => i.Course).FirstOrDefaultAsync(i => i.QuestionId == id);
            listCourse = await _context.Courses.ToListAsync();

            if (question == null) throw new ArgumentException("Can not find question !");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ques = await _context.Questions.FirstOrDefaultAsync(i => i.QuestionId == question.QuestionId);
            ques.QuestionContent = question.QuestionContent;
            ques.Answer = question.Answer;
            ques.CourseId = question.CourseId;
            ques.Status = question.Status;
            await _context.SaveChangesAsync();
            return RedirectToPage("ViewAllQuestion");
        }
    }
}
