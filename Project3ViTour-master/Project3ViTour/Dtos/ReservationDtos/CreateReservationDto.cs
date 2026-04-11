namespace Project3ViTour.Dtos.ReservationDtos
{
    public class CreateReservationDto
    {
        public string TourId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ChildCount { get; set; }
        public int YouthCount { get; set; }
        public int AdultCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TourDate { get; set; }
    }
}
