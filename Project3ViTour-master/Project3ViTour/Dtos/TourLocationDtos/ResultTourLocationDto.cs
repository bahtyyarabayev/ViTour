namespace Project3ViTour.Dtos.TourLocationDtos
{
    public class ResultTourLocationDto
    {
        public string TourLocationId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
        public string TourTitle { get; set; }
        public List<string> Items { get; set; }
    }
}
