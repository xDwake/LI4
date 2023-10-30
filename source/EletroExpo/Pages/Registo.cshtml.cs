using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EletroExpo.Pages
{
    public class RegistoModel : PageModel
    {
        public bool hasData = false;
        public string username = "";
        public string email = "";
        public string password = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            hasData = true;
            username = Request.Form["username"];
            email = Request.Form["email"];
            password = Request.Form["password"];
        }
    }
}
