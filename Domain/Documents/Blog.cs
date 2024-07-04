using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Documents;

public class Blog : BaseEntity
{
    public Blog(string title, string content, string imageURL,bool isFeatured, List<string> tags) =>
        (Title, Content, ImageURL, IsFeatured, Tags) = (title, content, imageURL, isFeatured, tags);

    [BsonElement("title")]
    public string Title { get; }

    [BsonElement("content")]
    public string Content { get; }

    [BsonElement("imageURL")]
    public string ImageURL { get; }

    [BsonElement("isFeatured")]
    public bool IsFeatured { get; }

    [BsonElement("tags")]
    public List<string> Tags { get; }
}
