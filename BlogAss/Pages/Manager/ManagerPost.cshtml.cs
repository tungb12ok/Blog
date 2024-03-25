using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogAss.Pages.Manager
{
    public class ManagerPostModel : PageModel
    {
        private FblogContext _blogContext = new FblogContext();
        public List<Article> articles { get; set; }
        public List<Status> ls { get; set; }
        public String mess { get; set; } = String.Empty;
        public IActionResult OnGet()
        {
            User u = HttpContext.Session.GetObject<User>("UserObject");
            ls = _blogContext.Statuses.ToList();
            if (u == null)
            {
                return RedirectToPage("/Index");
            }

            if (u.RoleId == 1)
            {
                articles = _blogContext.Articles.Include(a => a.User).ToList();
                return Page();
            }
            else if (u.RoleId == 3)
            {
                articles = _blogContext.Articles
                            .Where(a => a.FieldId == u.FieldId)
                            .Include(a => a.User)
                            .ToList();
                return Page();
            }
            else if (u.RoleId == 2)
            {
                articles = _blogContext.Articles
                            .Where(a => a.UserId == u.Id)
                            .Include(a => a.User)
                            .ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
        public async Task<IActionResult> OnPostAsync(int aId, int sId)
        {
            var u = HttpContext.Session.GetObject<User>("UserObject");

            if (u.RoleId == 1 || u.RoleId == 3)
            {
                Article aedit = _blogContext.Articles.Where(u => u.Id == aId).FirstOrDefault();

                aedit.StatusId = sId;
                await _blogContext.SaveChangesAsync();
                mess = "Update successfully!";

                return RedirectToPage("/Manager/ManagerPost");
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }

    }
}
