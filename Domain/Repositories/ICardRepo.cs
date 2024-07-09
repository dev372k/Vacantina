using Domain.Document;
using Domain.Documents;
using Domain.Repositories.Base;
using Shared.DTOs.BlogDTOs;
using Shared.DTOs.CardDTOs;

namespace Domain.Repositories;

public interface ICardRepo : IBaseRepo<UserCard>
{
    Task InsertAsync(AddCustomerCardDTO dto);
    //Task InsertAsync(AddCardDTO dto);
    Task UpdateAsync(UpdateCardDTO dto);
    Task<IEnumerable<UserCard>> GetCardsAsync();
    Task<UserCard> GetCardAsync(string id);
}
