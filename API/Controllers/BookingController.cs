using Domain.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(IPaymentGateway _paymentGateway) : ControllerBase
{
    [HttpPost("create-customer")]
    public async Task<IActionResult> CreateCustomer(AddCustomerDTO request) =>
        Ok(await _paymentGateway.CreateCustomersync(request.Email, request.Name, request.CardToken).ToResponse());
}

public class AddCustomerDTO
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string CardToken { get; set; }
}
