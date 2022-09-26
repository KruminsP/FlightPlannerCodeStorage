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
