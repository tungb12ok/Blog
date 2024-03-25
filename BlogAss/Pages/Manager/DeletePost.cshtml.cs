using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
namespace BlogAss.Pages.Manager
{
    public class DeletePostModel : PageModel
    {
        private FblogContext context = new FblogContext();
        public IActionResult OnGet(int id)
        {
            try
            {
                User u = HttpContext.Session.GetObject<User>("UserObject");

                if (u == null)
                {
                    TempData["Mess"] = "Access denied!";
                    return RedirectToPage("/Error");
                }

                Article a = context.Articles.FirstOrDefault(x => x.Id == id && x.UserId == u.Id);

                if (a == null)
                {
                    return RedirectToPage("/Error");
                }
                else
                {
                    context.Articles.Remove(a);
                    context.SaveChanges();
                    return Redirect("/Manager/ManagerPost");
                }

                return Page();
            }
            catch (Exception ex)
            {
                ViewData["Mess"] = "Access denied!";
                return RedirectToPage("/Error");
            }
        }
    }
}
