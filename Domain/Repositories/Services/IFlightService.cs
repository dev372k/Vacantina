using Shared.DTOs.Service.FlightDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Services;

public interface IFlightService
{
    Task<object> GetFlights(FlightFilter filter);
    Task Session();
}
