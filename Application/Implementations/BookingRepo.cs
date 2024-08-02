using Domain.Document;
using Domain.Repositories.Services;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations;

public class BookingRepo
{
    private IFlightService _flightService;
    public IStateHelper _stateHelper;

    public BookingRepo(IFlightService flightService, IStateHelper stateHelper)
    {
        _flightService = flightService;
        _stateHelper = stateHelper;
    }

    public async Task<string> Flight()
    {
        return await _flightService.Session(_stateHelper.User().Id);
    }
}
