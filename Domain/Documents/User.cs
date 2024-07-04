using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Document;

public class User : BaseEntity
{
    public User(string name, string email,string passwordHash) =>
        (Name, Email, PasswordHash) = (name, email, passwordHash);

    [BsonElement("name")]
    public string Name { get; }

    [BsonElement("email")]
    public string Email { get; }
    
    [BsonElement("passwordHash")]
    public string PasswordHash { get; }
}
