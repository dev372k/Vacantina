using Domain.Repositories.Services;
using MongoDB.Bson;
using Newtonsoft.Json;
using Shared.Commons;
using Shared.DTOs.Service.FlightDTOs;
using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;
using Shared.Helpers;
using System.Web.Mvc;

namespace Infrastructure.Services;

public class HotelService : IHotelService
{
    public SelectList GetAmenities()
    {
        var values = Enum.GetValues(typeof(enHotelAmenities))
                         .Cast<enHotelAmenities>()
                         .Select(e => new SelectListItem
                         {
                             Value = e.ToString(),
                             Text = e.ToString().Replace("_", " ") // Optional: Replace underscores with spaces for display
                         })
                         .ToList();

        return new SelectList(values, "Value", "Text");
    }
    
    public SelectList GetBoardType()
    {
        var values = Enum.GetValues(typeof(enHotelBoardType))
                         .Cast<enHotelBoardType>()
                         .Select(e => new SelectListItem
                         {
                             Value = e.ToString(),
                             Text = e.ToString().Replace("_", " ") // Optional: Replace underscores with spaces for display
                         })
                         .ToList();

        return new SelectList(values, "Value", "Text");
    }

    public async Task<GetHotelDTO> GetHotels(HotelFilter filter)
    {
        Appsettings appsettings = Appsettings.Instance;
        var token = AmadeusTokenHelper.Token().GetAwaiter().GetResult().AccessToken;
        var client = new HttpClient();
        var requestUri = $"{appsettings.GetValue("Amadeus:base_url")}/v1/reference-data/locations/hotels/by-city?" +
                         $"cityCode={filter.CityCode}" +
                         $"&radius={filter.Radius}" +
                         $"&radiusUnit={filter.RadiusUnit}" +
                         $"&amenities={filter.Amenities}" +
                         $"&ratings={filter.Ratings}" +
                         $"&hotelSource={filter.HotelSource}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("Accept", "application/vnd.amadeus+json");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GetHotelDTO>(responseString) ?? new GetHotelDTO();
    }

}
