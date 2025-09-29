using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SuperSpammer.Common;

public class SenderDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }  = string.Empty;
}