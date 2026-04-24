using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3ViTour.Dtos.TourImageDtos;
using Project3ViTour.Services.GalleryImageService;
using Project3ViTour.Services.TourService;

namespace Project3ViTour.Controllers
{
    public class TourGalleryController : Controller
    {
    private readonly IGalleryImageService _galleryImageService;
    private readonly ITourService _tourService;

    public TourGalleryController(IGalleryImageService galleryImageService, ITourService tourService)
    {
        _galleryImageService = galleryImageService;
        _tourService = tourService;
    }

    public async Task<IActionResult> Index()
    {
        var value = await _galleryImageService.GetAllGalleryImageAsync();
        var tours = await _tourService.GetAllToursAsync();
        ViewBag.Tours = tours.ToDictionary(t => t.TourId, t => t.Title);
        return View(value);
    }
        [HttpGet]
        public async Task<IActionResult> CreateGalleryImage()
        {
           
            var tours = await _tourService.GetAllToursAsync();

          
            ViewBag.Tours = tours.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,          
                Value = x.TourId.ToString() 
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGalleryImage(CreateGalleryImageDto createGalleryImageDto)
        {
            if (!ModelState.IsValid)
            {
                
                var tours = await _tourService.GetAllToursAsync();
                ViewBag.Tours = tours.Select(x => new SelectListItem { Text = x.Title, Value = x.TourId }).ToList();
                return View(createGalleryImageDto);
            }

            await _galleryImageService.CreateGalleryImageAsync(createGalleryImageDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteGalleryImage(string id)
    {
        await _galleryImageService.DeleteGalleryImageAsync(id);
        return RedirectToAction("Index");
    }
        [HttpGet]
        public async Task<IActionResult> UpdateGalleryImage(string id)
        {
            
            var value = await _galleryImageService.GetGalleryImageByIdAsync(id);

            
            var tours = await _tourService.GetAllToursAsync();

            
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");

            return View(value);
        }
        [HttpPost]
    public async Task<IActionResult> UpdateGalleryImage(UpdateGalleryImageDto updateGalleryImageDto)
    {
        await _galleryImageService.UpdateGalleryImageAsync(updateGalleryImageDto);
        return RedirectToAction("Index");
    }
}
}
