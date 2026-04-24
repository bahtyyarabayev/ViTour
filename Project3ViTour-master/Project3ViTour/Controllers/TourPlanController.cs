using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project3ViTour.Dtos.TourPlanDtos;
using Project3ViTour.Services.TourPlanService;
using Project3ViTour.Services.TourService;

namespace Project3ViTour.Controllers
{
    public class TourPlanController : Controller
    {
        private readonly ITourPlanService _tourPlanService;
        private readonly ITourService _tourService; 

        public TourPlanController(ITourPlanService tourPlanService, ITourService tourService)
        {
            _tourPlanService = tourPlanService;
            _tourService = tourService;
        }

        public async Task<IActionResult> Index()
        {
           
            var values = await _tourPlanService.GetAllTourPlansAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTourPlan()
        {
            var tours = await _tourService.GetAllToursAsync();
            ViewBag.Tours = new SelectList(tours, "TourId", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTourPlan(List<CreateTourPlanDto> model)
        {
            if (model != null && model.Any())
            {
                foreach (var plan in model)
                {
                 
                    if (string.IsNullOrEmpty(plan.Title)) continue;

                    await _tourPlanService.CreateTourPlanAsync(plan);
                }
                return RedirectToAction("Index", "TourPlan");
            }

    
            return View();
        }
    }
}
