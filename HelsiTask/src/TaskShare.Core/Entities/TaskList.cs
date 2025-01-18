using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskShare.Core.Entities;

public class TaskList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Name")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "The Name field must be between 1 and 255 characters.")]
    public string Name { get; set; } = null!;

    [BsonElement("OwnerId")]
    public int OwnerId { get; set; }

    [BsonElement("SharedWithUserIds")]
    public List<int> SharedWithUserIds { get; set; } = [];

    [BsonElement("CreationDate")]
    public DateTime CreationDate { get; set; }
}