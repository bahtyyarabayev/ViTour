using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.ReviewDtos;
using Project3ViTour.Services.ReviewService;

namespace Project3ViTour.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            createReviewDto.Status = false;
            await _reviewService.CreateReviewAsync(createReviewDto);
            return RedirectToAction("ReviewList");
        }

        public async Task<IActionResult> GetReviewByTourId(string id)
        {
            var values = await _reviewService.GetAllReviewByTourIdAsync(id);
            return View(values);
        }

        public async Task<IActionResult> Index()
        {
            var values = await _reviewService.GetAllReviewsAsync();
            return View(values);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var value = await _reviewService.GetReviewByIdAsync(id);
            return View(value);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction("Index");
        }
    }
}