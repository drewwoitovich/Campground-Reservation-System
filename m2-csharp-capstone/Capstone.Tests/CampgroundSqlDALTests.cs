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
    public class CampgroundSqlDALTests
    {
        [TestClass()]
        public class CitySqlDALTests
        {
            private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
            private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            private int cityId;                 //<-- used to hold the city id of the row created for our test


            // Set up the database before each test        
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
                    cmd = new SqlCommand("INSERT INTO park VALUES (50, 'Alum Creek State Park', 'Lewis Center OH', '1970-01-01', 450, 12000, 'I just made up a bunch of values for this state park');", conn);
                    cmd.ExecuteNonQuery();

                    //Insert a Dummy Record for City that belongs to 'ABC Country'
                    //If we want to the new id of the record inserted we can use
                    // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                    // This will get the newest identity value generated for the record most recently inserted
                    cmd = new SqlCommand("INSERT INTO campground VALUES (50, 50, 'Alum Creek Campground', 3, 8, 10.00);", conn);
                    cityId = (int)cmd.ExecuteScalar();
                }
            }

            // Cleanup runs after every single test
            [TestCleanup]
            public void Cleanup()
            {
                tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
            }

            [TestMethod]
            public void ViewCampgroundsTest()
            {
                // Arrange 
                CampgroundSqlDAL campgroundDal = new CampgroundSqlDAL(connectionString);

                //Act
                List<Campground> campgrounds = campgroundDal.ViewCampgrounds(50); //<-- use our dummy country 

                //Assert
                Assert.AreEqual(1, campgrounds.Count);               // We should only have one city in ABC country
                Assert.AreEqual(50, campgrounds[0].CampgroundId);      // We created the city ahead of time and know the id to check for
            }
        }
    }
}
