using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;
using System.Text.RegularExpressions;

namespace PRN221_Project.Pages
{
    public class RegisterAccountModel : PageModel
    {
        private readonly PRN221Context _context;

        public RegisterAccountModel(PRN221Context context)
        {
            _context = context;
        }
        private static Random random = new Random();
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public Profile Profile { get; set; }
        public IActionResult OnPost()
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (Account == null || Account.Email == null)
            {
                ViewData["Title"] = "Email can not be empty !";
                return Page();
            }
            var regex = Regex.IsMatch(Account.Email, emailPattern);
            if (!regex)
            {
                ViewData["Title"] = "Email is invalid !";
                return Page();
            }
            var _user = _context.Accounts.Where(x => x.Email == Account.Email).FirstOrDefault();
            if (_user == null)
            {
                const string chars = "0123456789";
                var id = new string(Enumerable.Repeat(chars, 5) //Mỗi lần chạy vòng lặp Enumerable.Repeat, chọn một ký tự ngẫu nhiên từ chuỗi và lưu vào mảng
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                //gán giá trị cho các thuộc tính của đối tượng Profile
                Profile.Name = Profile.Name ?? "";
                Profile.ProfileId = int.Parse(id);
                Profile.RoleId = 3;
                Profile.Status = true;
                Profile.Email = Account.Email;
                _context.Profiles.Add(Profile);
                _context.SaveChanges();
                //gán giá trị cho các thuộc tính của đối tượng Account
                Account.AccountId = int.Parse(id);
                Account.RoleId = 3;
                Account.IsActivated = true;
                _context.Accounts.Add(Account);
                _context.SaveChanges();

                ViewData["error"] = "Register successful !!!!";
                return RedirectToPage("Login");
            }
            else
            {
                ViewData["error"] = "User is already exist !!!!";
                return Page();
            }
            return Page();

        }
    }
}
