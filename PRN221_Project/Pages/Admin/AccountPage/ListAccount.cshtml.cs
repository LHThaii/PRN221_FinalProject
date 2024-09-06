using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.AccountPage
{
    public class ListAccountModel : PageModel
    {
        private readonly PRN221Context _context;

        public List<Profile> ListAccount { get; set; }
        [BindProperty]
        public string? Keyword { get; set; }
        [BindProperty]
        public bool? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public ListAccountModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? keyword, bool? status, int pageIndex, int pagesize)
        {
            Keyword = keyword;
            Status = status;
            if (pageIndex == 0) pageIndex = 1;
            PageIndex = pageIndex;
            pagesize = 4;
            var query = _context.Profiles.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower().Trim()));
            }
            /* if (status != null)
             {
                 query = query.Where(x => x.Status == status);
             }*/

            ListAccount = await query.Include(i => i.Role).Skip((pageIndex - 1) * pagesize)
                .Take(pagesize).OrderByDescending(i => i.ProfileId).ToListAsync();

            TotalPage = (int)(Math.Ceiling(query.ToList().Count / (double)pagesize));
            return Page();
        }
    }
}
