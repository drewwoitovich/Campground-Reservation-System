using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public string Location { get; set; }
        public DateTime DateEstablished { get; set; } 
        public int Area { get; set; }
        public int AnnualVisitors { get; set; }
        public string Description { get; set; }

        public string ToStringLong()
        {
            return $"{ParkName} National Park\nLocation: {Location}\nDate Established: {DateEstablished.ToString("M/d/yyyy")}\nArea: {Area} sq km\nAnnual Visitors: {AnnualVisitors}\n\n{Description}";// + DateEstablished.ToString().PadRight(10) + Area.ToString().PadRight(15) + AnnualVisitors.ToString().PadRight(10) + Description.PadRight(15)";
        }

        public override string ToString()
        {
            return ParkId.ToString().PadRight(3) + ParkName.PadRight(5);
        }
    }
}
