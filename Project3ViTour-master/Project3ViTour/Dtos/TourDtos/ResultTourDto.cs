namespace Project3ViTour.Dtos.TourDtos
{
    public class ResultTourDto
    {
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string Badge { get; set; }
        public int DayCount { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool IsStatus { get; set; }
        public string Location { get; set; }
        public int ReviewCount { get; set; } // yorum sayısı
        public double Score { get; set; }    // ortalama puan
    }
}
