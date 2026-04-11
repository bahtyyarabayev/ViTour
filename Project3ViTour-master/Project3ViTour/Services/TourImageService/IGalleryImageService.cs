using Project3ViTour.Dtos.TourImageDtos;

namespace Project3ViTour.Services.GalleryImageService
{
    public interface IGalleryImageService
    {
        Task<List<ResultGalleryImageDto>> GetAllGalleryImageAsync();
        Task CreateGalleryImageAsync(CreateGalleryImageDto createGalleryImageDto);
        Task UpdateGalleryImageAsync(UpdateGalleryImageDto updateGalleryImageDto);
        Task DeleteGalleryImageAsync(string id);
        Task<GetGalleryImageByIdDto> GetGalleryImageByIdAsync(string id);
        Task<List<ResultGalleryImageDto>> GetGalleryImagesByTourIdAsync(string tourId);
    }
}
