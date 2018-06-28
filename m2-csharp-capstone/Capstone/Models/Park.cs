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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
