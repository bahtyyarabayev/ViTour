using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Project3ViTour.Entities
{
    public class TourLocation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourLocationId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
        public List<string> Items { get; set; }
    }
}
