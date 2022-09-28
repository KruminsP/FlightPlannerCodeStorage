using FlightPlanner.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var returnAirports = FlightStorage.SearchAirports(search);
            return returnAirports.Count < 1 ? NotFound() : Ok(returnAirports);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            if (FlightRequestValidator.IsRequestInvalid(req) ||
                FlightRequestValidator.IsFromAndToAirportTheSame(req))
            {
                return BadRequest();
            }

            var pageResult = new PageResult(FlightStorage.GetRequestedFlights(req));
            return Ok(pageResult);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            return flight == null ? NotFound($"No flight with id {id}") : Ok(flight);
        }
    }
}
