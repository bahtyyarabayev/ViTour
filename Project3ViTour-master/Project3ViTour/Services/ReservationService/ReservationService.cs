using AutoMapper;
using MongoDB.Driver;
using Project3ViTour.Dtos.ReservationDtos;
using Project3ViTour.Entities;
using Project3ViTour.Settings;
namespace Project3ViTour.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _collection;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMapper _mapper;

        public ReservationService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Reservation>(settings.ReservationCollectionName);
            _tourCollection = database.GetCollection<Tour>(settings.TourCollectionName);
            _reservationCollection = database.GetCollection<Reservation>(settings.ReservationCollectionName);

           
            _mapper = mapper;
        }

        public async Task<List<ResultReservationDto>> GetAllReservationsAsync()
        {
            var values = await _collection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultReservationDto>>(values);

            foreach (var item in result)
            {
                var tour = await _tourCollection.Find(x => x.TourId == item.TourId).FirstOrDefaultAsync();
                item.TourTitle = tour?.Title;
            }

            return result;
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(string id)
        {
            var value = await _collection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetReservationByIdDto>(value);
            var tour = await _tourCollection.Find(x => x.TourId == result.TourId).FirstOrDefaultAsync();
            result.TourTitle = tour?.Title;
            return result;
        }

        public async Task CreateReservationAsync(CreateReservationDto dto)
        {
            var value = _mapper.Map<Reservation>(dto);
            value.ReservationDate = DateTime.Now;
            value.Status = false;
            await _collection.InsertOneAsync(value);
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _collection.FindOneAndDeleteAsync(x => x.ReservationId == id);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto dto)
        {
            
            var tour = await _tourCollection.Find(x => x.TourId == dto.TourId).FirstOrDefaultAsync();

            if (tour != null)
            {
                
                decimal adultPrice = tour.Price;
                decimal youthPrice = tour.Price * 0.85m;
                decimal childPrice = tour.Price * 0.70m;

               
                dto.TotalPrice = (dto.AdultCount * adultPrice) +
                                 (dto.YouthCount * youthPrice) +
                                 (dto.ChildCount * childPrice);
            }

            
            var values = _mapper.Map<Reservation>(dto);

           
            await _collection.ReplaceOneAsync(x => x.ReservationId == dto.ReservationId, values);
        }
    }
}
