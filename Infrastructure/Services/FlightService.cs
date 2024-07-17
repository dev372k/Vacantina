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
}
