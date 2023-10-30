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
    public class ListaProdutosModel : PageModel
    {
        public string? nomeLj { get; set; }
        public List<Produto> listProdutos = new List<Produto>();
        public void OnGet(string lojaPath)
        {
            int idForm = 0;
            nomeLj = lojaPath;
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM formularios WHERE nomeEmpresa=@empresa";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@empresa", nomeLj);
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

                                idForm = formulario.idFormulario;
                            }
                        }
                    }
                    sql = "SELECT * FROM produtos WHERE idFormulario=@idF";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idF", idForm);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produto produto = new Produto();
                                produto.idProduto = reader.GetInt32(0);
                                produto.marca = reader.GetString(1);
                                produto.modelo = reader.GetString(2);
                                produto.rating = reader.GetFloat(3);
                                produto.especificacao = reader.GetString(4);
                                produto.categoria = reader.GetString(5);
                                produto.stock = reader.GetInt32(6);
                                produto.preco = reader.GetFloat(7);

                                listProdutos.Add(produto);
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
    public class Produto
    {
        public int idProduto;
        public String marca = "";
        public String modelo = "";
        public float rating;
        public String especificacao = "";
        public String categoria = "";
        public int stock;
        public float preco;
    }
}

