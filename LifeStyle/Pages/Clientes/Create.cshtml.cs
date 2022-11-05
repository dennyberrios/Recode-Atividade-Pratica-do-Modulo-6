using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LifeStyle.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();

        public String errorMessage = "";
        public String successMesage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clienteInfo.Nome = Request.Form["nome"];
            clienteInfo.Cpf = Request.Form["cpf"];
            clienteInfo.Rg = Request.Form["rg"];
            clienteInfo.Email = Request.Form["email"];
            clienteInfo.Telefone = Request.Form["telefone"];

            if (clienteInfo.Nome.Length == 0 || clienteInfo.Cpf.Length == 0 ||
                clienteInfo.Rg.Length == 0 || clienteInfo.Email.Length == 0 ||
                clienteInfo.Telefone.Length == 0)
            {
                errorMessage = "Preencha todos os campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-B4HQ29B;Initial Catalog=lifestyle;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO CLIENTE " +
                        "(nome, cpf, rg, email, telefone) VALUES" +
                        "(@Nome, @Cpf, @Rg, @Email, @Telefone);";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nome", clienteInfo.Nome);
                        cmd.Parameters.AddWithValue("@Cpf", clienteInfo.Cpf);
                        cmd.Parameters.AddWithValue("@Rg", clienteInfo.Rg);
                        cmd.Parameters.AddWithValue("@Email", clienteInfo.Email);
                        cmd.Parameters.AddWithValue("@Telefone", clienteInfo.Telefone);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clienteInfo.Nome = ""; clienteInfo.Cpf = ""; clienteInfo.Rg = ""; clienteInfo.Email = ""; clienteInfo.Telefone = "";
            successMesage = "Cliente cadastrado com Sucesso!";

            Response.Redirect("/Clientes/Index");
        }
    }
}
