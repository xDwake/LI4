using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EletroExpo.Pages.Utilizadores;
using System.Data.SqlClient;
using EletroExpo.Pages;

namespace EletroExpo.Pages
{
   public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<FormularioInfo> nomeLojas = new List<FormularioInfo>();
        public List<FormularioInfo> GetLojas()
        {
            int contador = 0;
            List<UtilizadorInfo> listUtilizadores = new List<UtilizadorInfo>();
            List<FormularioInfo> listFormularios = new List<FormularioInfo>();
            List<FormularioInfo> res = new List<FormularioInfo>();
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
                                if (utilizadorInfo.isVendedor == false)
                                {
                                    utilizadorInfo.idFormulario = 0;
                                }
                                else
                                {
                                    utilizadorInfo.idFormulario = reader.GetInt32(7);
                                }
                                if (utilizadorInfo.isVendedor == true && utilizadorInfo.isAtivo 
                                    && contador < 10)
                                {
                                    listUtilizadores.Add(utilizadorInfo);
                                    contador++;
                                }
                            }
                        }
                    }
                    sql = "SELECT * FROM formularios";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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
                                listFormularios.Add(formulario);
                            }
                        }
                    }
                }
                contador = 0;
                foreach (var user in listUtilizadores)
                {
                    foreach (var formulario in listFormularios)
                    {
                        if (formulario.idFormulario == user.idFormulario)
                        {
                            res.Add(formulario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return res;
        }
        public void OnGet() { this.nomeLojas = GetLojas(); }
    }
    public class FormularioInfo
    {
        public int idFormulario;
        public String ramoAtividade = "";
        public String morada = "";
        public String contacto = "";
        public String nomeEmpresa = "";
        public String codPostal = "";
    }
}