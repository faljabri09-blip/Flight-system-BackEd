using Flight_system_bsckEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd
{
    public class FlightContext
    {

        public List<Aircraft> aircrafts { get; set; }
        public List<Flight> flights { get; set; }
        public List<Booking> bookings { get; set; }
        public List<Pilot> pilots { get; set; }
        public List<Passenger> passengers { get; set; }


    }
}
