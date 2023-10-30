using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EletroExpo.Pages
{
    public class PageProdutoModel : PageModel
    {
        public dadosPagamento dados = new dadosPagamento();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            dados.titular = Request.Form["titular"];
            dados.zona = Request.Form["zona"];
            dados.cidade = Request.Form["cidade"];
            dados.codPostal = Request.Form["codPostal"];
            dados.telefone = Request.Form["telefone"];
            dados.n_cartao = Request.Form["n_cartao"];
            dados.codSeg = Request.Form["codSeg"];
            if (dados.titular.Length == 0  || dados.zona.Length == 0 || dados.cidade.Length == 0 || 
                dados.codPostal.Length == 0 || dados.telefone.Length == 0 || dados.n_cartao.Length == 0 || 
                dados.codSeg.Length == 0)
            {
                errorMessage = "Todos os Campos Devem Ser Preenchidos!";
                return;
            }
            dados.titular = "";
            dados.zona = "";
            dados.cidade = "";
            dados.codPostal = "";
            dados.telefone = "";
            dados.n_cartao = "";
            dados.codSeg = "";
            successMessage = "Pagamento Processado!. A Fatura Será Enviada Por Email Com os Detalhes de Entrega!";
        }
    }
    public class dadosPagamento
    {
        public string titular = "";
        public string zona = "";
        public string cidade = "";
        public string codPostal = "";
        public string telefone = "";
        public string n_cartao = "";
        public string codSeg = "";
    }
}