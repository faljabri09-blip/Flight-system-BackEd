using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd.Models
{
    public class Booking
    {
        public int bookingId { get; set; } // system generated id
        public int passengerId { get; set; } // user input passenger id
        public int flightId { get; set; } // user input flight id

        public string seatNumber { get; set; } // user input seat number
        public string bookingDate { get; set; } // user input booking date
        public decimal totalPrice { get; set; } // user input total price

        public string bookingStatus { get; set; } // user input booking status (e.g., confirmed, cancelled)
    }
}
