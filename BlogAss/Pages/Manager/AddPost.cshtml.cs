using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogAss.Pages.Manager
{
    public class AddPostModel : PageModel
    {
        private FblogContext _context = new FblogContext();

        [BindProperty]
        public Article Article { get; set; }
        public String mess { get; set; }


        public IActionResult OnGet()
        {
            // Retrieve the list of Fields from the database.
            // Retrieve the list of Fields from the database.
            User u = HttpContext.Session.GetObject<User>("UserObject");
            if (u == null)
            {
                return Redirect("/AccessDenied");
            }


            return Page();
        }

        public IActionResult OnPost()
        {

            try
            {
                User u = (User)HttpContext.Session.GetObject<User>("UserObject");

                Article.UserId = u.Id;
                Article.DatePublished = DateTime.Now;
                if (u.RoleId == 1)
                {
                    Article.StatusId = 2;
                }
                else
                {
                    Article.StatusId = 1;
                }
                _context.Articles.Add(Article);
                mess = "Add Successfuly!";
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Redirect("/Error");
            }
            // Redirect to a success page or perform any other necessary actions.
            return Page();
        }
    }
}
