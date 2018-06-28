using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string connectionString;
        private const string SQL_ViewCampgrounds = "SELECT * FROM campground c RIGHT OUTER JOIN park p ON p.park_id = c.park_id WHERE p.name = @parkName";

        //constructor
        public CampgroundSqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<string> ViewCampgrounds(string parkName)
        {

            List<string> output = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@p.name", parkName);
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string campground = Convert.ToString(reader["campground"]);
                        output.Add(campground);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There was an error.");
                Console.WriteLine(e.Message);
            }

            output.Sort();
            return output;
        }
    }
}
