using Domain.Document;
using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Documents;

public class UserCard : BaseEntity
{
    public UserCard(string name, string email, string last4Digit, long month, long year, string type, string userId, string customerId, string cardId) =>
        (Name, Email, Last4Digit, Month, Year, Type, UserId, CustomerId, CardId) = (name, email, last4Digit, month, year, type, userId, customerId, cardId);

    [BsonElement("name")]
    public string Name { get; }

    [BsonElement("email")]
    public string Email { get; }

    [BsonElement("last4Digit")]
    public string Last4Digit { get; }

    [BsonElement("month")]
    public long Month { get; }

    [BsonElement("year")]
    public long Year { get; }   
    
    [BsonElement("type")]
    public string Type { get; }    
    
    [BsonElement("userId")]
    public string UserId { get; }
    
    [BsonElement("customerId")]
    public string CustomerId { get; }
    
    [BsonElement("cardId")]
    public string CardId { get; }
}
