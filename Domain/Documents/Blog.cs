using DL.Commons;
using Domain.Document.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Documents;

public class Blog : BaseEntity
{
    public Blog(string title, string content, string imageURL, List<string> tags) =>
        (Title, Content, ImageURL, Tags) = (title, content, imageURL, tags);

    [BsonElement("title")]
    public string Title { get; }

    [BsonElement("content")]
    public string Content { get; }

    [BsonElement("imageURL")]
    public string ImageURL { get; }

    [BsonElement("tags")]
    public List<string> Tags { get; }
}
