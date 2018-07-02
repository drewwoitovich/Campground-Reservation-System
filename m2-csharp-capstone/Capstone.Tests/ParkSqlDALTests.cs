using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Models;
using Capstone.DAL;



namespace Capstone.Tests
{
    [TestClass]
    public class ParkSqlDALTests
    {
        [TestClass()]
        public class CitySqlDALTests
        {
            private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
            private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            private int parkCount = 0;                 //<-- used to hold the city id of the row created for our test


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


                }
            }

            // Cleanup runs after every single test
            [TestCleanup]
            public void Cleanup()
            {
                tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
            }

            [TestMethod]
            public void ViewAvailableParksTest()
            {
                //Arrange
                ParkSqlDAL parkSqlDal = new ParkSqlDAL(connectionString);

                //Act
                List<Park> parks = parkSqlDal.ViewAvailableParks();

                //Assert
                Assert.IsNotNull(parkSqlDal);
                Assert.AreEqual(parkCount + 1, parks.Count);
            }
        }
    }
}
