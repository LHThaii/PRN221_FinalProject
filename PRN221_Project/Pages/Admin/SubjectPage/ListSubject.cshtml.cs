using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.SubjectPage
{
    public class ListSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        public List<Subject> ListSubject { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public ListSubjectModel(PRN221Context context)
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
            var query = _context.Subjects.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower())
                                      || !string.IsNullOrEmpty(x.Code) && x.Code.ToLower().Contains(keyword.ToLower())
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

            ListSubject = await query.Include(i => i.Profile).Skip((pageIndex - 1) * pagesize)
                .Take(pagesize).OrderByDescending(i => i.SubjectId).ToListAsync();

            TotalPage = (int)(Math.Ceiling(query.ToList().Count / (double)pagesize));
            return Page();
        }
    }
}
