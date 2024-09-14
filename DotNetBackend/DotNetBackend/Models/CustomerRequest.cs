using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class CustomerRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Rid { get; set; }

    [BsonElement("customer_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CustomerId { get; set; }

    [BsonElement("property_id")]
    public string? PropertyId { get; set; }  // This will be the SQL Property ID as a string

    [BsonElement("executive_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExecutiveId { get; set; }

    [BsonElement("locality")]
    public string? Locality { get; set; }  // Locality where the customer is searching for properties

    [BsonElement("request_status")]
    public required string RequestStatus { get; set; }  // e.g., "pending", "fulfilled"

}
