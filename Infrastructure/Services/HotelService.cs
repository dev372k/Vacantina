﻿using Domain.Repositories.Services;
using MongoDB.Bson;
using Newtonsoft.Json;
using Shared.Commons;
using Shared.DTOs.Service.FlightDTOs;
using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;
using System.Net.Http.Headers;
using System.Net;
using System.Web.Mvc;
using Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.DTOs.Service;
using Shared.Helpers.Amadeus;

namespace Infrastructure.Services;

public class HotelService : IHotelService
{
    public async Task<GetHotelDTO> GetHotels(HotelFilter filter)
    {
        Appsettings appsettings = Appsettings.Instance;
        var token = AmadeusTokenHelper.Token().GetAwaiter().GetResult().AccessToken; // Assuming AccessToken is now an async property or method

        using (var client = new HttpClient())
        {
            var requestUri = $"{appsettings.GetValue("Amadeus:base_url")}/v1/reference-data/locations/hotels/by-city?" +
                             $"cityCode={filter.CityCode}" +
                             $"&radius={filter.Radius}" +
                             $"&radiusUnit={filter.RadiusUnit}" +
                             $"&amenities={filter.Amenities}" +
                             $"&ratings={filter.Ratings}" +
                             $"&hotelSource=ALL";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Accept", "application/vnd.amadeus+json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                //var errors = JsonConvert.DeserializeObject<AmadeusError>(responseString);
                throw new CustomException(HttpStatusCode.BadRequest, responseString);
            }

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<GetHotelDTO>(responseString) ?? new GetHotelDTO();
        }
    }

}
