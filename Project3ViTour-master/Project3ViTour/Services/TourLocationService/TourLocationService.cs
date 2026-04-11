using AutoMapper;
using MongoDB.Driver;
using Project3ViTour.Dtos.TourLocationDtos;
using Project3ViTour.Entities;
using Project3ViTour.Settings;

namespace Project3ViTour.Services.TourLocationService
{
    public class TourLocationService : ITourLocationService
    {
        private readonly IMongoCollection<TourLocation> _collection;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;

        public TourLocationService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TourLocation>(settings.TourLocationCollectionName);
            _tourCollection = database.GetCollection<Tour>(settings.TourCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultTourLocationDto>> GetAllAsync()
        {
            var locations = await _collection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultTourLocationDto>>(locations);

            foreach (var item in result)
            {
                var tour = await _tourCollection.Find(x => x.TourId == item.TourId).FirstOrDefaultAsync();
                item.TourTitle = tour?.Title;
            }

            return result;
        }

        public async Task<GetTourLocationByIdDto> GetByIdAsync(string id)
        {
            var value = await _collection.Find(x => x.TourLocationId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourLocationByIdDto>(value);
        }

        public async Task<GetTourLocationByIdDto> GetByTourIdAsync(string tourId)
        {
            var value = await _collection.Find(x => x.TourId == tourId).FirstOrDefaultAsync();
            return _mapper.Map<GetTourLocationByIdDto>(value);
        }

        public async Task CreateAsync(CreateTourLocationDto dto)
        {
            var value = _mapper.Map<TourLocation>(dto);
            await _collection.InsertOneAsync(value);
        }

        public async Task UpdateAsync(UpdateTourLocationDto dto)
        {
            var value = _mapper.Map<TourLocation>(dto);
            await _collection.FindOneAndReplaceAsync(x => x.TourLocationId == dto.TourLocationId, value);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.FindOneAndDeleteAsync(x => x.TourLocationId == id);
        }
    }
}
