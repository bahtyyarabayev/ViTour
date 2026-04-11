using AutoMapper;
using MongoDB.Driver;
using Project3ViTour.Dtos.CategoryDtos;
using Project3ViTour.Dtos.TourDtos;
using Project3ViTour.Dtos.TourImageDtos;
using Project3ViTour.Entities;
using Project3ViTour.Settings;
namespace Project3ViTour.Services.GalleryImageService
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<TourImage> _galleryImageCollection;

        public GalleryImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _galleryImageCollection = database.GetCollection<TourImage>(_databaseSettings.GalleryImageCollectionName);
        }
        public async Task CreateGalleryImageAsync(CreateGalleryImageDto createGalleryImageDto)
        {
            var value = _mapper.Map<TourImage>(createGalleryImageDto);
            await _galleryImageCollection.InsertOneAsync(value);
        }

        public async Task DeleteGalleryImageAsync(string id)
        {
            await _galleryImageCollection.DeleteOneAsync(x => x.GalleryImageId == id);
        }

        public async Task<List<ResultGalleryImageDto>> GetAllGalleryImageAsync()
        {
            var values = await _galleryImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultGalleryImageDto>>(values);
        }

        public async Task<GetGalleryImageByIdDto> GetGalleryImageByIdAsync(string id)
        {
            var value = await _galleryImageCollection.Find(x => x.GalleryImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetGalleryImageByIdDto>(value);
        }

        public async Task UpdateGalleryImageAsync(UpdateGalleryImageDto updateGalleryImageDto)
        {
            var value = _mapper.Map<TourImage>(updateGalleryImageDto);
            await _galleryImageCollection.FindOneAndReplaceAsync(x => x.GalleryImageId == updateGalleryImageDto.GalleryImageId, value);
        }
        public async Task<List<ResultGalleryImageDto>> GetGalleryImagesByTourIdAsync(string tourId)
        {
            // MongoDB'de TourId alanına göre filtreleme yapıyoruz
            var values = await _galleryImageCollection.Find(x => x.TourId == tourId).ToListAsync();
            return _mapper.Map<List<ResultGalleryImageDto>>(values);
        }
    }
}
