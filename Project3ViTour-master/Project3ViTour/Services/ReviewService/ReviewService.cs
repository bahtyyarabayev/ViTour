using AutoMapper;
using MongoDB.Driver;
using Project3ViTour.Dtos.CategoryDtos;
using Project3ViTour.Dtos.ReviewDtos;
using Project3ViTour.Entities;
using Project3ViTour.Settings;

namespace Project3ViTour.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Review> _reviewCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        public ReviewService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reviewCollection = database.GetCollection<Review>(_databaseSettings.ReviewCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
        }

            public async Task CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var value = _mapper.Map<Review>(createReviewDto);
            await _reviewCollection.InsertOneAsync(value);
        }

        public async Task DeleteReviewAsync(string id)
        {
            await _reviewCollection.DeleteOneAsync(x => x.ReviewId == id);
        }

        public async Task<List<ResultReviewByTourDto>> GetAllReviewByTourIdAsync(string id)
        {
            var values = await _reviewCollection.Find(x => x.TourId == id).ToListAsync();
            return _mapper.Map<List<ResultReviewByTourDto>>(values);
        }

        public async Task<List<ResultReviewDto>> GetAllReviewsAsync()
        {
            var values = await _reviewCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultReviewDto>>(values);

            foreach (var item in result)
            {
                var tour = await _tourCollection.Find(x => x.TourId == item.TourId).FirstOrDefaultAsync();
                item.TourTitle = tour?.Title;
            }

            return result;
        }
    

        public async Task<GetReviewByIdDto> GetReviewByIdAsync(string id)
        {
            var value = await _reviewCollection.Find(x => x.ReviewId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetReviewByIdDto>(value);
            var tour = await _tourCollection.Find(x => x.TourId == result.TourId).FirstOrDefaultAsync();
            result.TourTitle = tour?.Title;
            return result;
        }

        public async Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            var value = _mapper.Map<Review>(updateReviewDto);
            await _reviewCollection.FindOneAndReplaceAsync(x => x.ReviewId == updateReviewDto.ReviewId, value);
        }
    }
}
