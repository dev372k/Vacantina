using Application.Implementations.Base;
using Domain.Documents;
using Domain.Repositories;
using Domain.Repositories.Services;
using MongoDB.Driver;
using Shared;
using Shared.DTOs.BlogDTOs;
using Shared.DTOs.CardDTOs;

namespace Application.Implementations;

public class CardRepo : BaseRepo<UserCard>, ICardRepo
{
    private IStateHelper _stateHelper;
    private IPaymentGateway _paymentGateway;

    public CardRepo(
    IMongoClient mongoClient,
    IClientSessionHandle clientSessionHandle,
    IStateHelper stateHelper,
    IPaymentGateway paymentGateway
    ) : base(mongoClient, clientSessionHandle, "user_card")
    {
        _stateHelper = stateHelper;
        _paymentGateway = paymentGateway;
    }

    public async Task InsertAsync(AddCustomerCardDTO dto)
    {
        var customerResponse = await _paymentGateway.CreateCustomersync(dto.Email, dto.Name, dto.CardToken);
        var cardResponse = await _paymentGateway.GetCustomerCardAsync(customerResponse.Id, customerResponse.DefaultSourceId);

        await InsertAsync(new UserCard
        (
            dto.Name,
            dto.Email,
            cardResponse.Last4,
            cardResponse.ExpMonth,
            cardResponse.ExpYear,
            cardResponse.Brand,
            _stateHelper.User().Id,
            customerResponse.Id,
            customerResponse.DefaultSourceId
        ));
    }

    public async Task<UserCard> GetCardAsync(string id)
    {
        var filter = Builders<UserCard>.Filter.Eq(f => f.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserCard>> GetCardsAsync() =>
        await Collection.AsQueryable().ToListAsync();

    public async Task UpdateAsync(UpdateCardDTO dto)
    {
        var card = new UserCard
        (
            dto.Name,
            dto.Email,
            dto.Last4Digit,
            dto.Month,
            dto.Year,
            dto.Type,
            dto.UserId,
            dto.CustomerId,
            dto.CardId
        );
        card.SetId(dto.Id);
        await UpdateAsync(card);
    }
}
