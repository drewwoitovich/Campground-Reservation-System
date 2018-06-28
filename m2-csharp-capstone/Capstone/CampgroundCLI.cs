using System;
using System.Collections.Generic;
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
        const string Command_ViewCampgrounds = "2";
        const string Command_Quit = "q";
        const string DatabaseConnectionString = @"";

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

                    case Command_ViewCampgrounds:
                        Console.WriteLine("Please enter the name of the park you would like to search: ");
                        string parkName = Console.ReadLine();
                        //ViewCampgrounds(parkName);
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

        }
    }
}
