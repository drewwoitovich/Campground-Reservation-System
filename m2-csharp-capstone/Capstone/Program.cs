using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            CampgroundCLI cli = new CampgroundCLI();
            cli.RunCLI();
        }
    }
}
