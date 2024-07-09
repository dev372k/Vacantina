using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.CardDTOs
{
    public class UpdateCardDTO
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Last4Digit { get; set; } = String.Empty;
        public long Month { get; set; }
        public long Year { get; set; }
        public string Type { get; set; } = String.Empty;
        public string UserId { get; set; } = String.Empty;
        public string CustomerId { get; set; } = String.Empty;
        public string CardId { get; set; } = String.Empty;
    }
}
