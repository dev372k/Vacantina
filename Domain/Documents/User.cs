using Shared.Commons;
using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Document;

public class User : BaseEntity
{
    public User(string name, string email,string passwordHash, enRole role) =>
        (Name, Email, PasswordHash, Role) = (name, email, passwordHash, role);

    [BsonElement("name")]
    public string Name { get; }

    [BsonElement("email")]
    public string Email { get; }
    
    [BsonElement("passwordHash")]
    public string PasswordHash { get; }
    
    [BsonElement("role")]
    public enRole Role { get; }
}
