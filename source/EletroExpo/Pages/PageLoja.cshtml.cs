using EletroExpo.Pages.Utilizadores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Console;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace EletroExpo.Pages
{
    [Microsoft.AspNetCore.Components.Route("loja/{lojaPath}")]
    public class PageLojaModel : PageModel
    {
        public string? NomeLoja { get; set; }
        public FormularioInfo vendedor = new FormularioInfo();
        public void OnGet(string lojaPath)
        {
            NomeLoja = lojaPath;
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM formularios WHERE nomeEmpresa=@empresa";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@empresa", NomeLoja);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FormularioInfo formulario = new FormularioInfo();
                                formulario.idFormulario = reader.GetInt32(0);
                                formulario.ramoAtividade = reader.GetString(1);
                                formulario.morada = reader.GetString(2);
                                formulario.contacto = reader.GetString(3);
                                formulario.nomeEmpresa = reader.GetString(4);
                                formulario.codPostal = reader.GetString(5);

                                vendedor = formulario;
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
}
