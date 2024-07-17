using Domain.Repositories;
using Domain.Repositories.Services;
using Shared.Commons;
using Shared.DTOs.Service.HotelDTOs;
using Shared.DTOs.Service.HotelService;
using System.Web.Mvc;

namespace Application.Implementations;

public class HotelRepo(IHotelService hotelService) : IHotelRepo
{
    public SelectList GetAmenities()
    {
        var values = Enum.GetValues(typeof(enHotelAmenities))
                         .Cast<enHotelAmenities>()
                         .Select(e => new SelectListItem
                         {
                             Value = e.ToString(),
                             Text = e.ToString().Replace("_", " ")
                         })
                         .ToList();

        return new SelectList(values, "Value", "Text");
    }

    public SelectList GetBoardType()
    {
        var values = Enum.GetValues(typeof(enHotelBoardType))
                         .Cast<enHotelBoardType>()
                         .Select(e => new SelectListItem
                         {
                             Value = e.ToString(),
                             Text = e.ToString().Replace("_", " ")
                         })
                         .ToList();

        return new SelectList(values, "Value", "Text");
    }

    public async Task<GetHotelDTO> GetHotels(HotelFilter filter)
    {
        var hotels = await hotelService.GetHotels(filter);
        return hotels;
    }
}
