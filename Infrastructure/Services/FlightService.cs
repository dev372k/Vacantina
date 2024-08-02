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
    public async Task Session()
    {
        Appsettings appsettings = Appsettings.Instance;
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, appsettings.GetValue("Duffels:SessionAPI"));
        request.Headers.Add("Duffel-Version", "v1");
        request.Headers.Add("Authorization", $"Bearer {appsettings.GetValue("Duffels:APIKey")}");
        var content = new StringContent("{\r\n    \r\n    \"data\": {\r\n        \"reference\": \"USER_1\",\r\n        \"success_url\": \"https://vacatina.vercel.app/\",\r\n        \"failure_url\": \"https://vacatina.vercel.app/\",\r\n        \"abandonment_url\": \"https://vacatina.vercel.app/\",\r\n        \"logo_url\": \"https://dashboard.zakhaer.com/download.png\",\r\n        \"checkout_display_text\": \"Checkout\",\r\n        \"primary_color\": \"#3498DB\",\r\n        \"secondary_color\": \"#3498DB\",\r\n        \"traveller_currency\": \"USD\",\r\n        \"markup_amount\": \"1.00\",\r\n        \"markup_currency\": \"USD\",\r\n        \"markup_rate\": \"0.01\",\r\n        \"flights\": {\r\n            \"enabled\": \"true\"\r\n        },\r\n        \"stays\": {\r\n            \"enabled\": \"false\"\r\n        }\r\n    }\r\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
    }
}
