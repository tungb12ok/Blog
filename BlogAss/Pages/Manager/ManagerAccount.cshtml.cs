using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogAss.Pages.Manager
{
    public class ManagerAccountModel : PageModel
    {
        private FblogContext _blogContext = new FblogContext();

        public List<User> Users { get; set; }
        public User UserEdit { get; set; }
        public User userLogin { get; set; }
        public String mess { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var u = HttpContext.Session.GetObject<User>("UserObject");
            userLogin = (User)u;
            if (u == null || u.RoleId == 2)
            {
                return Redirect("/AccessDenied");
            }

            Users = _blogContext.Users.Where(u => u.RoleId != 1).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int userId, int roleId, string status)
        {
            var u = HttpContext.Session.GetObject<User>("UserObject");
            Users = _blogContext.Users.Where(user => user.RoleId != 1).ToList();

            if (u.RoleId == 1)
            {
                User uedit = _blogContext.Users.Where(u => u.Id == userId).FirstOrDefault();
                uedit.Status = status;
                uedit.RoleId = roleId;  
                await _blogContext.SaveChangesAsync();
                mess = "Update successfully!";
                Users = _blogContext.Users.Where(user => user.RoleId != 1).ToList();

                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }

    }
}
