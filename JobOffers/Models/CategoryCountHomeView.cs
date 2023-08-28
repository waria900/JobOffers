namespace JobOffers.Models
{
    public class CategoryCountHomeView
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int CategoryCount { get; set; }

    }
}
