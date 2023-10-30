using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EletroExpo.Pages.Utilizadores
{
    public class CreateModel : PageModel
    {
        public UtilizadorInfo utilizadorInfo = new UtilizadorInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            utilizadorInfo.username = Request.Form["username"];
            utilizadorInfo.email = Request.Form["email"];
            utilizadorInfo.password = Request.Form["password"];

            if (utilizadorInfo.username.Length == 0 || utilizadorInfo.email.Length == 0 || 
                utilizadorInfo.password.Length == 0)
            {
                errorMessage = "Todos os Campos Devem Ser Preenchidos!";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO utilizadores " +
                                 "(username, email, password, isVendedor, isAtivo, isAdministrador) VALUES " +
                                 "(@username, @email, @password, @isVendedor, @isAtivo, @isAdministrador);"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", utilizadorInfo.username);
                        command.Parameters.AddWithValue("@email", utilizadorInfo.email);
                        command.Parameters.AddWithValue("@password", utilizadorInfo.password);
                        command.Parameters.AddWithValue("@isVendedor", 0);
                        command.Parameters.AddWithValue("@isAtivo", 0);
                        command.Parameters.AddWithValue("@isAdministrador", 0);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            utilizadorInfo.username = "";
            utilizadorInfo.email = "";
            utilizadorInfo.password = "";
            successMessage = "Novo Utilizador Registado!";

            Response.Redirect("/Utilizadores/Index");
        }    
    }
}
