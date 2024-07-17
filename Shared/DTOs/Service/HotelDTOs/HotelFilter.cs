namespace Shared.DTOs.Service.HotelService;

public class HotelFilter
{
    public string CityCode { get; set; } = String.Empty;
    public double Radius { get; set; }
    public string RadiusUnit { get; set; } = "KM";
    public string Amenities { get; set; } = String.Empty;
    public string Ratings { get; set; } = String.Empty;
    //public string HotelSource { get; set; } = "ALL";
}
