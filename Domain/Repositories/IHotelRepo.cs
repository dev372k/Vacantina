using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;

namespace Domain.Repositories;

public interface IHotelRepo
{
    Task<GetHotelDTO> GetHotels(HotelFilter filter);
}
