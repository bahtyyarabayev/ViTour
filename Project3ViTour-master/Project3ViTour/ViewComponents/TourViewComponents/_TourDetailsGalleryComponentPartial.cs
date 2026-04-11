using Microsoft.AspNetCore.Mvc;
using Project3ViTour.Services.GalleryImageService;

namespace Project3ViTour.ViewComponents.TourViewComponents
{
    // 👇 BU SATIRI EKLE (Hatanın kesin çözümü budur)
    [ViewComponent(Name = "_TourDetailsGalleryComponentPartial")]
    public class _TourDetailsGalleryComponentPartial : ViewComponent
    {
        private readonly IGalleryImageService _galleryImageService;

        public _TourDetailsGalleryComponentPartial(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id, int? take = null)
        {
            var images = await _galleryImageService.GetGalleryImagesByTourIdAsync(id);
            var result = take.HasValue ? images.Take(take.Value).ToList() : images;
            return View(result);
        }
    }
}