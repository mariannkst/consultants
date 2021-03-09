using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConsultantController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // HTTP request with GET method to get All the data on a Consultant with the ID
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            // This is just a simple version of writing the query, it should be Stored procedure with parameters or Entity framework
            string query = @"
                SELECT * FROM dbo.ConsultantsList WHERE ConsultantId = " + id + @" ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("ConsultantApiConnection");
            SqlDataReader myReader;

            // SQL connection and putting the data into a DataTable
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);

        }



    }
}
