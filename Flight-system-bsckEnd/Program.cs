using Flight_system_bsckEnd.Models;
using System.Numerics;

namespace Flight_system_bsckEnd
{
    public class Program
    {

        public static FlightContext context = new FlightContext
        {
            aircrafts = new List<Aircraft>(),
            flights = new List<Flight>(),
            bookings = new List<Booking>(),
            pilots = new List<Pilot>(),
            passengers = new List<Passenger>()
        };

        public static void RegisterPassenger(List<Passenger> passengers)
        {

            //data entry by passenger
            Console.WriteLine("Enter passenger Name:");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Enter passenger Email:");
            string passengerEmail = Console.ReadLine();

            Console.WriteLine("Enter passenger Phone:");
            string passengerPhone = Console.ReadLine();

            Console.WriteLine("Enter passenger number:");
            string passengerNumber = Console.ReadLine();

            Console.WriteLine("Enter your nationality");
            string nationality = Console.ReadLine();

            //passenger id is system generated

            int passengerId = context.passengers.Count + 1;

            context.passengers.Add(
                new Passenger
                {
                    passengerId = passengerId,
                    passengerName = passengerName,
                    passengerEmail = passengerEmail,
                    passengerPhone = passengerPhone,
                    passengerNumber = passengerNumber,
                    nationality = nationality
                });

            Console.WriteLine($"Passenger registered successfully , Assigned ID: {passengerId}");
        }

        public static void AddAnAircraft(List<Aircraft> aircrafts)
        {
            Console.WriteLine("Enter Aircraft Model:");
            string model = Console.ReadLine();

            Console.WriteLine("Enter Total seate");
            int totalSeate = Convert.ToInt32(Console.ReadLine());

            int aircraftId = context.aircrafts.Count + 1;

            context.aircrafts.Add(
                new Aircraft
                {
                    aircraftId = aircraftId,
                    model = model,
                    totalSeats = totalSeate,
                    isOperational = true
                });

            Console.WriteLine($"Aircraft added successfully , Assigned ID: {aircraftId}");
        }

        public static void RegisterPilot(List<Pilot> pilots)
        {

            Console.WriteLine("Enter Pilot Name:");
            string pilotName = Console.ReadLine();

            Console.WriteLine("Enter Pilot Phone:");
            string pilotPhone = Console.ReadLine();

            Console.WriteLine("Enter Pilot license Number:");
            string pilotLicenseNumber = Console.ReadLine();

            Console.WriteLine("Enter Flight Hours:");
            int flightHours = Convert.ToInt32(Console.ReadLine());

            int pilotId = context.pilots.Count + 1;

            context.pilots.Add(
                new Pilot
                {
                    pilotId = pilotId,
                    pilotName = pilotName,
                    pilotPhone = pilotPhone,
                    pilotLicenseNumber = pilotLicenseNumber,
                    flightHours = flightHours,
                    isAvailable = true
                });

            Console.WriteLine($"Piolt register successfully , Assigne ID : {pilotId}");
        }

        public static void viewAllFlights(List<Flight> flights)
        {
            if (flights.Count == 0)
            {
                Console.WriteLine("Flight not found");
                return;
            }

            foreach (Flight F in context.flights)
            {
                Console.WriteLine($"Flight code: {F.flightCode} | Flight origin: {F.origin} | Flight destination: {F.destination} | Departue date : {F.departureDate} | " +
                    $"Departue Time : {F.departureTime} | Available Seats: {F.availableSeats} | Ticket Price : {F.ticketPrice} | Current Status: {F.status}");
            }
        }

        public static void ScheduleAFlight(FlightContext context)
        {
            // Get the list of available aircrafts and pilots
            //check if the aircraft and pilot are already scheduled for a flight
            Console.WriteLine("Enter Aircraft ID:");
            int aircraftId = int.Parse(Console.ReadLine());

            List<Flight> aircraftFlights = context.flights.Where(f => f.aircraftId == aircraftId).ToList();

            if (aircraftFlights.Count > 0)
            {
                Console.WriteLine("Aircraft is already scheduled for a flight. Please choose another aircraft.");
                return;
            }

            Console.WriteLine("Enter Pilot ID:");
            int pilotId = int.Parse(Console.ReadLine());

            List<Flight> pilotFlights = context.flights.Where(f => f.pilotId == pilotId).ToList();

            if (pilotFlights.Count > 0)
            {
                Console.WriteLine("Pilot is already scheduled for a flight. Please choose another pilot.");
                return;
            }

            // Get the total seats of the selected aircraft
            int totalSeats = context.aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId)?.totalSeats ?? 0;


