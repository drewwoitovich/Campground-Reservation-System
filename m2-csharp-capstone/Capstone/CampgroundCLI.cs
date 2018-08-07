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

        // Establishes database connection
        public CampgroundCLI()
        {
            databaseConnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }

        // Starts the program
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

        // First menu that's displayed
        private void PrintMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("Type in a command");
            Console.WriteLine("(1) View available parks");
            Console.WriteLine("(q) Quit");
        }

        // Calls the DAO and gives options to user on what to do next
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

        // Allows user to select a certain park in order to view the details of that park
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
            ParkInterface(userInput);
            
        }

        // Next menu once a park is selected. Allows user to view campgrounds within that park, search for 
        // available reservations, or return to the main menu
        private void ParkInterface(int parkNumber)
        {
            Console.WriteLine("Select a command");
            Console.WriteLine("(1) View Campgrounds");
            Console.WriteLine("(2) Search for a Reservation");
            Console.WriteLine("(3) Return to Main Menu");

            int userInput = int.Parse(Console.ReadLine());

            switch(userInput)
            {
                case 1:
                    ViewCampgrounds(parkNumber);
                    ParkInterface(parkNumber);
                    break;

                case 2:
                    SearchForAvailableReservationInterface(parkNumber);
                    break;

                case 3:
                    RunCLI();
                    break;

                default:
                    Console.WriteLine("Please enter a valid command");
                    break;
            }
        }

        // Called when a user chooses to search for a reservation
        private void ReservationInterface(int parkNumber)
        {
            Console.WriteLine("(1) Search for available reservation");
            Console.WriteLine("(2) Return to previous screen");

            int userInput = int.Parse(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    ViewCampgrounds(parkNumber);
                    SearchForAvailableReservationInterface(parkNumber);
                    break;

                case 2:
                    ParkInterface(parkNumber);
                    break;

                default:
                    Console.WriteLine("Please enter a valid command");
                    return;
            }
        }

        // Prompts the user to select a campsite, start date, and end date in order to search for available campsites
        private void SearchForAvailableReservationInterface(int parkNumber)
        {
            ViewCampgrounds(parkNumber);
            Console.WriteLine("Which campground (enter 0 to cancel)?");
            int campgroundSelection = int.Parse(Console.ReadLine());
            if (campgroundSelection == 0)
            {
                ReservationInterface(parkNumber);
            }
            else
            {
                Console.WriteLine("What is the arrival date? __/__/____");
                DateTime arrivalDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("What is the departure date? __/__/____");
                DateTime departureDate = DateTime.Parse(Console.ReadLine());

                SearchForAvailableReservation(campgroundSelection, arrivalDate, departureDate);
            }
        }

        // Uses a DAO to view a list of all campgrounds within a given park
        public void ViewCampgrounds(int parkNumber)
        {

            CampgroundSqlDAL myDal = new CampgroundSqlDAL(databaseConnectionString);
            List<Campground> camps = myDal.ViewCampgrounds(parkNumber);

            Console.WriteLine("Camp ID            Name                                     Open Month        Close Month           Daily Fee");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
            foreach (Campground camp in camps)
            {
                Console.WriteLine(camp);
            }
            Console.WriteLine();
        }

        // Called after user chooses a campsite, start date, and end date in order to search for available reservations.
        // Uses a DAO to search the database searching for available sites within the given dates and returns a list of
        // those that are available
        public void SearchForAvailableReservation(int campgroundSelection, DateTime arrivalDate, DateTime departureDate)
        {
            ReservationSqlDAL myDAL = new ReservationSqlDAL(databaseConnectionString);
            List<Site> availableSites = myDAL.SearchForAvailableReservation(campgroundSelection, arrivalDate, departureDate);

            Console.WriteLine("Site ID    Max Occupancy");
            foreach (Site site in availableSites)
            {
                Console.WriteLine(site);
            }
            Console.WriteLine();
            ConfirmReservation(campgroundSelection, arrivalDate, departureDate);
        }

        // Called after viewing a list of available (unreserved) sites. Prompts the user to select a which available
        // campsite they'd like to make a reservation for. Prompts the user to give a name for the reservation. Attempts
        // to create the reservation and update the database. If successful, it will give return their reservation
        // ID as confirmation
        public void ConfirmReservation(int campgroundSelection, DateTime arrivalDate, DateTime departureDate)
        {
            Console.WriteLine("Which site should be reserved (0 to cancel)?");
            int siteSelection = int.Parse(Console.ReadLine());
            if (siteSelection == 0)
            {
                ViewAvailableParks();
            }
            else
            {
                Console.WriteLine("What name should the reservation be made under?");
                string reservationName = Console.ReadLine();

                ReservationSqlDAL myDAL = new ReservationSqlDAL(databaseConnectionString);
                Reservation newReservation = new Reservation();


                myDAL.MakeReservation(siteSelection, reservationName, arrivalDate, departureDate);
            }
        }
        
    }
}
