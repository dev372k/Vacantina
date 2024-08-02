using Domain.Repositories.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFlightService _flightService;

    public FlightController(IHttpContextAccessor httpContextAccessor, IFlightService flightService)
    {
        _httpContextAccessor = httpContextAccessor;
        _flightService = flightService;
    }

    [HttpGet("Session")]
    public async Task<IActionResult> Session()
    {
        var response = await _flightService.Session().ToResponseAsync();
        return Ok(response);
    }

    [HttpGet("Success")]
    public IActionResult Success()
    {
        var check = _httpContextAccessor.HttpContext;
        // Do something with 'check' if needed
        return Ok();
    }

    [HttpGet("Failure")]
    public IActionResult Failure()
    {
        // Implement functionality if needed
        return Ok();
    }

    [HttpGet("Abandonment")]
    public IActionResult Abandonment()
    {
        // Implement functionality if needed
        return Ok();
    }
}
