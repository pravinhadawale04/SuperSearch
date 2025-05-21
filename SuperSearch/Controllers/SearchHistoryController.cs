using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperSearch.model;

namespace SuperSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public SearchHistoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string connection = "Data Source=DESKTOP-0ISA8J5;Initial Catalog=SearchEngine;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
       
        [HttpPost("log")]
        public IActionResult LogSearch( searchhistory request)
        {
            if (request == null || request.userid == 0 || request.bikeid == 0)

                return BadRequest("Invalid input data.");



            SqlConnection con = new SqlConnection(connection);

            SqlCommand cmd = new SqlCommand("InsertSearchHistory", con);
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userid", request.userid);
                cmd.Parameters.AddWithValue("@bikeid", request.bikeid);
                cmd.Parameters.AddWithValue("@search_Desc", request.search_desc);
                cmd.Parameters.AddWithValue("@searchdate", DateTime.Now);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0
                        ? Ok("Search logged via stored procedure.")
                        : StatusCode(500, "Failed to insert.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Database error: {ex.Message}");
                }
            }
        }
           
    }
}
