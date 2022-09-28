using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = ++_id;
            _flights.Add(flight);
            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(first => first.Id == id);
        }

        public static List<Flight> GetAllFlights(SearchFlightsRequest req)
        {
            return _flights.Where(flight =>
                flight.From.AirportCode == req.From &&
                flight.To.AirportCode == req.To &&
                DateTime.Parse(flight.DepartureTime).Date == DateTime.Parse(req.DepartureDate).Date).ToList();
        }

        public static HashSet<Airport> SearchAirports(string input)
        {
            var returnList = new HashSet<Airport>();
            input = input.Trim().ToLower();

            foreach (var flight in _flights)
            {
                if (flight.From.AirportCode.ToLower().Contains(input) ||
                    flight.From.City.ToLower().Contains(input) ||
                    flight.From.Country.ToLower().Contains(input))
                {
                    returnList.Add(flight.From);
                }

                if (flight.To.AirportCode.ToLower().Contains(input) ||
                    flight.To.City.ToLower().Contains(input) ||
                    flight.To.Country.ToLower().Contains(input))
                {
                    returnList.Add(flight.To);
                }
            }
            return returnList;
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static void DeleteFlight(int id)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == id);
            _flights.Remove(flight);
        }

        public static bool FlightExists(Flight flight)
        {
            foreach (var storedFlight in _flights)
            {
                if (flight.ArrivalTime == storedFlight.ArrivalTime &&
                    flight.DepartureTime == storedFlight.DepartureTime &&
                    flight.Carrier == storedFlight.Carrier &&
                    flight.From.AirportCode == storedFlight.From.AirportCode &&
                    flight.To.AirportCode == storedFlight.To.AirportCode)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
