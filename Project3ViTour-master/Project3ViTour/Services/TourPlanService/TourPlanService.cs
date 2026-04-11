using MongoDB.Driver;
using Project3ViTour.Dtos.TourPlanDtos;
using Project3ViTour.Entities;
using Project3ViTour.Settings;

namespace Project3ViTour.Services.TourPlanService
{
    public class TourPlanService : ITourPlanService
    {
        private readonly IMongoCollection<TourPlan> _tourPlanCollection;
        private readonly IMongoCollection<Tour> _tourCollection;

        public TourPlanService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tourPlanCollection = database.GetCollection<TourPlan>("TourPlans");
            _tourCollection = database.GetCollection<Tour>("Tours");
        }

        public async Task<List<ResultTourPlanDto>> GetAllTourPlansAsync()
        {
            var plans = await _tourPlanCollection.Find(_ => true).ToListAsync();
            var result = new List<ResultTourPlanDto>();

            foreach (var x in plans)
            {
                var tour = await _tourCollection.Find(t => t.TourId == x.TourId).FirstOrDefaultAsync();
                result.Add(new ResultTourPlanDto
                {
                    TourPlanId = x.TourPlanId,
                    TourId = x.TourId,
                    TourTitle = tour?.Title,
                    DayNumber = x.DayNumber,
                    Title = x.Title,
                    Description = x.Description,
                    Items = x.Items
                });
            }

            return result;
        }

        public async Task<List<ResultTourPlanDto>> GetPlansByTourIdAsync(string tourId)
        {
            var plans = await _tourPlanCollection.Find(x => x.TourId == tourId).SortBy(x => x.DayNumber).ToListAsync();
            return plans.Select(x => new ResultTourPlanDto
            {
                TourPlanId = x.TourPlanId,
                TourId = x.TourId,
                DayNumber = x.DayNumber,
                Title = x.Title,
                Description = x.Description,
                Items = x.Items
            }).ToList();
        }

        public async Task CreateTourPlanAsync(CreateTourPlanDto createTourPlanDto)
        {
            var plan = new TourPlan
            {
                TourId = createTourPlanDto.TourId,
                DayNumber = createTourPlanDto.DayNumber,
                Title = createTourPlanDto.Title,
                Description = createTourPlanDto.Description,
                Items = createTourPlanDto.Items ?? new List<string>()
            };
            await _tourPlanCollection.InsertOneAsync(plan);
        }

        public async Task UpdateTourPlanAsync(UpdateTourPlanDto updateTourPlanDto)
        {
            var plan = new TourPlan
            {
                TourPlanId = updateTourPlanDto.TourPlanId,
                TourId = updateTourPlanDto.TourId,
                DayNumber = updateTourPlanDto.DayNumber,
                Title = updateTourPlanDto.Title,
                Description = updateTourPlanDto.Description,
                Items = updateTourPlanDto.Items ?? new List<string>()
            };
            await _tourPlanCollection.ReplaceOneAsync(x => x.TourPlanId == updateTourPlanDto.TourPlanId, plan);
        }

        public async Task DeleteTourPlanAsync(string id)
        {
            await _tourPlanCollection.DeleteOneAsync(x => x.TourPlanId == id);
        }

        public async Task<ResultTourPlanDto> GetTourPlanByIdAsync(string id)
        {
            var plan = await _tourPlanCollection.Find(x => x.TourPlanId == id).FirstOrDefaultAsync();
            if (plan == null) return null;
            return new ResultTourPlanDto
            {
                TourPlanId = plan.TourPlanId,
                TourId = plan.TourId,
                DayNumber = plan.DayNumber,
                Title = plan.Title,
                Description = plan.Description,
                Items = plan.Items
            };
        }
    }
}
