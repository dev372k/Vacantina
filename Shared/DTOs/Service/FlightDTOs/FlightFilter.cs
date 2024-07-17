namespace Shared.DTOs.Service.FlightDTOs;

public class FlightFilter
{
    public string OriginLocationCode { get; set; }
    public string DestinationLocationCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable to handle one-way trips
    public int Adults { get; set; }
    public int Children { get; set; }
    public int Infants { get; set; }
    public string TravelClass { get; set; }
    public string IncludedAirlineCodes { get; set; }
    public string ExcludedAirlineCodes { get; set; }
    public bool NonStop { get; set; }
    public string CurrencyCode { get; set; }
    public decimal MaxPrice { get; set; }
    public int MaxResults { get; set; }
}

