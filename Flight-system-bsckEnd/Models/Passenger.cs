using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd.Models
{
    public class Passenger
    {

        public int passengerId { get; set; } // system generated id
        public string passengerName { get; set; } //user input name
        public string passengerEmail { get; set; } // user input email
        public string passengerPhone { get; set; } // user input phone number
        public string passengerNumber { get; set; } // user input passport number
        public string nationality { get; set; } // user input nationality

    }
}