            // Check if the aircraft ID is valid
            if (totalSeats == 0)
            {
                Console.WriteLine("Invalid Aircraft ID");
                return;
            }


            // Generate a unique flight ID
            int flightId = context.flights.Count + 1;


            // Create a new flight object and add it to the flights list (flight record)

            context.flights.Add(
                new Flight
                {
                    flightId = flightId,
                    flightCode = "OA-201",
                    aircraftId = aircraftId,
                    pilotId = pilotId,
                    origin = "Oman",
                    destination = "Dubai",
                    departureDate = "2024-06-01",
                    departureTime = "10:00 AM",
                    arrivalTime = "12:00 PM",
                    ticketPrice = 200.00m,
                    availableSeats = totalSeats,
                    status = "Scheduled"
                });

            Console.WriteLine($"Flight scheduled successfully , Assigned ID: {flightId}");


        }

        public static void BookAFlight(FlightContext context)
        {

            // Get the passenger name and flight ID from the user
            // Check if the passenger exists in the passengers list
            // Check if the destination exists in the flights list
            Console.WriteLine("Enter Passenger Name:");
            string passengerName = Console.ReadLine();

            var passenger = context.passengers.FirstOrDefault(p => p.passengerName == passengerName);

            if (passenger == null)

            {
                Console.WriteLine("Passenger not found");
                return;
            }



            // Get the flight ID from the user and check if the flight exists
            Console.WriteLine("Enter flight destination:");
            string destination = Console.ReadLine();

            var flights = context.flights.Where(f => f.destination == destination
                                                  && f.status == "Scheduled"
                                                  && f.availableSeats > 0).ToList();

            if (flights == null)
            {
                Console.WriteLine("destination not found");
                return;
            }

            foreach (var f in flights)
            {
                Console.WriteLine($"Flight ID: {f.flightId}, Code: {f.flightCode}, Seats: {f.availableSeats} , Destination : {destination}");
            }


            Console.WriteLine("Enter flight ID to book:");
            int flightId = int.Parse(Console.ReadLine());

            var flight = context.flights.FirstOrDefault(f => f.flightId == flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found");
                return;
            }

            //system generated booking id
            int bookingId = context.bookings.Count + 1;


            // Assign a seat number based on the number of existing bookings for that flight
            int seatFlighrCount = context.bookings.Count(b => b.flightId == flight.flightId);

            // Assuming seat numbers are in the format "A1", "A2", etc.
            string seatNumber = $"A{seatFlighrCount + 1}";

            context.bookings.Add(
                new Booking
                {
                    bookingId = bookingId,
                    passengerId = passenger.passengerId,
                    flightId = flightId,
                    seatNumber = seatNumber,
                    bookingDate = "2026-10-5",
                    totalPrice = flight.ticketPrice, // taken from the flight ticket price
                    bookingStatus = "Confirmed"

                });

            Console.WriteLine($"Booking successful , Assigned Seat Number:{seatNumber}");

            //seat count decrease by 1 after booking
            flight.availableSeats = flight.availableSeats - 1;
        }

        public static void CancelBooking(FlightContext context)
        {

            Console.WriteLine("Enter Booking ID to Cancel:");
            int bookingId = int.Parse(Console.ReadLine());

            Booking booked = context.bookings.FirstOrDefault(b => b.bookingId == bookingId);

            if (booked == null)
            {
                Console.WriteLine("Booking not found");
                return;
            }

            Flight flight = context.flights.FirstOrDefault(f => f.flightId == booked.flightId);

            if (flight == null)
            {
                Console.WriteLine("Flight not found");
                return;
            }

            if (booked.bookingStatus == "Cancelled")
            {
                Console.WriteLine("Booking is already cancelled");
                return;
            }

            booked.bookingStatus = "Cancelled";

            // seats count increase by 1 after cancellation
            flight.availableSeats++;


            Console.WriteLine($"Booking :{bookingId} has been cancelled and the seate number : {booked.seatNumber} is now available again.");

        }

