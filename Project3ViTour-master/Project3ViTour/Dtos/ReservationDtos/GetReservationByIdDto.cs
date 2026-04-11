namespace Project3ViTour.Dtos.ReservationDtos
{
    public class GetReservationByIdDto
    {
        public string ReservationId { get; set; }
        public string TourId { get; set; }
        public string TourTitle { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ChildCount { get; set; }
        public int YouthCount { get; set; }
        public int AdultCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime TourDate { get; set; }
        public bool Status { get; set; }
    }
}
