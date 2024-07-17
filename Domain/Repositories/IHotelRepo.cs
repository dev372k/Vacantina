using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;
using System.Web.Mvc;

namespace Domain.Repositories;

public interface IHotelRepo
{
    SelectList GetAmenities();
    SelectList GetBoardType();
    Task<GetHotelDTO> GetHotels(HotelFilter filter);
}
