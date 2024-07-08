using Domain.Repositories.Services;
using Stripe;

namespace Infrastructure.Services;

public class PaymentGateway : IPaymentGateway
{
    public PaymentGateway()
    {
        StripeConfiguration.ApiKey = Appsettings.Instance.GetValue("Stripe:SecretKey");
    }

    public Charge CreateCharge(string token, decimal amount, string currency = "usd")
    {
        var options = new ChargeCreateOptions
        {
            Amount = (int)(amount * 100),
            Currency = currency,
            Source = token,
            Description = "Test Charge"
        };

        return new ChargeService().Create(options);
    }

    public Customer CreateCustomer(string email, string cardToken, string name = "")
    {
        var options = new CustomerCreateOptions
        {
            Email = email,
            Name = name,
            Source = cardToken,
        };

        return new CustomerService().Create(options);
    }
}
