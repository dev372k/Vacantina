﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Domain.Document.Base;

public abstract class BaseEntity
{
    [BsonElement("_id")]
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public virtual string Id { get; private set; }

    public void SetId(string id) =>
        Id = id;
}
