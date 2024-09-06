using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Models;

namespace PRN221_Project.Pages.Admin.AccountPage
{
    public class AddAccountModel : PageModel
    {
        private readonly PRN221Context _context;
        public AddAccountModel(PRN221Context context)
        {
            _context = context;
        }
        private static Random random = new Random();
        [BindProperty]
        public Models.Profile Profile { get; set; }
        [BindProperty]
        public Models.Account Account { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // ModelState lưu trữ thông tin về trạng thái và lỗi của các thuộc tính trong mô hình
            {
                var _user = _context.Accounts.Where(x => x.Email == Account.Email).FirstOrDefault();
                if (_user == null)
                {
                    const string chars = "0123456789";
                    var id = new string(Enumerable.Repeat(chars, 5) //Mỗi lần chạy vòng lặp Enumerable.Repeat, chọn một ký tự ngẫu nhiên từ chuỗi và lưu vào mảng
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    //gán giá trị cho các thuộc tính của đối tượng Profile
                    Profile.Name = Profile.Name ?? "";
                    Profile.ProfileId = Int32.Parse(id);
                    Profile.RoleId = 2;
                    Profile.Email = Account.Email;
                    _context.Profiles.Add(Profile);
                    _context.SaveChanges();

                    //gán giá trị cho các thuộc tính của đối tượng Account
                    Account.AccountId = Int32.Parse(id);
                    Account.RoleId = 2;
                    _context.Accounts.Add(Account);
                    _context.SaveChanges();

                    //return RedirectToPage("./SignIn");
                    ViewData["error"] = "Add successful !!!!";
                    return RedirectToPage("ListAccount");
                }
                else
                {
                    ViewData["error"] = "User is already exist !!!!";
                    return Page();
                }
            }
            else
            {
                ModelState.Clear();
            }
            return Page();

        }
    }
}
