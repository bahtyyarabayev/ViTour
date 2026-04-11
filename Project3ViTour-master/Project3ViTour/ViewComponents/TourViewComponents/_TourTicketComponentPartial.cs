using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.TourDtos;
using Project3ViTour.Services.TourService;

namespace Project3ViTour.ViewComponents.TourViewComponents
{
    public class _TourTicketComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(GetTourByIdDto tour)
        {
            return View(tour);
        }
    }
}