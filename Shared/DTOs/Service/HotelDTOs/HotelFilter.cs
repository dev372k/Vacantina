namespace Shared.DTOs.Service.HotelService;

public class HotelFilter
{
    public string CityCode { get; set; }
    public double Radius { get; set; }
    public string RadiusUnit { get; set; } = "KM";
    public string Amenities { get; set; }
    public string Ratings { get; set; }
    public string HotelSource { get; set; }
}
