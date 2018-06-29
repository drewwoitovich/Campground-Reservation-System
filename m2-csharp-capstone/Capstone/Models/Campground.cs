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
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal DailyFee { get; set; } 

        public override string ToString()
        {
            return $"{CampgroundId}      {CampgroundName}            {OpenDate.ToString("MM/dd")}              {CloseDate.ToString("MM/dd")}    {DailyFee}";
        }
    }
}
