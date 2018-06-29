using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;



namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {

        private string connectionString;
        private const string SQL_ReservationSearch = "select s.site_id, s.max_occupancy, c.daily_fee FROM site s join campground c on s.campground_id = c.campground_id where s.campground_id = @campground_id AND s.site_id NOT IN (SELECT site_id from reservation WHERE @requested_start<to_date AND @requested_end> from_date);";

        //constructor
        public ReservationSqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<Site> SearchForAvailableReservation(int campgroundId, DateTime startDate, DateTime endDate)
        {
            List<Site> output = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_ReservationSearch;
                    cmd.Parameters.AddWithValue("@campground_id", campgroundId);
                    cmd.Parameters.AddWithValue("@requested_start", startDate);
                    cmd.Parameters.AddWithValue("@requested_end", endDate);
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();

                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

                        output.Add(s);
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
