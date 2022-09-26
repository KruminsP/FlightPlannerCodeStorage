using System;
using System.Globalization;

namespace FlightPlanner.Validators
{
    public static class InvalidTimeValidator
    {
        public static bool Validator(Flight flight)
        {
            var departure = DateTime.ParseExact(
                flight.DepartureTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var arrival = DateTime.ParseExact(
                flight.ArrivalTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            return departure >= arrival;
        }
    }
}
