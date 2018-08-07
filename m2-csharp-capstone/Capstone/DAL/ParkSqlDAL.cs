using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        private const string SQL_ViewAvailableParks = "SELECT * FROM park";
        private const string SQL_ViewParkInfo = "SELECT * FROM park WHERE park.park_id = @parkid";

        //constructor
        public ParkSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        // Returns a list of all parks in the database table "park"
        public List<Park> ViewAvailableParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_ViewAvailableParks;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();

                        p.ParkId = Convert.ToInt32(reader["park_id"]);
                        p.ParkName = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.DateEstablished = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.AnnualVisitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);

                        output.Add(p);
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

        // Returns all details of a given park
        public Park ViewParkInfo(int parkId)
        {
            Park p = new Park();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_ViewParkInfo;
                    cmd.Parameters.AddWithValue("@parkid", parkId);
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        p.ParkId = Convert.ToInt32(reader["park_id"]);
                        p.ParkName = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.DateEstablished = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.AnnualVisitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);                        
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There was an error.");
                Console.WriteLine(e.Message);
                throw;
            }

            return p;
        }

    }
}
