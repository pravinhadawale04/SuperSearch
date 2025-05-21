using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SuperSearch.model;

namespace SuperSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       public string connection = "Data Source=DESKTOP-0ISA8J5;Initial Catalog=SearchEngine;Integrated Security=True;TrustServerCertificate=True";

        [HttpPost("Login")]
        public IActionResult Adminlogin(string username, string password)
        {
            SqlConnection con = new SqlConnection(connection);
            {
                con.Open();
                string query = "SELECT * FROM Admin WHERE email = @username AND password = @password";
               
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
             
                if (reader.Read())
                {

                    return Ok("Login Successfully..!");
                }
                else
                {

                    return Ok("Wrong login id or password !");           
                }
                con.Close();
            }
        }

    }
}
