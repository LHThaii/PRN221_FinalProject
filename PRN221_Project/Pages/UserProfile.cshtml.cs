using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using PRN221_Project.Pages.ViewModel;

namespace PRN221_Project.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly PRN221Context _context;
        private readonly IMapper _mapper;

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public ChangePasswordRequest Request { get; set; }
        public UserProfileModel(PRN221Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(i => i.ProfileId == id);
            if (Profile == null) { throw new ArgumentException("Can not find!"); }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(i => i.ProfileId == Profile.ProfileId);
                if (user == null) { throw new ArgumentException("Can not find!"); }
                var newUser = _mapper.Map<Models.Profile, Models.Profile>(Profile, user);
                newUser.RoleId = user.RoleId;
                _context.Profiles.Update(newUser);
                await _context.SaveChangesAsync();
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("Error");
            }
        }
        public async Task<IActionResult> OnPostChangePassword()
        {
            try
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(i => i.AccountId == Profile.ProfileId);
                if (Request.OldPassword == user.Password)
                {
                    if (Request.NewPassword == Request.ConfirmPassword)
                    {
                        user.Password = Request.NewPassword;
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Change password successfully";
                        return Page();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "New password and confirm password are not the same";
                        return Page();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Old password is not correct";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("Error");
            }
        }
    }
}