        public static void DepartFlight(List<Flight> flights)
        {

            //user input for flight ID to depart
            Console.WriteLine("Enter Flight ID to depart:");
            int flightId = int.Parse(Console.ReadLine());

            //search for the flight in the flights list based on the provided flight ID
            Flight departFlight = context.flights.FirstOrDefault(d => d.flightId == flightId);

            //check if the flight exists and if it is in a valid state for depart
            if (departFlight == null)
            {
                Console.WriteLine("Flight not found");
                return;
            }


            //check if the flight is already departed before updating the status to Departed
            if (departFlight.status == "Departed")
            {
                Console.WriteLine("Flight has already departed");
                return;
            }


            //check if the flight is cancelled before updating the status to Departed
            if (departFlight.status == "Cancelled")
            {
                Console.WriteLine("Cannot depart a cancelled flight");
                return;
            }

            // Update the flight status to "Departed"
            departFlight.status = "Departed";

            // Display a message confirming the departure of the flight
            Console.WriteLine($"Flight : {departFlight.flightId} has departed successfully , Flight Status : {departFlight.status}");
        }

        public static void CancelFlight(List<Flight> flights)
        {
            //user input for flight ID to cancel
            Console.WriteLine("Enter Flight ID");
            int flightId = int.Parse(Console.ReadLine());

            //search for the flight in the flights list 
            Flight cancelFlight = context.flights.FirstOrDefault(f => f.flightId == flightId);

            //check if the flight exists and if it is in a valid state for cancellation
            if (cancelFlight == null)
            {
                Console.WriteLine("Flight not found");
                return;
            }

            //check if the flight is already departed before updating the status to Cancelled
            if (cancelFlight.status == "Cancelled")
            {
                Console.WriteLine("Flight is already cancelled");
                return;
            }

            //cencelled flight 
            cancelFlight.status = "Cancelled";

            //cancelled every confirmed booking for the flight
            List<Booking> bookingsCancelled = context.bookings.Where(b => b.flightId == flightId
                                                                         && b.bookingStatus == "Confirmed").ToList();


            //set the booking status to "Cancelled" for each confirmed booking 
            foreach (Booking booking in bookingsCancelled)
            {
                booking.bookingStatus = "Cancelled";
            }

            Console.WriteLine($"Flight : {cancelFlight.flightId} has been cancelled successfully , Flight Status : {cancelFlight.status}");

            //return the seats to the flight's available seats count
            cancelFlight.availableSeats += bookingsCancelled.Count;


            // Display the number of bookings that were Affected
            Console.WriteLine($"Affected booking : {bookingsCancelled.Count}");


            // Display a message confirming that all cancelled seats are now available again
            Console.WriteLine("All cancle seats are available againe");

            //search for the pilot associated with the cancelled flight 
            Pilot pilot = context.pilots.FirstOrDefault(p => p.pilotId == cancelFlight.pilotId);

            //check if the pilot exists and if the flight is cancelled before displaying the message
            if (pilot != null && cancelFlight.status == "Cancelled")
            {
                Console.WriteLine($"Pilot ID: {pilot.pilotId} is now available for other flights.");
            }

        }

        public static void PassengerBookingHistory(FlightContext context)
        {
            //user input for passenger id to view booking history
            Console.WriteLine("Enter passenger id to view booking history:");
            int passengerId = int.Parse(Console.ReadLine());

            //search for the passenger in the passengers list
            Passenger passenger = context.passengers.FirstOrDefault(p => p.passengerId == passengerId);


            //check if the passenger exists for view booking history
            if (passenger == null)
            {
                Console.WriteLine("Passenger not found");
                return;
            }

            //search for the bookings associated with the passenger ( booking class)
            var passengerBooking = context.bookings.Where(b => b.passengerId == passengerId).ToList();


            //check if the passenger has any booking history
            if (passengerBooking.Count == 0)
            {
                Console.WriteLine("No booking history found for this passenger");
                return;
            }


            //search for the flight associated with each booking and display the booking history
            foreach (Booking booking in context.bookings)
            {

                //search for the flight associated with the booking
                Flight flight = context.flights.FirstOrDefault(f => f.flightId == booking.flightId);


                //check if the flight exists before displaying the booking history
                if (flight != null)
                {
                    Console.WriteLine($"Booking ID: {booking.bookingId}, Passenger: {passenger.passengerName}, Flight Code: {flight.flightCode}, " +
                        $"Origin: {flight.origin}, Destination: {flight.destination},Depart Date: {flight.departureDate} ,Booking Status: {booking.bookingStatus} " +
                        $"Number of seat : {booking.seatNumber}, Price paid : {booking.totalPrice}");
                }

                //calculate the total amount paid for confirmed bookings by the passenger
                decimal totalAmount = 0;

                //check if the booking status is "Confirmed" before adding the total amount
                if (booking.bookingStatus == "Confirmed")
                {
                    totalAmount += booking.totalPrice; // totalAmount = totalAmount + booking.totalPrice;
                }

                //display the total amount paid for confirmed bookings by the passenger
                Console.WriteLine($"Total Confirmed Amount : {totalAmount}");
            }


        }

