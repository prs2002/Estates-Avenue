using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DotNetBackend.Models
{
    public class Executive
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("password")]
        public required string Password { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("number")]
        public required string Number { get; set; }

        [BsonElement("location")]
        public required string Location { get; set; }

        [BsonElement("userType")]
        public required string UserType { get; set; } //Manager or Executive
    }
}
