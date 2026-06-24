using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd.Models
{
    public class Pilot
    {

        public int pilotId { get; set; } // system generated id
        public string pilotName { get; set; } //user input name
        public string pilotPhone { get; set; } // user input phone number
        public string pilotLicenseNumber { get; set; } // user input license number
        public int flightHours { get; set; } // user input flight hours
        public bool isAvailable { get; set; } // user input availability status

    }
}
