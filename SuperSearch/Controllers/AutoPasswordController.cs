using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SuperSearch.model;

namespace SuperSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoPasswordController : ControllerBase
    {
        private readonly string connection = "Data Source=DESKTOP-0ISA8J5;Initial Catalog=SearchEngine;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        [HttpPut]
        public IActionResult GeneratePassword(string useremail, string autopassword, string newpass, string confirmpass)
        {
            if (newpass != confirmpass)
                return BadRequest("New password and confirm password do not match.");

            SqlConnection con = new SqlConnection(connection);
            {
                con.Open();


                string query = "SELECT autopassword FROM autogeneratepass WHERE usermail = @usermail AND autopassword = @autopassword";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@usermail", useremail);
                cmd.Parameters.AddWithValue("@autopassword", autopassword);

                SqlDataReader reader = cmd.ExecuteReader();

                con.Close();

                con.Open();

                if (reader.Read())
                {
                   // reader.Close(); // Close the reader before executing another command


                    string updateQuery = "UPDATE [User] SET password = @newpass WHERE email = @usermail";

                    SqlCommand cmd2 = new SqlCommand(updateQuery, con);
                    {
                        cmd2.Parameters.AddWithValue("@newpass", newpass);
                        cmd2.Parameters.AddWithValue("@usermail", useremail);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Ok("Password updated successfully.");
                        }
                        else
                        {
                            return BadRequest("User not found or password not updated.");
                        }
                    }
                }
                else
                {
                    return BadRequest("Invalid email or auto-generated password.");
                }

                con.Close();
            }
        }
    }
}
