using AutoMapper;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.ReservationDtos;
using Project3ViTour.Services.ReservationService;
using Project3ViTour.Services.TourService;
using Project3ViTour.Services.MailService;
using IMailService = Project3ViTour.Services.MailService.IMailService;

namespace Project3ViTour.Controllers
{
    public class ReservationController: Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public ReservationController(IReservationService reservationService, ITourService tourService, IMapper mapper, IMailService mailService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
            _mapper = mapper;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string tourId, int childCount, int youthCount, int adultCount, decimal totalPrice)
        {
            var tour = await _tourService.GetTourByIdAsync(tourId);
            ViewBag.TourTitle = tour?.Title;
            ViewBag.TourId = tourId;
            ViewBag.ChildCount = childCount;
            ViewBag.YouthCount = youthCount;
            ViewBag.AdultCount = adultCount;
            ViewBag.TotalPrice = totalPrice;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationDto dto)
        {
            await _reservationService.CreateReservationAsync(dto);

            var tour = await _tourService.GetTourByIdAsync(dto.TourId);
            await _mailService.SendReservationMailAsync(
                dto.Email,
                dto.NameSurname,
                tour?.Title,
                dto.TourDate,
                dto.ChildCount,
                dto.YouthCount,
                dto.AdultCount,
                dto.TotalPrice
            );

            TempData["Success"] = "true";
            return RedirectToAction("Create", "Reservation", new { tourId = dto.TourId });
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var values = await _reservationService.GetAllReservationsAsync();
            return View(values);
        }

        public async Task<IActionResult> Detail(string id)
        {
            
            var value = await _reservationService.GetReservationByIdAsync(id);

            
            var mappedValue = _mapper.Map<UpdateReservationDto>(value);

            
            return View(mappedValue);
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateReservationDto dto)
        {
            await _reservationService.UpdateReservationAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("Index");
        }
    }
}

