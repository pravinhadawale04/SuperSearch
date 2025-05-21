using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperSearch.model;

namespace SuperSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCRUDController : ControllerBase
    {

        public string connection = "Data Source=DESKTOP-0ISA8J5;Initial Catalog=SearchEngine;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        [HttpGet]
        public IActionResult ShowUsers()
        {
            List<User> users = new List<User>();

            SqlConnection conn = new SqlConnection(connection);

            string query = "Select * from [User]";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                User user = new User
                {
                    userId = Convert.ToInt32(reader["userId"]),
                    username = reader["username"].ToString(),
                    email = reader["email"].ToString(),
                    password = reader["password"].ToString(),
                    phone_no = Convert.ToInt64(reader["phone_no"]),
                    registerdate = Convert.ToDateTime(reader["registerdate"]),
                    Isconfirm = reader["Isconfirm"].ToString()
                };

                users.Add(user);

            }
            reader.Close();


            conn.Close();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            SqlConnection conn = new SqlConnection(connection);

            string query = "DELETE FROM [User] WHERE userId=@userId";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@userId", id);

            conn.Open();


            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                return Ok("Login Successfully..!");
            }
            else
            {

                return Ok("Wrong login id or password !");
            }
            conn.Close();
        }




        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            SqlConnection conn = new SqlConnection(connection);
            
                string query = "UPDATE [User] SET username=@username, email=@email, password=@password, phone_no=@phone_no, registerdate=@registerdate, Isconfirm=@Isconfirm WHERE userId=@userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@phone_no", user.phone_no);
                cmd.Parameters.AddWithValue("@registerdate", user.registerdate);
                cmd.Parameters.AddWithValue("@Isconfirm", user.Isconfirm);
                cmd.Parameters.AddWithValue("@userId", id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                conn.Close();

                return rows > 0 ? Ok("User updated successfully") : NotFound("User not found");
            }
        }

    }


