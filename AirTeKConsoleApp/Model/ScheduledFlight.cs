using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTeKConsoleApp.Model
{
    public class ScheduledFlight
    {
        public Flight Flight { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
