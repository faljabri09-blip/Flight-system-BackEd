using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd.Models
{
    public class Flight
    {

        public int flightId { get; set; } // system generated id
        public string flightCode { get; set; } // user input flight number
        public int aircraftId { get; set; } // user input aircraft id
        public int pilotId { get; set; } // user input pilot id
        public string origin { get; set; } // user input origin
        public string destination { get; set; } // user input destination
        public string departureDate { get; set; } // user input departure time
        public string departureTime { get; set; } // user input departure time
        public string arrivalTime { get; set; } // user input arrival time
        public decimal ticketPrice { get; set; } // user input ticket price
        public int availableSeats { get; set; } // user input available seats
        public int FlightDuration { get; set; } // user input flight duration
        public string status { get; set; } // user input flight status (e.g., scheduled, delayed, cancelled)





    }
}
