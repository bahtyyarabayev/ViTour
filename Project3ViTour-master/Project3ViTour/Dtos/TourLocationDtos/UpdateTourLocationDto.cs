namespace Project3ViTour.Dtos.TourLocationDtos
{
    public class UpdateTourLocationDto
    {
        public string TourLocationId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
        public List<string> Items { get; set; }
    }
}
