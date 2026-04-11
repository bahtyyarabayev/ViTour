namespace Project3ViTour.Settings
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string TourCollectionName { get; set; }

        public string CategoryCollectionName { get; set; }

        public string ReviewCollectionName { get; set; }

        public string GalleryImageCollectionName { get; set; }

        public string TourLocationCollectionName { get; set; }

        public string ReservationCollectionName { get; set; }
    }
}
