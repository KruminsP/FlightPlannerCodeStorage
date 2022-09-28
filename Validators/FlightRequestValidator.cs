namespace FlightPlanner.Validators
{
    public static class FlightRequestValidator
    {
        public static bool IsRequestInvalid(SearchFlightsRequest search)
        {
            return string.IsNullOrEmpty(search.DepartureDate) ||
                   string.IsNullOrEmpty(search.From) ||
                   string.IsNullOrEmpty(search.To);
        }

        public static bool IsFromAndToAirportTheSame(SearchFlightsRequest search)
        {
            return search.From == search.To;
        }
    }
}
