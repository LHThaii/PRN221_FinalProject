using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.CoursePage
{
    public class ListCourseModel : PageModel
    {
        private readonly PRN221Context _context;

        public List<Course> ListCourse { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public ListCourseModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize, int id)
        {
            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 4;
            var query = _context.Courses.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Title.ToLower().Contains(keyword.ToLower())
                                      || !string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(keyword.ToLower())
                );
            }
            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if (id != 0)
            {
                query = query.Where(x => x.ProfileId == id);
            }

            ListCourse = await query.Include(i => i.Subject).Skip((pageIndex - 1) * pagesize)
                .Take(pagesize).OrderByDescending(i => i.CourseId).ToListAsync();

            TotalPage = (int)(Math.Ceiling(query.ToList().Count / (double)pagesize));
            return Page();
        }
    }
}
