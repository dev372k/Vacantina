namespace Shared.DTOs.Service.HotelDTOs;

public class GeoCode
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class Address
{
    public string CountryCode { get; set; }
}

public class Distance
{
    public double Value { get; set; }
    public string Unit { get; set; }
}

public class Hotel
{
    public string ChainCode { get; set; }
    public string IataCode { get; set; }
    public long DupeId { get; set; }
    public string Name { get; set; }
    public string HotelId { get; set; }
    public GeoCode GeoCode { get; set; }
    public Address Address { get; set; }
    public Distance Distance { get; set; }
    public List<string> Amenities { get; set; }
    public int Rating { get; set; }
    public DateTime LastUpdate { get; set; }
}

public class MetaLinks
{
    public string Self { get; set; }
}

public class Meta
{
    public int Count { get; set; }
    public MetaLinks Links { get; set; }
}

public class GetHotelDTO
{
    public List<Hotel> Data { get; set; }
    public Meta Meta { get; set; }
}

