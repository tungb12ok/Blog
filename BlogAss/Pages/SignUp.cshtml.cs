using System;
using BussinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogAss.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly UserBusinessLogic userBusinessLogic;

        [BindProperty]
        public string Mess { get; set; }

        public SignUpModel()
        {
            userBusinessLogic = new UserBusinessLogic();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int userId, string name, string email, string password, string re_password, string phone, DateTime dateOfBirth, string gender, int fieldId)
        {
            if(password != re_password)
            {
                Mess = "Password and re-password not same!";
                return Page(); // Redirect to the same page with an error message
            }
            try
            {
                userBusinessLogic.CreateUser(name, email, password, phone, dateOfBirth, gender, fieldId);

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                Mess = ex.Message;
                return Page(); // Redirect to the same page with an error message
            }
        }
    }
}
