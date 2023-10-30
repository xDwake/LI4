using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using EletroExpo.Pages;
using Microsoft.AspNetCore.Routing;

namespace EletroExpo.Pages
{
    public class PageOfertaModel : PageModel
    {
        public oferta offer = new oferta();
        public string nomeLoja { get; set; }
        public string idProd { get; set; }
        public string rejectMessage = "";
        public string acceptMessage = "";
        public void OnGet(string lojaPath, string idProduto)
        {
            nomeLoja = lojaPath;
            idProd = idProduto;
        }
        public void OnPost()
        {
            offer.valor = Request.Form["valor"];
            float oferta = float.Parse(offer.valor);
            Produto produto = new Produto();
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EletroExpoDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM produtos WHERE idProduto=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", idProd);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                produto.idProduto = reader.GetInt32(0);
                                produto.marca = reader.GetString(1);
                                produto.modelo = reader.GetString(2);
                                produto.rating = reader.GetFloat(3);
                                produto.especificacao = reader.GetString(4);
                                produto.categoria = reader.GetString(5);
                                produto.stock = reader.GetInt32(6);
                                produto.preco = reader.GetFloat(7);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            float diff = oferta - produto.preco;
            if (diff > 0.15)
            {
                rejectMessage = "A Oferta Não Foi Aceite!";
                return;
            }
            else
            {
                acceptMessage = "A Oferta Foi Aceite!";
            }
            Response.Redirect("/loja/{lojaPath}/Produtos/{idProduto}/Compra");
        }
    }
    public class oferta
    {
        public String valor;
    }
}
