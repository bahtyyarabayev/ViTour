using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.ReviewDtos;
using Project3ViTour.Dtos.TourDtos;
using Project3ViTour.Entities;
using Project3ViTour.Services.ReviewService;
using Project3ViTour.Services.TourLocationService;
using Project3ViTour.Services.TourPlanService;
using Project3ViTour.Services.TourService;
namespace Project3ViTour.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IReviewService _reviewService;
        private readonly ITourLocationService _tourLocationService;

        public TourController(ITourService tourService,IReviewService reviewService,ITourLocationService tourLocationService)
        {
            _tourService = tourService;
            _reviewService = reviewService;
            _tourLocationService = tourLocationService;
          
        }
        public IActionResult CreateTour()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createDtoTour)
        {
            await _tourService.CreateTourAsync(createDtoTour);
            return RedirectToAction("TourList");
        }

        public IActionResult TourList()
        {
            return View();
        }
        // 1. TourDetay metodunu "async Task<IActionResult>" olarak güncelledik
        public async Task<IActionResult> TourDetay(string id)
        {
            var values = await _tourService.GetTourByIdAsync(id);
            if (values == null) return NotFound();

            var reviews = await _reviewService.GetAllReviewByTourIdAsync(id);
            values.Reviews = reviews;

            ViewBag.TourId = id;
            ViewBag.Tours = await _tourService.GetAllToursAsync();
            values.TourLocation = await _tourLocationService.GetByTourIdAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            createReviewDto.ReviewDate = DateTime.Now;
            createReviewDto.Status = false;
            await _reviewService.CreateReviewAsync(createReviewDto);
            return RedirectToAction("TourDetay", new { id = createReviewDto.TourId });
        }

        public async Task<IActionResult> Booking()
        {
            var tours = await _tourService.GetAllToursAsync();
            return View(tours);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocation(string id, string location)
        {
            // Manuel olarak DTO oluşturuyoruz
            var updateDto = new UpdateTourDto
            {
                TourId = id, // DTO içindeki ID alanı neyse o (Id veya TourId)
                Location = location,
                // Eğer UpdateTourDto içinde başka zorunlu alanlar varsa (Title vb.) 
                // onları da tour nesnesinden alıp buraya eklemelisin.
            };

            await _tourService.UpdateTourAsync(updateDto);

            return Json(new { success = true });
        }
    }

}
