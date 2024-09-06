using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;
using System.Security.Claims;

namespace PRN221_Project.Pages
{
    public class LoginModel : PageModel
    {
        private readonly PRN221Context _context;

        public LoginModel(PRN221Context context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        { // lấy và lưu trữ thông tin vào clamis 
            var accountId = User.Claims.FirstOrDefault(x => x.Type.Split("/").Last().Equals("name"));
            if (accountId != null)
            {
                return Redirect("/Home");
            }
            return Page();
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToPage("/Login");
        }
        public async Task<IActionResult> OnPost(string email, string password, [FromQuery] string? ReturnUrl)
        {
            var _user = _context.Accounts.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (_user == null)
            {
                ViewData["Title"] = "Username or password wrong !!!";
                return Page();
            }
            else
            {
                var _userInfo = _context.Profiles.FirstOrDefault(x => x.ProfileId == _user.AccountId);
                var role = "";
                if (_user.RoleId == 1) role = "Admin";
                else if (_user.RoleId == 2) role = "Lecture";
                else role = "Student";
                var claims = new[] {
                new Claim(ClaimTypes.Sid, _user.AccountId.ToString()),
                new Claim(ClaimTypes.Name, _userInfo.Name.ToString().Trim()),
                new Claim(ClaimTypes.Role, role.ToString().Trim()),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7),
                });
                if (_user.RoleId == 3)
                {
                    // Tra ve trang quan ly nhe
                    return RedirectToPage("/SubjectPage/ListSubject");
                }
                else
                {
                    if (_user.RoleId == 1 || _user.RoleId == 2)
                    {
                        return RedirectToPage("/Admin/AdminHome");
                    }
                    else
                    {
                        return RedirectToPage("/Home");
                    }
                }
            }
        }
    }
}
