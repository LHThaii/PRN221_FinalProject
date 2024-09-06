using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.AccountPage
{
    public class UpdateAccountModel : PageModel
    {
        private readonly PRN221Context _context;
        public List<Role> ListRole { get; set; }

        [BindProperty]
        public Profile? Profile { get; set; }
        public UpdateAccountModel(PRN221Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ListRole = await _context.Roles.ToListAsync();
            Profile = await _context.Profiles.FirstOrDefaultAsync(i => i.ProfileId == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var p = await _context.Profiles.FirstOrDefaultAsync(i => i.ProfileId == Profile.ProfileId);
            var acc = await _context.Accounts.FirstOrDefaultAsync(i => i.AccountId == Profile.ProfileId);
            if (p == null) throw new ArgumentException("Can not find");
            p.RoleId = Profile.RoleId;
            acc.RoleId = Profile.RoleId;
            await _context.SaveChangesAsync();
            return RedirectToPage("ListAccount");
        }
    }
}
