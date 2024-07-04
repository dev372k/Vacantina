using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Documents;

public class Contact : BaseEntity
{
    public Contact(string name, string email, string message) =>
        (Name, Email, Message) = (name, email, message);

    [BsonElement("name")]
    public string Name { get; }

    [BsonElement("email")]
    public string Email { get; }

    [BsonElement("message")]
    public string Message { get; }

}