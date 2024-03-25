using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessLogic;
using DataAccess.Models;

namespace BlogAss.Pages
{
    public class ProfileModel : PageModel
    {
        FblogContext context  = new FblogContext();
        UserBusinessLogic userBusinessLogic = new UserBusinessLogic();
        [BindProperty]
        public string Mess { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(int userId, string name, string email, string password, string re_password, string phone, DateTime dateOfBirth, string gender, int fieldId)
        {
            if (password != re_password)
            {
                Mess = "Password and re-password not same!";
                return Page(); // Redirect to the same page with an error message
            }
            try
            {
                userBusinessLogic.UpdateUser(userId, name, email,password, phone, dateOfBirth, gender, fieldId);
                Mess = "Update successfuly!";
                HttpContext.Session.SetObject("UserObject", context.Users.Where(u => u.Id == userId).FirstOrDefault());

                return RedirectToPage("Profile");
            }
            catch (Exception ex)
            {
                Mess = ex.Message;
                return Page(); // Redirect to the same page with an error message
            }
        }
    }
}
