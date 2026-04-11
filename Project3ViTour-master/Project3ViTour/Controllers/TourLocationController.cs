using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.TourLocationDtos;
using Project3ViTour.Services.TourLocationService;
using Project3ViTour.Services.TourService;

namespace Project3ViTour.Controllers
{
    public class TourLocationController : Controller
    {
        private readonly ITourLocationService _tourLocationService;
        private readonly ITourService _tourService;

        public TourLocationController(ITourLocationService tourLocationService, ITourService tourService)
        {
            _tourLocationService = tourLocationService;
            _tourService = tourService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _tourLocationService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Tours = await _tourService.GetAllToursAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTourLocationDto dto)
        {
            await _tourLocationService.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var value = await _tourLocationService.GetByIdAsync(id);
            var dto = new UpdateTourLocationDto
            {
                TourLocationId = value.TourLocationId,
                ImageUrl = value.ImageUrl,
                Description = value.Description,
                TourId = value.TourId,
                Items = value.Items
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTourLocationDto dto)
        {
            await _tourLocationService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _tourLocationService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}