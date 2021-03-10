using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private Consultant Consultant;
        private WorkExperienceDetails WorkExperienceDetails;

        public ConsultantController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        // Verion 1.0 - Fetch only from 1 database view and present the result
        /*
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

        */

        

        // Version 2.0 - Bind the received data to the Consultant model

        // HTTP request with GET method to get All the data on a Consultant with the ID
        [HttpGet("{id}")]
        public Consultant Get(int id)
        {
            Consultant = new Consultant();


            string sqlDataSource = _configuration.GetConnectionString("ConsultantApiConnection");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                // 1. Get data from ConsultantsList view and bind it to the Consultant object

                // This query should be written with Stored procedure and with parameters
                string query = @"
                    SELECT * FROM dbo.ConsultantsList WHERE ConsultantId = " + id + @" ";

                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    using (SqlDataReader oReader = myCommand.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            Consultant.ConsultantId = Convert.ToInt32(oReader["ConsultantId"]);
                            Consultant.FirstName = oReader["FirstName"].ToString();
                            Consultant.LastName = oReader["LastName"].ToString();
                            Consultant.CityName = oReader["CityName"].ToString();
                            Consultant.StateAbbreviation = oReader["StateAbbreviation"].ToString();
                            Consultant.CountryAbbreviation = oReader["CountryAbbreviation"].ToString();
                            Consultant.HourlyRateShortProject = Convert.ToInt32(oReader["HourlyRateShortProject"].ToString());
                            Consultant.HourlyRateMediumProject = Convert.ToInt32(oReader["HourlyRateMediumProject"].ToString());
                            Consultant.HourlyRateLongProject = Convert.ToInt32(oReader["HourlyRateLongProject"].ToString());
                            Consultant.ProfilePicture = oReader["ProfilePicture"].ToString();
                            
                        }

                    }


                }


                // 2. Get Titles and bind it to the Consultant object

                // This query should be written with Stored procedure and with parameters
                string query2 = @"
                    SELECT TitleName FROM TitlesView WHERE ConsultantId = " + id + @" ";

                List<string> Titles = new List<string>();

                using (SqlCommand myCommand = new SqlCommand(query2, myCon))
                {

                    using (SqlDataReader oReader = myCommand.ExecuteReader())
                    {
                        
                        while (oReader.Read())
                        {
                            Titles.Add(oReader["TitleName"].ToString());
                        }

                    }


                }

                Consultant.Titles = Titles;


                // 3. Get WorkExperience and bind it to the Consultant object

                // This query should be written with Stored procedure and with parameters
                string query3 = @"
                    SELECT JobTitle, CompanyName, StartDate, EndDate, Descrption, IndustryName
                    FROM WorkExperienceView WHERE ConsultantId = " + id + @" ";

                List<WorkExperienceDetails> WorkExpList = new List<WorkExperienceDetails>();
                


                using (SqlCommand myCommand = new SqlCommand(query3, myCon))
                {

                    using (SqlDataReader oReader = myCommand.ExecuteReader())
                    {

                        while (oReader.Read())
                        {
                            WorkExperienceDetails WorkExp = new WorkExperienceDetails();
                            WorkExp.JobTitle = oReader["JobTitle"].ToString();
                            WorkExp.CompanyName = oReader["CompanyName"].ToString();
                            WorkExp.StartDate = Convert.ToDateTime(oReader["StartDate"]);
                            if (!string.IsNullOrEmpty(oReader["EndDate"].ToString()))
                            {
                                WorkExp.EndDate = Convert.ToDateTime(oReader["EndDate"]);
                            };
                            WorkExp.Description = oReader["Descrption"].ToString();
                            WorkExp.IndustryName = oReader["IndustryName"].ToString();

                            WorkExpList.Add(WorkExp);
                        }
                    }

                    myCon.Close();

                }

                Consultant.WorkExperience = WorkExpList;



            }

            return Consultant;

        }



    }
}
