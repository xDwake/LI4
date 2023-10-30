using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EletroExpo.Pages.Utilizadores
{
    public class EditModel : PageModel
    {
        public UtilizadorInfo utilizadorInfo = new UtilizadorInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM utilizadores WHERE id=@idUtilizador";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idUtilizador", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                utilizadorInfo.username = "" + reader.GetString(1);
                                utilizadorInfo.email = "" + reader.GetString(2);
                                utilizadorInfo.password = "" + reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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
                    String sql = "UPDATE utilizadores " + "SET username=@username, email=@email" +
                        "password=@password WHERE id=@idUtilizador";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", utilizadorInfo.username);
                        command.Parameters.AddWithValue("@email", utilizadorInfo.email);
                        command.Parameters.AddWithValue("@password", utilizadorInfo.password);
                    
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            { 
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Utilizadores/Index");
        }
    }
}
