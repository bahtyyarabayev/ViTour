using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Services.TourPlanService;

namespace Project3ViTour.ViewComponents.TourViewComponents
{
    public class _TourPlaningComponentPartial : ViewComponent
    {
        private readonly ITourPlanService _tourPlanService;

        public _TourPlaningComponentPartial(ITourPlanService tourPlanService)
        {
            _tourPlanService = tourPlanService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = RouteData.Values["id"]?.ToString();
            var plans = await _tourPlanService.GetPlansByTourIdAsync(id);
            return View(plans);
        }
    }
}