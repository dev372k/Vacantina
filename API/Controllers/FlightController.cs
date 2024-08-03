using Application.Implementations;
using Domain.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BookingRepo _bookingRepo;

    public FlightController(IHttpContextAccessor httpContextAccessor, BookingRepo bookingRepo)
    {
        _httpContextAccessor = httpContextAccessor;
        _bookingRepo = bookingRepo;
    }

    [HttpGet("Session"), Authorize]
    public async Task<IActionResult> Session()
    {
        return Ok(await _bookingRepo.Flight().ToResponseAsync());
    }

    [HttpGet("Success")]
    public IActionResult Success()
    {
        var reference = _httpContextAccessor.HttpContext.Request.Query["reference"];
        var orderId = _httpContextAccessor.HttpContext.Request.Query["order_id"];

        return Redirect(Appsettings.Instance.GetValue("Frontend:Page:Success"));
    }

    [HttpGet("Failure")]
    public IActionResult Failure()
    {
        return Ok(Appsettings.Instance.GetValue("Frontend:Page:Failure"));
    }

    [HttpGet("Abandonment")]
    public IActionResult Abandonment()
    {
        return Ok(Appsettings.Instance.GetValue("Frontend:Page:Home"));
    }
}
