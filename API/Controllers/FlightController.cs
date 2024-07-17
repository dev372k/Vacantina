using Domain.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.CardDTOs;
using Shared.Extensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController(IFlightService _flightService) : ControllerBase
    {
        //[HttpGet("token")]
        //public async Task<IActionResult> Token() =>
        //         Ok(await _flightService.Token().ToResponseAsync());
    }
}
