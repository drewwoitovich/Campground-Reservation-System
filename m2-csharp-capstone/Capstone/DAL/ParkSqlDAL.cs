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
        private const string SQL_ViewAvailableParks = "";

        //constructor
        public ParkSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<string> ViewAvailableParks()
        {
            List<string> output = new List<string>();

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
                        string parks = Convert.ToString(reader["park"]);
                        output.Add(parks);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return output;
        }
    }
}
