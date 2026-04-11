using AutoMapper;
using Project3ViTour.Dtos.CategoryDtos;
using Project3ViTour.Dtos.ReservationDtos;
using Project3ViTour.Dtos.ReviewDtos;
using Project3ViTour.Dtos.TourDtos;
using Project3ViTour.Dtos.TourImageDtos;
using Project3ViTour.Dtos.TourLocationDtos;
using Project3ViTour.Dtos.TourPlanDtos;
using Project3ViTour.Entities;

namespace Project3ViTour.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping() 
        { 
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, GetCatgoryByIdDto>().ReverseMap();

            CreateMap<Tour, CreateTourDto>().ReverseMap();
            CreateMap<Tour, UpdateTourDto>().ReverseMap();
            CreateMap<Tour, ResultTourDto>().ReverseMap();
            CreateMap<Tour, GetTourByIdDto>().ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewByIdDto>().ReverseMap();
            CreateMap<Review, ResultReviewByTourDto>().ReverseMap();

            CreateMap<TourImage, CreateGalleryImageDto>().ReverseMap();
            CreateMap<TourImage, UpdateGalleryImageDto>().ReverseMap();
            CreateMap<TourImage, ResultGalleryImageDto>().ReverseMap();
            CreateMap<TourImage, GetGalleryImageByIdDto>().ReverseMap();

            // DOĞRU YAPI: Planlar kendi DTO'ları ile eşleşir
            CreateMap<TourPlan, CreateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, UpdateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanDto>().ReverseMap();

            CreateMap<TourLocation, ResultTourLocationDto>().ReverseMap();
            CreateMap<TourLocation, GetTourLocationByIdDto>().ReverseMap();
            CreateMap<TourLocation, CreateTourLocationDto>().ReverseMap();
            CreateMap<TourLocation, UpdateTourLocationDto>().ReverseMap();

            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();
            CreateMap<GetReservationByIdDto, UpdateReservationDto>().ReverseMap();


        }

    }
}
