using Project3ViTour.Dtos.ReservationDtos;

namespace Project3ViTour.Services.ReservationService
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservationsAsync();
        Task<GetReservationByIdDto> GetReservationByIdAsync(string id);
        Task CreateReservationAsync(CreateReservationDto dto);
        Task DeleteReservationAsync(string id);
        Task UpdateReservationAsync(UpdateReservationDto dto);
    }
}
