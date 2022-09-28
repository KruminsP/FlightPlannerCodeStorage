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
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
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
            try
            {
                FlightValidator.Validator(flight);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return BadRequest("Airport of departure and arrival cannot be the same");
            }

            if (TimeValidator.Validator(flight))
            {
                return BadRequest("Invalid arrival or departure times");
            }

            if (FlightStorage.FlightExists(flight))
            {
                return Conflict("Flight already exists");
            }

            flight = FlightStorage.AddFlight(flight);
            return Created("",flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok("Deleted");
        }
    }
}
