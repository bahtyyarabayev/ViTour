namespace Project3ViTour.Dtos.TourPlanDtos
{
    public class ResultTourPlanDto
    {
        public string TourPlanId { get; set; }
        public string TourId { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TourTitle { get; set; }
        public List<string> Items { get; set; }
    }
}
