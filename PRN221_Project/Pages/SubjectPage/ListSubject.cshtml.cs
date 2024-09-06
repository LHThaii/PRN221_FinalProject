using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PRN221_Project.Pages.SubjectPage
{
    public class ListSubjectModel : PageModel
    {
        private readonly PRN221Context _context;

        [BindProperty]
        public string? Search { get; set; }
        public List<Subject> listSubject { get; set; }
        
        public ListSubjectModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? search)
        {
            Search = search;
            var query = _context.Subjects.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                //listSubject = await _context.Subjects.ToListAsync();
                //query =  query.ToListAsync();
                query = query.Where(i => i.Name.ToLower().Contains(search.ToLower()) || !string.IsNullOrEmpty(i.Code) && i.Code.ToLower().Contains(search.ToLower()));

            }
            listSubject = await query.OrderByDescending(i => i.SubjectId).ToListAsync();
            return Page();
        }
    }
}
