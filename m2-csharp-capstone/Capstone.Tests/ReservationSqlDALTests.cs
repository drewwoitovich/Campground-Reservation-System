using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;


namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDALTests
    {
        private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int campgroundID;                 //<-- used to hold the campgroundID of the row created for our test
        private DateTime startDate = Convert.ToDateTime("01/01/2019");
        private DateTime endDate = Convert.ToDateTime("01/05/2019");
        private int parkID;
        private int siteID;
        private int reservationID;

        [TestInitialize]
        public void Initialize()
        {

            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                //Insert a Dummy Record for Park                
                cmd = new SqlCommand("INSERT INTO park VALUES ('Alum Creek State Park', 'Lewis Center OH', '1970-01-01', 450, 12000, 'I just made up a bunch of values for this state park'); SELECT SCOPE_IDENTITY();", conn);
                parkID = Convert.ToInt32(cmd.ExecuteScalar());

                //Insert a Dummy Record for campground that belongs to Alum Creek
                //If we want to the new id of the record inserted we can use
                // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                // This will get the newest identity value generated for the record most recently inserted
                cmd = new SqlCommand("INSERT INTO campground VALUES (@campground_id, @park_id, 'Alum Creek Campground', 3, 8, 10.00); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@campground_id", campgroundID);
                cmd.Parameters.AddWithValue("@park_id", parkID);
                campgroundID = Convert.ToInt32(cmd.ExecuteScalar());
              

                cmd = new SqlCommand($"INSERT INTO site VALUES ("+campgroundID+", 1, 5, 0, 0, 0); SELECT SCOPE_IDENTITY();", conn);
                
                siteID = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand($"INSERT INTO reservation VALUES ("+siteID+", 'Drew', "+startDate+", "+endDate+", '07/03/2018');", conn);
                
                reservationID = Convert.ToInt32(cmd.ExecuteScalar());
                
            }
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }


        [TestMethod]
        public void SearchForAvailableReservationTest()
        {
            ReservationSqlDAL myDAL = new ReservationSqlDAL(connectionString);

            List<Site> testReservationList = myDAL.SearchForAvailableReservation(siteID, startDate, endDate);
            Assert.AreEqual(testReservationList.Count, 0);

            List<Site> testReservationList2 = myDAL.SearchForAvailableReservation(siteID, Convert.ToDateTime("02/01/2019"), Convert.ToDateTime("02/02/2019"));
            Assert.AreEqual(testReservationList.Count, 1);

        }
    }
}
