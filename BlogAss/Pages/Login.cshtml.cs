using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlogAss.Pages
{
    public class LoginModel : PageModel
    {
        private FblogContext context = new FblogContext();
        public string mess { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(String email, String password)
        {
            User u = context.Users.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
            if (u !=null && u.Status.Equals("Ban"))
            {
                mess = "Tài khoản của bạn bị cấm vui lòng liên hệ admin!";
                return Page();
            }
            // Xử lý đăng nhập ở đây, kiểm tra tên người dùng và mật khẩu
            if (u != null)
            {
                HttpContext.Session.SetObject("UserObject", u);

                if (u.RoleId == 1)
                {
                    return RedirectToPage("/Manager/ManagerAccount");
                } else if(u.RoleId == 2)
                {
                    return RedirectToPage("/Manager/ManagerPost");

                }
                else
                {
                    return RedirectToPage("/Index");

                }

            }
            else
            {
                mess = "Email or Password not Correct!";
                return Page();
            }
        }
    }
}
