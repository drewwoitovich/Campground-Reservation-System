using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class CampgroundCLI
    {
        const string Command_ViewParks = "1";
        const string Command_Quit = "q";
        private string databaseConnectionString = "";

        public CampgroundCLI()
        {
            databaseConnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }
        public void RunCLI()
        {
            PrintMenu();

            while(true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ViewParks:                        
                        ViewAvailableParks();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Have a nice day");
                        return;

                    default:
                        Console.WriteLine("Please enter a valid command");
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("Type in a command");
            Console.WriteLine("(1) View available parks");
            Console.WriteLine("(q) Quit");
        }

        private void ViewAvailableParks()
        {
            ParkSqlDAL parkDal = new ParkSqlDAL(databaseConnectionString);

            List<Park> parks = parkDal.ViewAvailableParks();

            Console.WriteLine();
            Console.WriteLine("List of available parks: ");
            Console.WriteLine();
            Console.WriteLine("Select a park number for further details");

            foreach (Park park in parks)
            {
                Console.WriteLine(park);
            }

            ViewParkInfo();
        }

        private void ViewParkInfo()
        {
            ParkSqlDAL parkDal = new ParkSqlDAL(databaseConnectionString);

            Console.WriteLine();
            int userInput = int.Parse(Console.ReadLine());

            Park park = parkDal.ViewParkInfo(userInput);

            Console.WriteLine();
            Console.WriteLine("Park Information: ");
            Console.WriteLine(park.ToStringLong());

            Console.WriteLine();
            SelectCommand(userInput);
        }

        private void SelectCommand(int parkNumber)
        {
            Console.WriteLine("Select a command");
            Console.WriteLine("(1) View Campgrounds");
            Console.WriteLine("(2) Search for a Reservation");
            Console.WriteLine("(3) Return to Main Menu");

            string userInput = Console.ReadLine();

            switch(userInput)
            {
                case "1":
                    CampgroundSqlDAL myDal = new CampgroundSqlDAL(databaseConnectionString);
                    List<Campground> camps = myDal.ViewCampgrounds(parkNumber);
                    foreach (Campground camp in camps)
                    {
                        Console.WriteLine(camp);
                    }
                    Console.WriteLine();
                    break;

                case "2":
                    break;

                case "3":
                    RunCLI();
                    break;

                default:
                    Console.WriteLine("Please enter a valid command");
                    break;
            }
        }

        private void ViewCampgrounds()
        {
            
        }
    }
}
