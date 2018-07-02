using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        
        public override string ToString()
        {
            return SiteNumber.ToString().PadRight(15) + MaxOccupancy.ToString();
        }
    }
}
