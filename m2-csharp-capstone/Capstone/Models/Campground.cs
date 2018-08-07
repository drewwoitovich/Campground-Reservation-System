using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public string CampgroundName { get; set; }
        public int OpenDate { get; set; }
        public int CloseDate { get; set; }
        public decimal DailyFee { get; set; } 

        // Formats campground details to look nice and neat
        public override string ToString()
        {
            return CampgroundId.ToString().PadRight(15) + CampgroundName.ToString().PadRight(50) + OpenDate.ToString().PadRight(15) + CloseDate.ToString().PadRight(20) + "$" + DailyFee.ToString();
        }
    }
}