        public static void FlightRevenueAndLoadFactorReport(FlightContext context)
        {

            //search for all flights and order by descending.
            var flights = context.flights.OrderByDescending(f => context.bookings
                                                  .Where(b => b.flightId == b.flightId && b.bookingStatus == "Confirmed")
                                                  .Sum(b => b.totalPrice));

            //search for each flight and calculate the total confirmed bookings, total revenue, and load factor
            foreach (Flight flight in context.flights)
            {

                //search for all confirmed bookings for the flight
                var Confirmedbooking = context.bookings.Where(b => b.flightId == flight.flightId
                                                              && b.bookingStatus == "Confirmed")
                                                       .ToList();

                //calculate the total number of confirmed bookings for the flight
                int totalBooking = Confirmedbooking.Count;

                //calculate the total revenue generated from confirmed bookings for the flight
                decimal totalRevenue = Confirmedbooking.Sum(b => b.totalPrice);


                //search for the aircraft associated with the flight
                var aircraft = context.aircrafts.FirstOrDefault(a => a.aircraftId == flight.aircraftId);

                //calculate the load factor for the flight of total confirmed bookings to total seats of the aircraft
                decimal percentageLoadFactor = 0;

                if (aircraft != null)
                {

                    percentageLoadFactor = totalBooking / context.aircrafts.Sum(a => a.totalSeats) * 100;

                }

                //calculate the total grand revenue for all flights
                decimal totalGrandRevenue = 0;

                totalGrandRevenue += totalRevenue;


                //print report for each flight including flight code, total revenue, route, total confirmed bookings, load factor, and grand revenue
                Console.WriteLine($"flight Code: {flight.flightCode}");

                Console.WriteLine($"Total Revenue : {totalRevenue}");

                Console.WriteLine($"Route : {flight.origin} to {flight.destination}");

                Console.WriteLine($"Total Confirmed booking : {totalBooking}");

                Console.WriteLine($"Total Revenue : {totalRevenue}");

                Console.WriteLine($"Total Load Factor : {percentageLoadFactor}");

                Console.WriteLine($"Total Grand Revenue: {totalGrandRevenue}");
            }
        }


        static void Main(string[] args)
        {

            bool exit = false;
            while (exit == false)
            {

                Console.WriteLine("Welcome to the Flight System");
                Console.WriteLine("^--------------------------------------------^");
                Console.WriteLine("1. Register a Passenger");
                Console.WriteLine("2. Add an Aircraft");
                Console.WriteLine("3. Register a Pilot");
                Console.WriteLine("4. View All Flights");
                Console.WriteLine("5. Schedule a Flight");
                Console.WriteLine("6. Book a Flight");
                Console.WriteLine("7. Cancel a Booking");
                Console.WriteLine("8. Depart a Flight");
                Console.WriteLine("9. Cancel a Flight");
                Console.WriteLine("10. Passenger Booking History");
                Console.WriteLine("11. Flight Revenue and Load Factor Report");
                Console.WriteLine("12. Exit");
                Console.WriteLine("^--------------------------------------------^");
                Console.WriteLine("Choose one option :");



                int option = int.Parse(Console.ReadLine());

                switch (option)
                {

                    case 1:
                        RegisterPassenger(context.passengers);
                        break;

                    case 2:
                        AddAnAircraft(context.aircrafts);
                        break;

                    case 3:
                        RegisterPilot(context.pilots);
                        break;

                    case 4:
                        viewAllFlights(context.flights);
                        break;

                    case 5:
                        ScheduleAFlight(context);
                        break;

                    case 6:
                        BookAFlight(context);
                        break;

                    case 7:
                        CancelBooking(context);
                        break;

                    case 8:
                        DepartFlight(context.flights);
                        break;


                    case 9:
                        CancelFlight(context.flights);
                        break;

                    case 10:
                        PassengerBookingHistory(context);
                        break;

                    case 11:
                        FlightRevenueAndLoadFactorReport(context);
                        break;

                    case 12:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;

                }
            }
        }
    }
}

