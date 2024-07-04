using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Domain.Document.Base;

public abstract class BaseEntity
{
    [BsonElement("_id")]
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public virtual string Id { get; private set; }
    [BsonElement("createdAt")]
    public virtual DateTime CreatedAt { get; private set; } = DateTime.Now;
    [BsonElement("updatedAt")]
    public virtual DateTime UpdatedAt { get; private set; }

    public void SetId(string id) =>
        Id = id;

    public void SetCreatedAt(DateTime createdAt) =>
        CreatedAt = createdAt;
    
    public void SetUpdatedAt(DateTime updatedAt) =>
        UpdatedAt = updatedAt;
}
