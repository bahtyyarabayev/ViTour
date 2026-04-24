using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Dtos.DashboardDtos;
using Project3ViTour.Services.ReservationService;
using Project3ViTour.Services.ReviewService;
using Project3ViTour.Services.TourService;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Project3ViTour.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IReservationService _reservationService;
        private readonly IReviewService _reviewService;

        public DashboardController(ITourService tourService, IReservationService reservationService, IReviewService reviewService)
        {
            _tourService = tourService;
            _reservationService = reservationService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllToursAsync();
            var reservations = await _reservationService.GetAllReservationsAsync();
            var reviews = await _reviewService.GetAllReviewsAsync();

            var topTours = reservations
                .GroupBy(x => x.TourTitle)
                .Select(g => new TopTourDto
                {
                    TourTitle = g.Key,
                    ReservationCount = g.Count()
                })
                .OrderByDescending(x => x.ReservationCount)
                .Take(4)
                .ToList();

            int maxCount = topTours.Any() ? topTours.Max(x => x.ReservationCount) : 1;
            topTours.ForEach(x => x.Percent = (int)((double)x.ReservationCount / maxCount * 100));

            var dto = new DashboardDto
            {
                TourCount = tours.Count,
                ReservationCount = reservations.Count,
                ReviewCount = reviews.Count,
                TotalRevenue = reservations.Sum(x => x.TotalPrice),
                LastReservations = reservations.OrderByDescending(x => x.ReservationDate).Take(5).ToList(),
                LastReviews = reviews.OrderByDescending(x => x.ReviewDate).Take(3).ToList(),
                TopTours = topTours
            };

            return View(dto);
        }

        public async Task<IActionResult> ExportPdf()
        {
            var tours = await _tourService.GetAllToursAsync();
            var reservations = await _reservationService.GetAllReservationsAsync();

          
            var topTours = reservations
                .GroupBy(x => x.TourTitle)
                .Select(g => new TopTourDto
                {
                    TourTitle = g.Key,
                    ReservationCount = g.Count()
                })
                .OrderByDescending(x => x.ReservationCount)
                .ToList();

            var dto = new DashboardDto
            {
                TourCount = tours.Count,
                ReservationCount = reservations.Count,
                TotalRevenue = reservations.Sum(x => x.TotalPrice),
                LastReservations = reservations,
                TopTours = topTours
            };

            var document = QuestPDF.Fluent.Document.Create(docContainer =>
            {
                docContainer.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header().Element(header => ComposeHeader(header));
                    page.Content().Element(content => ComposeContent(content, dto));

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("ViTour Admin Raporu — ");
                        x.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                    });
                });
            });

            var pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"vitour-rapor-{DateTime.Now:yyyyMMdd}.pdf");
        }

        private void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(innerCol =>
                    {
                        innerCol.Item().Text("ViTour").FontSize(22).Bold().FontColor("#0C447C");
                        innerCol.Item().Text("Genel Tur ve Rezervasyon Raporu").FontSize(12).FontColor("#378ADD");
                        innerCol.Item().Text(DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"))).FontSize(10).FontColor("#888");
                    });
                });
                col.Item().PaddingTop(5).PaddingBottom(10).BorderBottom(1).BorderColor("#E6F1FB");
            });
        }

        private void ComposeContent(QuestPDF.Infrastructure.IContainer container, DashboardDto dto)
        {
            container.PaddingTop(20).Column(col =>
            {
               
                col.Item().PaddingBottom(20).Row(row =>
                {
                    row.RelativeItem().Border(1).BorderColor("#E6F1FB").Padding(12).Column(c => {
                        c.Item().Text("Toplam Tur").FontSize(9).FontColor("#888");
                        c.Item().Text(dto.TourCount.ToString()).FontSize(18).Bold().FontColor("#185FA5");
                    });
                    row.ConstantItem(10);
                    row.RelativeItem().Border(1).BorderColor("#E6F1FB").Padding(12).Column(c => {
                        c.Item().Text("Rezervasyon").FontSize(9).FontColor("#888");
                        c.Item().Text(dto.ReservationCount.ToString()).FontSize(18).Bold().FontColor("#3B6D11");
                    });
                    row.ConstantItem(10);
                    row.RelativeItem().Border(1).BorderColor("#E6F1FB").Padding(12).Column(c => {
                        c.Item().Text("Toplam Gelir").FontSize(9).FontColor("#888");
                        c.Item().Text($"${dto.TotalRevenue:F2}").FontSize(18).Bold().FontColor("#534AB7");
                    });
                });

                
                col.Item().PaddingBottom(8).Text("Tur Bazlı Rezervasyon Dağılımı").FontSize(13).Bold().FontColor("#042C53");
                col.Item().PaddingBottom(20).Table(table =>
                {
                    table.ColumnsDefinition(columns => {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(1);
                    });
                    table.Header(header => {
                        header.Cell().Background("#E6F1FB").Padding(8).Text("Tur Adı").Bold().FontColor("#0C447C");
                        header.Cell().Background("#E6F1FB").Padding(8).Text("Adet").Bold().FontColor("#0C447C");
                    });
                    foreach (var tour in dto.TopTours)
                    {
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(8).Text(tour.TourTitle ?? "-");
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(8).Text(tour.ReservationCount.ToString()).FontColor("#185FA5");
                    }
                });

                
                col.Item().PaddingBottom(8).Text("Müşteri ve İletişim Detayları").FontSize(13).Bold().FontColor("#042C53");
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns => {
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn(1.5f); 
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn(1); 
                    });

                    table.Header(header => {
                        header.Cell().Background("#E6F1FB").Padding(6).Text("Ad Soyad").Bold().FontSize(8).FontColor("#0C447C");
                        header.Cell().Background("#E6F1FB").Padding(6).Text("E-posta").Bold().FontSize(8).FontColor("#0C447C");
                        header.Cell().Background("#E6F1FB").Padding(6).Text("Telefon").Bold().FontSize(8).FontColor("#0C447C");
                        header.Cell().Background("#E6F1FB").Padding(6).Text("Tur").Bold().FontSize(8).FontColor("#0C447C");
                        header.Cell().Background("#E6F1FB").Padding(6).Text("Tutar").Bold().FontSize(8).FontColor("#0C447C");
                    });

                    foreach (var r in dto.LastReservations)
                    {
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(6).Text(r.NameSurname).FontSize(8);
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(6).Text(r.Email ?? "-").FontSize(8);
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(6).Text(r.Phone ?? "-").FontSize(8);
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(6).Text(r.TourTitle ?? "-").FontSize(8);
                        table.Cell().BorderBottom(1).BorderColor("#f0f0f0").Padding(6).Text($"${r.TotalPrice:F2}").FontSize(8).Bold();
                    }
                });
            });
        }

    }
}

