namespace Project3ViTour.Dtos.TourLocationDtos
{
    public class CreateTourLocationDto
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
        public List<string> Items { get; set; }
    }
}
