using Domain.Repositories;
using Domain.Repositories.Services;
using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;

namespace Application.Implementations;

public class HotelRepo(IHotelService hotelService) : IHotelRepo
{
    public async Task<GetHotelDTO> GetHotels(HotelFilter filter)
    {
        var hotels = await hotelService.GetHotels(filter);
        return hotels;
    }
}
