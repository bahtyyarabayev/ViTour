using Project3ViTour.Dtos.TourLocationDtos;

namespace Project3ViTour.Services.TourLocationService
{
    public interface ITourLocationService
    {
        Task<List<ResultTourLocationDto>> GetAllAsync();
        Task<GetTourLocationByIdDto> GetByIdAsync(string id);
        Task<GetTourLocationByIdDto> GetByTourIdAsync(string tourId);
        Task CreateAsync(CreateTourLocationDto dto);
        Task UpdateAsync(UpdateTourLocationDto dto);
        Task DeleteAsync(string id);
    }
}