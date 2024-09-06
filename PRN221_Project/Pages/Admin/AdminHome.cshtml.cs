using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project.Utility;

namespace PRN221_Project.Pages.Admin
{
    public class AdminHomeModel : PageModel
    {
        [Authorize(Roles = RoleConstant.ADMIN + "," + RoleConstant.LECTURE)]
        public void OnGet()
        {
        }
    }
}
