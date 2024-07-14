using Shared.Commons;
using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Document;

public class User : BaseEntity
{
    public User(string name, string email, string phone, string address, DateTime dob, string passwordHash, enRole role) =>
        (Name, Email, Phone, Address, DoB, PasswordHash, Role) = (name, email, phone, address, dob, passwordHash, role);

    [BsonElement("name")]
    public string Name { get; }

    [BsonElement("email")]
    public string Email { get; }

    [BsonElement("phone")]
    public string Phone { get; }

    [BsonElement("address")]
    public string Address { get; }

    [BsonElement("dob")]
    public DateTime DoB { get; }

    [BsonElement("passwordHash")]
    public string PasswordHash { get; }

    [BsonElement("role")]
    public enRole Role { get; }
}
