using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.CardDTOs;

public class AddCustomerCardDTO
{
    public string Email { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string CardToken { get; set; } = String.Empty;
}
