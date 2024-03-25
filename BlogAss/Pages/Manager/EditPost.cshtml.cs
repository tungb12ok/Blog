using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogAss.Pages.Manager
{
    public class EditPostModel : PageModel
    {
        private FblogContext context = new FblogContext();

        [BindProperty]
        public Article Article { get; set; }
        public String mess { get; set; }


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

                Article = context.Articles.FirstOrDefault(x => x.Id == id && x.UserId == u.Id);

                if (Article == null)
                {
                    return RedirectToPage("/AccessDenied");
                }

                return Page();
            }
            catch (Exception ex)
            {
                ViewData["Mess"] = "Access denied!";
                return RedirectToPage("/Error");
            }
        }


        public IActionResult OnPost()
        {
            try
            {
                User u = (User)HttpContext.Session.GetObject<User>("UserObject");
                Article aEdit = context.Articles.Where(a => a.Id == Article.Id).FirstOrDefault();
                aEdit.Title = Article.Title.Trim();
                aEdit.ArticleContent = Article.ArticleContent;
                aEdit.FieldId = Article.FieldId;
                aEdit.Vote = Article.Vote;

                context.SaveChanges();
                mess = "Add Successfuly!";
                return Page();
            }
            catch (Exception ex)
            {
                return Redirect("/Error");
            }
        }
    }
}
