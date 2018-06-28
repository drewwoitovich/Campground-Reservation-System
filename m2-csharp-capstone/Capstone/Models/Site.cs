using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public int SiteNumber { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
