using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;
using System.Web.Mvc;

namespace Domain.Repositories.Services;

public interface IHotelService
{
    SelectList GetAmenities();
    SelectList GetBoardType();
    Task<GetHotelDTO> GetHotels(HotelFilter filter);
}
