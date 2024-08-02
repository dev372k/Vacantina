using Domain.Repositories.Services;
using Shared.DTOs.Service.FlightDTOs;
using Shared.Helpers;

namespace Infrastructure.Services;

public class FlightService : IFlightService
{
    public async Task<object> GetFlights(FlightFilter filter)
    {
        Appsettings appsettings = Appsettings.Instance;
        var token = AmadeusTokenHelper.Token().GetAwaiter().GetResult().AccessToken;
        var client = new HttpClient();
        var requestUri = $"{appsettings.GetValue("Amadeus:base_url")}/v2/shopping/flight-offers?" +
                         $"originLocationCode={filter.OriginLocationCode}" +
                         $"&destinationLocationCode={filter.DestinationLocationCode}" +
                         $"&departureDate={filter.DepartureDate:yyyy-MM-dd}" +
                         $"&returnDate={(filter.ReturnDate.HasValue ? filter.ReturnDate.Value.ToString("yyyy-MM-dd") : string.Empty)}" +
                         $"&adults={filter.Adults}" +
                         $"&children={filter.Children}" +
                         $"&infants={filter.Infants}" +
                         $"&travelClass={filter.TravelClass}" +
                         $"&includedAirlineCodes={filter.IncludedAirlineCodes}" +
                         $"&excludedAirlineCodes={filter.ExcludedAirlineCodes}" +
                         $"&nonStop={filter.NonStop.ToString().ToLower()}" +
                         $"&currencyCode={filter.CurrencyCode}" +
                         $"&maxPrice={filter.MaxPrice}" +
                         $"&max={filter.MaxResults}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("Accept", "application/vnd.amadeus+json");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Duffel
    public async Task<string> Session(string UserId)
    {
        Appsettings appsettings = Appsettings.Instance;
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, appsettings.GetValue("Duffels:SessionAPI"));
        request.Headers.Add("Duffel-Version", "v1");
        request.Headers.Add("Authorization", $"Bearer {appsettings.GetValue("Duffels:APIKey")}");

        var data = new
        {
            data = new
            {
                reference = UserId,
                success_url = $"{appsettings.GetValue("Duffels:FallbackURL")}/success",
                failure_url = $"{appsettings.GetValue("Duffels:FallbackURL")}/failure",
                abandonment_url = $"{appsettings.GetValue("Duffels:FallbackURL")}/abandonment",
                logo_url = "https://dashboard.zakhaer.com/download.png",
                checkout_display_text = "Checkout",
                primary_color = "#fff",
                secondary_color = "#3498DB",
                traveller_currency = "USD",
                markup_amount = "1.00",
                markup_currency = "USD",
                markup_rate = "0.01",
                flights = new
                {
                    enabled = "true"
                },
                stays = new
                {
                    enabled = "false"
                }
            }
        };

        var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), null, "application/json");
        request.Content = content;

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
