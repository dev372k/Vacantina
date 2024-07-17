using Shared.DTOs.Service;
using Shared.DTOs.Service.FlightDTOs;

namespace Shared.Helpers.Amadeus;

public class AmadeusErrorHelper
{
    public static string Error(AmadeusError error)
    {
        string errors = String.Empty;
        foreach (var errorItem in error.Errors)
        {
            errors = $"{errorItem.Code}:{errorItem.Detail} ";
        }

        return errors.Trim();
    }
}
