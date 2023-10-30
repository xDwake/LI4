using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EletroExpo.Pages.Shared
{
    public class FormularioModel : PageModel
    {
        public bool hasData = false;
        public String ramoAtividade = "";
        public String morada = "";
        public String contacto = "";
        public String nomeEmpresa = "";
        public String nipc = "";
        public String codPostal = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            hasData = true;
            ramoAtividade = Request.Form["ramoAtividade"];
            morada = Request.Form["morada"];
            contacto = Request.Form["contacto"];
            nomeEmpresa = Request.Form["nomeEmpresa"];
            nipc = Request.Form["nipc"];
            codPostal = Request.Form["codPostal"];
        }
    }
}
