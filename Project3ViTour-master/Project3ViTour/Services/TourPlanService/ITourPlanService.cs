using Project3ViTour.Dtos.TourPlanDtos;

namespace Project3ViTour.Services.TourPlanService
{
    public interface ITourPlanService
    {
        Task<List<ResultTourPlanDto>> GetAllTourPlansAsync();
        Task<List<ResultTourPlanDto>> GetPlansByTourIdAsync(string tourId);
        Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto);
        Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto);
        Task DeleteTourPlanAsync(string id);
        Task<ResultTourPlanDto> GetTourPlanByIdAsync(string id);
    }
}