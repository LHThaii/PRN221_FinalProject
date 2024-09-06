using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.QuestionPage
{
    public class ListQuestionModel : PageModel
    {
        private readonly PRN221Context _context;

        public List<Question> ListQuestion { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        [BindProperty]
        public int? CourseId { get; set; }

        [BindProperty]
        public int Pid { get; set; }
        public List<Course> listCourses { get; set; }
        public ListQuestionModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int? courseId, int pageIndex, int pagesize, int id)
        {

            listCourses = await _context.Courses.Where(i => i.ProfileId == id).ToListAsync();
            Pid = id;
            Keyword = keyword;
            Status = status;
            CourseId = courseId;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 4;
            var query = _context.Questions.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.QuestionContent) && x.QuestionContent.ToLower().Contains(keyword.ToLower())
                );
            }

            if (courseId != 0 && courseId != null)
            {
                query = query.Where(x => x.CourseId == courseId);
            }

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if (id != 0)
            {
                query = query.Where(i => i.Course.ProfileId == id);
            }

            var query1 = query.Include(i => i.Course);
            ListQuestion = await query1.Skip((pageIndex - 1) * pagesize)
            .Take(pagesize).OrderByDescending(i => i.QuestionId).ToListAsync();

            TotalPage = (int)(Math.Ceiling(query1.ToList().Count / (double)pagesize));
            return Page();
        }
    }
}
