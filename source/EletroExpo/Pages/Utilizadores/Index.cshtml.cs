using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EletroExpo.Pages.Utilizadores
{
    public class IndexModel : PageModel
    {
        public List<UtilizadorInfo> listUtilizadores = new List<UtilizadorInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM utilizadores";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                UtilizadorInfo utilizadorInfo = new UtilizadorInfo();
                                utilizadorInfo.id = "" + reader.GetInt32(0);
                                utilizadorInfo.username = "" + reader.GetString(1);
                                utilizadorInfo.email = reader.GetString(2);
                                utilizadorInfo.password = reader.GetString(3);
                                utilizadorInfo.isVendedor = reader.GetBoolean(4);
                                utilizadorInfo.isAtivo = reader.GetBoolean(5);
                                utilizadorInfo.isAdministrador = reader.GetBoolean(6);
                                listUtilizadores.Add(utilizadorInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class UtilizadorInfo
    {
        public String id = "";
        public String username = "";
        public String email = "";
        public String password = "";
        public Boolean isVendedor = false;
        public Boolean isAtivo = false;
        public Boolean isAdministrador = false;
        public int idFormulario;
    }
}
