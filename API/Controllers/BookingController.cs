using Domain.Repositories;
using Domain.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.CardDTOs;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(ICardRepo _cardRepo) : ControllerBase
{
    [HttpPost("create-customer")]
    public async Task<IActionResult> CreateCustomer(AddCustomerCardDTO request) =>
        Ok(await _cardRepo.InsertAsync(request).ToResponseAsync());
}

    