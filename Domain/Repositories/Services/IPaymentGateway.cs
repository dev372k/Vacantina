using Stripe;

namespace Domain.Repositories.Services;

public interface IPaymentGateway
{
    Charge CreateCharge(string token, decimal amount, string currency = "usd");
    Customer CreateCustomer(string email, string cardToken, string name = "");
}
