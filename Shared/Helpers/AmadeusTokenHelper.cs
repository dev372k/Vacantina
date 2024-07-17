using Newtonsoft.Json;
using Shared.DTOs.Service.FlightDTOs;
using System.Net.Http.Headers;

namespace Shared.Helpers;

public class AmadeusTokenHelper
{
    public static async Task<GetTokenDTO> Token()
    {
        var client = new HttpClient();

        Appsettings _appsettings = Appsettings.Instance;
        var client_id = _appsettings.GetValue("Amadeus:client_id");
        var client_secret = _appsettings.GetValue("Amadeus:client_secret");
        var grant_type = _appsettings.GetValue("Amadeus:grant_type");

        var requestBody = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("client_id", client_id),
        new KeyValuePair<string, string>("client_secret", client_secret),
        new KeyValuePair<string, string>("grant_type", grant_type)
    });

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("zstd"));
        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.9));
        client.DefaultRequestHeaders.Connection.Add("keep-alive");
        client.DefaultRequestHeaders.Add("Origin", "https://developers.amadeus.com");
        client.DefaultRequestHeaders.Add("Referer", "https://developers.amadeus.com/");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
        client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");

        var response = await client.PostAsync("https://test.api.amadeus.com/v1/security/oauth2/token", requestBody);

        var responseString = await response.Content.ReadAsStringAsync();


        return JsonConvert.DeserializeObject<GetTokenDTO>(responseString) ?? new GetTokenDTO();
    }
}
