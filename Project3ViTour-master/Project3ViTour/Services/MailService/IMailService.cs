namespace Project3ViTour.Services.MailService
{
    public interface IMailService
    {
        Task SendReservationMailAsync(string toEmail, string nameSurname, string tourTitle, DateTime tourDate, int childCount, int youthCount, int adultCount, decimal totalPrice);
    }
}
