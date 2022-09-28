using System;
using FlightPlanner.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _lock = new object();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            Flight flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_lock)
            {
                try
                {
                    FlightValidator.Validator(flight);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }

                if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim() ||
                    TimeValidator.Validator(flight))
                {
                    return BadRequest();
                }

                if (FlightStorage.FlightExists(flight))
                {
                    return Conflict("Flight already exists");
                }

                flight = FlightStorage.AddFlight(flight);
            }

            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                FlightStorage.DeleteFlight(id);
            }

            return Ok("Deleted");
        }
    }
}
