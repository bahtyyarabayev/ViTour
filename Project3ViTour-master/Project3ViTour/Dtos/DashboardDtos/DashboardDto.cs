using Project3ViTour.Dtos.ReservationDtos;
using Project3ViTour.Dtos.ReviewDtos;

namespace Project3ViTour.Dtos.DashboardDtos
{
    public class DashboardDto
    {
        public int TourCount { get; set; }
        public int ReservationCount { get; set; }
        public int ReviewCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ResultReservationDto> LastReservations { get; set; }
        public List<ResultReviewDto> LastReviews { get; set; }
        public List<TopTourDto> TopTours { get; set; }
    }
}
