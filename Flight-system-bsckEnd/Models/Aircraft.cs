using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_system_bsckEnd.Models
{
    public class Aircraft
    {
        public int aircraftId { get; set; } // system generated id
        public string model { get; set; } // user input name
        public int totalSeats { get; set; } // user input 
        public bool isOperational { get; set; } // user input 
    }
}
