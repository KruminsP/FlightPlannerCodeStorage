using System;
using System.Threading.Channels;
using FlightPlanner.Exceptions;

namespace FlightPlanner.Validators
{
    public static class FlightValidator
    {
        public static void Validator(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.ArrivalTime) ||
                string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.From.AirportCode) ||
                string.IsNullOrEmpty(flight.From.City) ||
                string.IsNullOrEmpty(flight.From.Country) ||
                string.IsNullOrEmpty(flight.To.AirportCode) ||
                string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.Country)
                )
            {
                throw new InvalidFlightException();               
            }
        }
    }
}
