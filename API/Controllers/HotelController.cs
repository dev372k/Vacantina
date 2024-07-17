using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Service.HotelService;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IHotelRepo _hotelRepo) : ControllerBase
{

    //[HttpGet("amentities")]
    //public async Task<IActionResult> GetAmentities() =>
    //    Ok(await _hotelRepo.GetHotels(request).ToResponseAsync());
    [HttpPost]
    public async Task<IActionResult> GetHotels(HotelFilter request) =>
            Ok(await _hotelRepo.GetHotels(request).ToResponseAsync());
}
