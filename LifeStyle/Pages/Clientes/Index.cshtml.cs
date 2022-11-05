using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LifeStyle.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClienteInfo> ListClientes = new List<ClienteInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-B4HQ29B;Initial Catalog=lifestyle;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM CLIENTE";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                         using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClienteInfo clienteInfo = new ClienteInfo();
                                clienteInfo.Id = "" + reader.GetInt32(0);
                                clienteInfo.Nome = reader.GetString(1);
                                clienteInfo.Cpf = reader.GetString(2);
                                clienteInfo.Rg = reader.GetString(3);
                                clienteInfo.Email = reader.GetString(4);
                                clienteInfo.Telefone = reader.GetString(5);
                                clienteInfo.Created_at = reader.GetDateTime(6).ToString();

                                ListClientes.Add(clienteInfo);
                            }
                        }
                    

                    }
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine("Erro no show table cliente index" + ex.ToString());
            }
        }
    }
    public class ClienteInfo
    {
        public String Id;
        public String Nome;
        public String Cpf;
        public String Rg;
        public String Email;
        public String Telefone;
        public String Created_at;
    }
}
