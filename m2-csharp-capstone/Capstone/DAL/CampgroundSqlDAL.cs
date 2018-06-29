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
        private const string SQL_ViewCampgrounds = "SELECT * FROM campground c JOIN park p ON p.park_id = c.park_id WHERE p.park_id = @park_Id";

        //constructor
        public CampgroundSqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<Campground> ViewCampgrounds(int parkId)
        {

            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@park_id", parkId);
                    cmd.Connection = connection;
                    cmd.CommandText = SQL_ViewCampgrounds;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();

                        c.CampgroundName = Convert.ToString(reader["name"]);

                        output.Add(c);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There was an error.");
                Console.WriteLine(e.Message);
                throw;
            }
            
            return output;
        }
    }
}
