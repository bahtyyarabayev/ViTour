using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Project3ViTour.Entities
{
    public class TourImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GalleryImageId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string TourId { get; set; }
    }
}
